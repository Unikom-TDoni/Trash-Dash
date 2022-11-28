using Group8.TrashDash.Core;
using Lnco.Unity.Module.Layout;

namespace Group8.TrashDash.Level
{
    public sealed class LevelLayoutGroupController : LayoutGroupController<LevelLayoutGroupItem, int>
    {
        private void Awake()
        {
            for (int i = 0; i < GameManager.Instance.LevelHandler.GetTotalAmmountOfLevel(); i++)
            {
                Create();
                GeneratedGroupItems[i].UpdateContent(i+1);
            }
        }

        protected override LevelLayoutGroupItem InstatiateGroupItem() =>
            Instantiate(GroupItem, transform);
    }
}