using UnityEngine;
using Group8.TrashDash.Level;
using Group8.TrashDash.Setting;
using UnityEngine.SceneManagement;
using Lncodes.Module.Unity.Template;

namespace Group8.TrashDash.Core
{
    public sealed class GameManager : SingletonMonoBehavior<GameManager>
    {
        [field: SerializeField]
        public Tags Tags { get; private set; } = default;

        [field: SerializeField]
        public Scenes Scenes { get; private set; } = default;

        [field: SerializeField]
        public LevelHandler LevelHandler { get; private set; } = default;

        [field: SerializeField]
        public SettingDataPersistence SettingDataPersistence { get; private set; } = default;

        protected override void Awake()
        {
            base.Awake();
            LevelHandler.OnAwake();
        }

        private void Start()
        {
            SettingDataPersistence.OnStart();
            SceneManager.LoadScene(Scenes.MainMenu);
        }
    }
}