using System;
using UnityEngine;
using Group8.TrashDash.Level;
using UnityEngine.SceneManagement;
using Lncodes.Module.Unity.Template;

namespace Group8.TrashDash.Core
{
    public sealed class GameManager : SingletonMonoBehavior<GameManager>
    {
        [field: SerializeField]
        public Tags Tags { get; private set; } = default;

        [field:SerializeField]
        public Scenes Scenes { get; private set; } = default;

        public LevelScriptableObject LevelInfo { get; set; } = default;

        private readonly LevelDataPersistence LevelDataPersistence = new();

        protected override void Awake()
        {
            base.Awake();
            LevelDataPersistence.OnAwake();
            SceneManager.LoadScene("Doni");
            //SceneManager.LoadScene(Scenes.MainMenu);
        }

        public void SaveCurrentLevelData(float score) =>
            LevelDataPersistence.Save(LevelInfo.Level, score);

        public LevelEntity GetLevelEntity(uint level) =>
            LevelDataPersistence.GetEntity(level);
    }
}