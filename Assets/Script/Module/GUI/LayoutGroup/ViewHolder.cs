using UnityEngine;

namespace Lnco.Unity.Module.Layout
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class ViewHolder<T> : MonoBehaviour 
    {
        public abstract void UpdateContent(T content);
    }
}