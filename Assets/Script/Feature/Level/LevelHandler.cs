using System;
using System.Linq;
using UnityEngine;
using Group8.TrashDash.Core;
using UnityEngine.SceneManagement;

namespace Group8.TrashDash.Level
{
    [Serializable]
    public sealed class LevelHandler
    {
        private int SelectedLevel = default;

        [SerializeField]
        private LevelScriptableObject[] _levelScriptableObjects = default;

        private readonly LevelDataPersistence LevelDataPersistence = new();

        public void OnAwake() =>
            LevelDataPersistence.OnAwake();

        public void SelectLevel(int level) =>
            SelectedLevel = level;

        public int GetTotalAmmountOfLevel() =>
            _levelScriptableObjects.Count();

        public LevelEntity GetLevelEntity(int level) =>
            LevelDataPersistence.GetEntity(level);

        public void SaveCurrentLevelData(float score) =>
            LevelDataPersistence.Save(SelectedLevel, score);

        public void SaveNextLevel()
        {
            SelectedLevel++;
            SaveCurrentLevelData(default);
        }

        public float[] GetStarScoreLimit() =>
            _levelScriptableObjects.First(item => item.Level.Equals(SelectedLevel)).ScoreStarLimit;

        public float[] GetStarScoreLimit(int level) =>
            _levelScriptableObjects.First(item => item.Level.Equals(level)).ScoreStarLimit;

        public void SpawnLevel() =>
            UnityEngine.Object.Instantiate(_levelScriptableObjects.First(item => item.Level.Equals(SelectedLevel)).Prefab, default, Quaternion.identity);

        public void GoToTheNextLevel()
        {
            SaveNextLevel();
            SceneManager.LoadScene(GameManager.Instance.Scenes.Gameplay);
        }
    }
}