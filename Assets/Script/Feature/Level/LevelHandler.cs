using System;
using System.Linq;
using UnityEngine;

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

        public void GetMaxLevel() =>
            _levelScriptableObjects.Max(item => item.Level);

        public void SaveNextLevel()
        {
            SelectedLevel++;
            if (_levelScriptableObjects.Any(item => item.Level.Equals(SelectedLevel))) return;
            SaveCurrentLevelData(default);
        }

        public float[] GetStarScoreLimit() =>
            _levelScriptableObjects.First(item => item.Level.Equals(SelectedLevel)).ScoreStarLimit;

        public float[] GetStarScoreLimit(int level) =>
            _levelScriptableObjects.First(item => item.Level.Equals(level)).ScoreStarLimit;

        public void SpawnLevel() =>
            UnityEngine.Object.Instantiate(_levelScriptableObjects.First(item => item.Level.Equals(SelectedLevel)).Prefab, default, Quaternion.identity);
    }
}