using Group8.TrashDash.Core;
using Group8.TrashDash.Item.Trash;
using Group8.TrashDash.Module.Pool;
using Group8.TrashDash.Spawner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Group8.TrashDash.Score
{
    public enum ScoreState
    {
        Collect,
        Correct,
        CorrectNoCombo,
        Wrong,
    }

    public class ScoreManager : MonoBehaviour
    {
        [Header("Scores")]
        [SerializeField] private int baseScore = 10;
        [SerializeField] private int correctScore = 20;
        [SerializeField] private int comboScore = 5;
        [SerializeField] private int wrongScore = -20;
        [SerializeField] private int uncollectedScore = -10;

        private int currentScore = 0;
        private int currentCombo = 0;

        [Header("Combo")]
        [SerializeField] private bool multiplyScore = true;

        [Header("References")]
        [SerializeField] private TrashSpawner trashSpawner;

        public int Score { get => currentScore; }
        public int Combo { get => currentCombo; }

        public event Action OnScoreChange;

        // Data
        public LevelResultData ResultData = new LevelResultData();

        public void UpdateScore(ScoreState state)
        {
            switch (state)
            {
                case ScoreState.Collect:
                    {
                        currentScore += baseScore;
                        ResultData.Collected++;
                        break;
                    }
                case ScoreState.Correct:
                    {
                        currentCombo++;
                        HandleCombo();
                        ResultData.Sorted++;
                        break;
                    }
                case ScoreState.CorrectNoCombo:
                    {
                        currentScore += correctScore;
                        break;
                    }
                case ScoreState.Wrong:
                    {
                        currentScore += wrongScore;
                        ResetCombo();
                        ResultData.WrongSorted++;
                        break;
                    }
            }

            OnScoreChange?.Invoke();
        }

        public void ResetCombo()
        {
            ResultData.HighestCombo = (ResultData.HighestCombo < currentCombo) ? currentCombo : ResultData.HighestCombo;
            currentCombo = 0;
        }

        private void HandleCombo()
        {
            currentScore += correctScore + comboScore * ((multiplyScore) ? currentCombo : 1);
        }

        public void GameEnd()
        {
            int uncollectedTrashCount = trashSpawner.GetActiveSpawnObject();
            currentScore += uncollectedTrashCount * uncollectedScore;
            ResultData.Uncollected = uncollectedTrashCount;
            ResultData.TotalScore = currentScore;

            ResetCombo();
            GameManager.Instance.LevelHandler.SaveCurrentLevelData(currentScore);
            // Set Game Over Panel Score UI to currentScore
        }
    }
}
