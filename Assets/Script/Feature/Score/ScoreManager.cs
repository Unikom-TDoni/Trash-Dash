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
        Wrong,
        Uncollected,
    }

    public class ScoreManager : MonoBehaviour
    {
        [Header("Scores")]
        [SerializeField] private int baseScore = 10;
        [SerializeField] private int correctScore = 20;
        [SerializeField] private int wrongScore = -20;
        [SerializeField] private int uncollectedScore = -10;

        private int currentScore;
        private int currentCombo;

        public int Score { get => currentScore; }
        public int Combo { get => currentCombo; }

        public event Action OnScoreChange;

        public void UpdateScore(ScoreState state)
        {
            switch (state)
            {
                case ScoreState.Collect: currentScore += baseScore; break;
                case ScoreState.Correct:
                    {
                        currentScore += correctScore;
                        currentCombo++;
                        HandleCombo();
                        break;
                    }
                case ScoreState.Wrong:
                    {
                        currentScore += wrongScore;
                        // Reset Combo
                        currentCombo = 0;
                        break;
                    }
                case ScoreState.Uncollected:
                    {
                        // Calculate All Items and multiply score (mungkin lebih baik di calculate setelah game selesai)
                        //currentScore += uncollectedScore;
                        break;
                    }
            }

            OnScoreChange?.Invoke();
        }

        private void HandleCombo()
        {
            currentScore += currentCombo * correctScore;
        }
    }
}