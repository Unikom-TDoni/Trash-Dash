using System;
using UnityEngine;

namespace Group8.TrashDash.Setting
{
    [Serializable]
    public struct Resolution
    {
        [field:SerializeField]
        public int Width { get; private set; }

        [field: SerializeField]
        public int Height { get; private set; }
    }
}