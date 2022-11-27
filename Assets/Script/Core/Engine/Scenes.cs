using System;
using UnityEngine;
using Lncodes.Module.Unity.Editor;

namespace Group8.TrashDash.Core
{
    [Serializable]
    public struct Scenes
    {
        [field:SerializeField]
        public SceneObject MainMenu { get; private set; }

        [field:SerializeField]
        public SceneObject Gameplay { get; private set; }

        [field:SerializeField]
        public SceneObject LevelSelector { get; private set; }
    }
}