using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Group8.TrashDash.Level
{
    public sealed class LevelDataPersistence
    {
        private const string DataKey = "KEY_#$LEVEL";

        private const string DataKeyTutorial = "KEY_#$TutorialLevel";

        private HashSet<LevelEntity> _persistenceData = new();

        public bool IsPlayModeEnable { get; private set; } = default;

        public void OnAwake()
        {
            Load();
            Save(1, default);
        }

        public void Save(int level, float score)
        {
            var levelEntity = GetEntity(level);
            _persistenceData.Remove(levelEntity);
            if (score < levelEntity.HighScore) score = levelEntity.HighScore;
            _persistenceData.Add(new LevelEntity(level, score));
            PlayerPrefs.SetString(DataKey, JsonConvert.SerializeObject(_persistenceData, Formatting.Indented));
        }

        private void Load()
        {
            IsPlayModeEnable = PlayerPrefs.GetInt(DataKeyTutorial, 0) != 0;
            var json = PlayerPrefs.GetString(DataKey);
            if (string.IsNullOrEmpty(json)) return;
            _persistenceData = JsonConvert.DeserializeObject<HashSet<LevelEntity>>(json);
        }

        public LevelEntity GetEntity(int level) =>
            _persistenceData.FirstOrDefault(item => item.Level.Equals(level));

        public void EnablePlayMode()
        {
            IsPlayModeEnable = true;
            PlayerPrefs.SetInt(DataKeyTutorial, 1);
        }
    }
}