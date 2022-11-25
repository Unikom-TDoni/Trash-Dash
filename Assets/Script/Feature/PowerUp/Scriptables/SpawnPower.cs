using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Spawn", menuName = "PowerUp/Spawn")]
public class SpawnPower : PowerUpSO
{
    [SerializeField] private GameObject prefab;

    public override void Use()
    {
        Instantiate(prefab);
    }
}