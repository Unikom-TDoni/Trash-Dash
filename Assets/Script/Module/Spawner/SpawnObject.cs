using Group8.TrashDash.Module.Spawner;
using UnityEngine;

public abstract class SpawnObject : MonoBehaviour
{
    public void Release()
    {
        Spawner spawner = GetComponentInParent<Spawner>();

        if (spawner == null) return;

        spawner.OnRelease(this);
    }
}
