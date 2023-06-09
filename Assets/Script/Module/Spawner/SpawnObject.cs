using Group8.TrashDash.Module.Spawner;
using UnityEngine;

public abstract class SpawnObject : MonoBehaviour
{
    public Spawner spawner;
    public virtual void Release()
    {
        if (spawner == null) return;

        spawner.OnRelease(this);
    }
}
