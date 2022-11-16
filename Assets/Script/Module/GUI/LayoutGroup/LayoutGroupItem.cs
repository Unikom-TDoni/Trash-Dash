using UnityEngine;

namespace Lnco.Unity.Module.Layout
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class LayoutGroupItem<T> : MonoBehaviour 
    {
        public abstract void UpdateContent(T content);
    }
}