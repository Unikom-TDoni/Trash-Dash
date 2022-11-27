using System;
using UnityEngine;

namespace Group8.TrashDash.Level
{
    [CreateAssetMenu(fileName = "Level", menuName = "Item/Level Info")]
    public sealed class LevelScriptableObject : ScriptableObject
    {
        [field: SerializeField]
        public uint Level { get; private set; } = default;

        [field:SerializeField]
        public GameObject Prefab { get; private set; } = default;

        [SerializeField]
        private float[] _scoreStarLimit = default;

        public float[] ScoreStarLimit => _scoreStarLimit;

        private void OnValidate()
        {
            if (_scoreStarLimit.Length != 3)
                Array.Resize(ref _scoreStarLimit, 3);
        }
    }
}