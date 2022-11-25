using Lnco.Unity.Module.Layout;
using System.Collections.Generic;

namespace Group8.TrashDash.Level
{
    public sealed class LevelLayoutGroupController : LayoutGroupController<LevelLayoutGroupItem, LevelScriptableObject>
    {
        public void InitLayout(IEnumerable<LevelScriptableObject> data)
        {
            foreach (var item in data)
            {
                Create();
                GeneratedGroupItems[^1].UpdateContent(item);
            }
        }

        protected override LevelLayoutGroupItem InstatiateGroupItem() =>
            Instantiate(GroupItem, transform);
    }
}