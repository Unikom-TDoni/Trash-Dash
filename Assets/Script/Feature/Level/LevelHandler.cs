using Group8.TrashDash.Core;
using System;
using System.Linq;
using UnityEngine;
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

        public float[] GetStarScoreLimit(int level) =>
            _levelScriptableObjects.First(item => item.Level.Equals(level)).ScoreStarLimit;

        public void SpawnLevel() =>
            GameObject.Instantiate(_levelScriptableObjects[SelectedLevel].Prefab, default, Quaternion.identity);

        public void GoToTheNextLevel()
        {
            SelectedLevel++;
            SaveCurrentLevelData(default);
            SceneManager.LoadScene(GameManager.Instance.Scenes.Gameplay);
        }
    }
}