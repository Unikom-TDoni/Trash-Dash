using System;
using UnityEngine;
using System.Collections.Generic;

namespace Lnco.Unity.Module.Storage
{
    [Serializable]
    public sealed class Inventory<T>
    {
        private ICollection<T> Items = default;

        [field: SerializeField]
        public int MaxCapacity { get; private set; } = default;

        public void Init(ICollection<T> items) =>
            (Items) = (items);

        public bool Any() =>
            Items.Count != default;

        public bool IsFull() =>
            Items.Count >= MaxCapacity;

        public IEnumerable<T> GetItems() =>
            Items;

        public bool TryAdd(params T[] items)
        {
            foreach (var item in items)
            {
                if (IsFull()) return false;
                Items.Add(item);
            }
            return true;
        }

        public bool TryRemove(params T[] items)
        {
            if (Any()) return false;
            foreach (var item in items) 
                Items.Remove(item);
            return true;
        }
    }
}