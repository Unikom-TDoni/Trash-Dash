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
        GameObject obj = Instantiate(prefab, player.transform.position, Quaternion.identity);
        obj.GetComponent<AOEPowerUp>().aoePower = this;
    }
}