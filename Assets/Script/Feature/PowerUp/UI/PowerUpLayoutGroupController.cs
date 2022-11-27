using Lnco.Unity.Module.Layout;

public class PowerUpLayoutGroupController : LayoutGroupController<PowerUpLayoutGroupItem, PowerUpSO>
{
    public void InitLayout(int count)
    {
        for (int i = 0; i < count; i++)
            Create();
    }

    protected override PowerUpLayoutGroupItem InstatiateGroupItem() =>
            Instantiate(GroupItem, transform);
}
