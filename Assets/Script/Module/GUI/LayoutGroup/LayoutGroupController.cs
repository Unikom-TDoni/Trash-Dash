using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Still can be improved by move the item method
/// </summary>
namespace Lnco.Unity.Module.Layout
{
    [RequireComponent(typeof(LayoutGroup))]
    public abstract class LayoutGroupController<TViewHolder, TContent> : MonoBehaviour where TViewHolder : ViewHolder<TContent>
    {
        [SerializeField]
        protected TViewHolder ViewHolder = default;

        protected readonly Collection<TViewHolder> GeneratedViewHolders = new();

        protected abstract TViewHolder InstatiateViewHolder();

        public IReadOnlyCollection<TViewHolder> GetViewHolders() =>
            GeneratedViewHolders;

        public void Create()
        {
            var obj = InstatiateViewHolder();
            GeneratedViewHolders.Add(obj);
            obj.transform.SetParent(transform, default);
        }

        public void Remove(int index)
        {
            Destroy(GeneratedViewHolders[index]);
            GeneratedViewHolders.RemoveAt(index);
        }

        public void Clear()
        {
            foreach (var item in GeneratedViewHolders) Destroy(item);
            GeneratedViewHolders.Clear();
        }

        public bool TryRefreshContent(IEnumerable<TContent> content)
        {
            var index = 0;
            foreach (var item in content)
            {
                if (index >= GeneratedViewHolders.Count) return false;
                GeneratedViewHolders[index].UpdateContent(item);
                index++;
            }
            return true;
        }

        public bool TryUpdateContent(TContent content, int index)
        {
            if (index >= GeneratedViewHolders.Count) return false;
            GeneratedViewHolders[index].UpdateContent(content);
            return true;
        }

        public bool TryUpdateContent(TContent content, TViewHolder viewHolder)
        {
            var instance = GeneratedViewHolders.FirstOrDefault((item) => item.Equals(viewHolder));
            if (instance is null) return false;
            instance.UpdateContent(content);
            return true;
        }
    }
}