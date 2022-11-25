using System;
using UnityEngine;

namespace Lncodes.Module.Unity.Editor
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class TagSelectorAttribute : PropertyAttribute
    {
        public readonly bool UseDefaultTagFieldDrawer;

        public TagSelectorAttribute(bool useDefaultTagFieldDrawer = default)
        {
            UseDefaultTagFieldDrawer = useDefaultTagFieldDrawer;
        }
    }
}

