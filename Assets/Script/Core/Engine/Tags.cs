using Lncodes.Module.Unity.Editor;
using System;
using UnityEngine;

namespace Group8.TrashDash.Core
{
    [Serializable]
    public struct Tags
    {
        [field:TagSelector]
        [field:SerializeField]
        public string Player { get; private set; }
    }
}
