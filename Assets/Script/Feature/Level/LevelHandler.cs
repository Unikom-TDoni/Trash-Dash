using UnityEngine;

namespace Group8.TrashDash.Level
{
    public sealed class LevelHandler : MonoBehaviour
    {
        [SerializeField]
        private LevelScriptableObject[] _levelScriptableObject = default;

        [SerializeField]
        private LevelLayoutGroupController _levelLayoutGroupController = default;

        private void Awake()
        {
            _levelLayoutGroupController.InitLayout(_levelScriptableObject);
        }
    }
}