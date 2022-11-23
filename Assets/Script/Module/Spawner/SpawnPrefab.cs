using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Prefab", menuName = "Item/Spawn Prefab")]
public class SpawnPrefab : ScriptableObject
{
    public GameObject prefab;
    public int maxObjectInPool;
}
