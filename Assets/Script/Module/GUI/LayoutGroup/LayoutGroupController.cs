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
    public abstract class LayoutGroupController<TGroupItem, TContent> : MonoBehaviour where TGroupItem : LayoutGroupItem<TContent>
    {
        [SerializeField]
        protected TGroupItem GroupItem = default;

        protected readonly Collection<TGroupItem> GeneratedGroupItems = new();

        protected abstract TGroupItem InstatiateGroupItem();

        public IReadOnlyCollection<TGroupItem> GetGroupItems() =>
            GeneratedGroupItems;

        public void Create()
        {
            var obj = InstatiateGroupItem();
            GeneratedGroupItems.Add(obj);
        }

        public void Remove(int index)
        {
            Destroy(GeneratedGroupItems[index]);
            GeneratedGroupItems.RemoveAt(index);
        }

        public void Clear()
        {
            foreach (var item in GeneratedGroupItems) Destroy(item);
            GeneratedGroupItems.Clear();
        }

        public bool TryRefreshContent(IEnumerable<TContent> content)
        {
            var index = 0;
            foreach (var item in content)
            {
                if (index >= GeneratedGroupItems.Count) return false;
                GeneratedGroupItems[index].UpdateContent(item);
                index++;
            }
            return true;
        }

        public bool TryUpdateContent(TContent content, int index)
        {
            if (index >= GeneratedGroupItems.Count) return false;
            GeneratedGroupItems[index].UpdateContent(content);
            return true;
        }

        public bool TryUpdateContent(TContent content, TGroupItem groupItem)
        {
            var instance = GeneratedGroupItems.FirstOrDefault((item) => item.Equals(groupItem));
            if (instance == null) return false;
            instance.UpdateContent(content);
            return true;
        }
    }
}