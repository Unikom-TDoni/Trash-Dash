using UnityEngine;

namespace Lncodes.Module.Unity.Editor
{
    [System.Serializable]
    public sealed class SceneObject
    {
        [SerializeField]
        private int _index = default;

        [SerializeField]
        private string _name = default;

        [SerializeField]
        private Object _asset = default;

        public int GetIndex() => _index;

        public string GetName() => _name;

        public Object GetAsset() => _asset;

        public static implicit operator string(SceneObject sceneObject) =>
            sceneObject._name;
    }
}
