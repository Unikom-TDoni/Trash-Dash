using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AoE", menuName = "PowerUp/AoE")]
public class AOEPower : PowerUpSO
{
    [SerializeField] private GameObject prefab;
    public float radius;
    public float interval;

    GameObject player;

    public override void Initialize(GameObject go)
    {
        player = go;
    }

    public override void Use()
    {
        Vector3 spawnPos = player.transform.position + player.transform.forward - new Vector3(0f, 1f, 0f);
        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
        obj.GetComponent<AOEPowerUp>().aoePower = this;
    }
}