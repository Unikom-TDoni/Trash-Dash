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

        [field: SerializeField]
        public LevelHandler LevelHandler { get; private set; } = default;

        protected override void Awake()
        {
            base.Awake();
            LevelHandler.OnAwake();
            SceneManager.LoadScene("Doni");
            //SceneManager.LoadScene(Scenes.MainMenu);
        }
    }
}