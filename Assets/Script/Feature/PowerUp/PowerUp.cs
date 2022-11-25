using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : SpawnObject
{
    public PowerUpSO powerUpInfo;

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public virtual void Use()
    {
        powerUpInfo.Use();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PowerUpHandler handler = other.GetComponentInChildren<PowerUpHandler>();
        if (handler == null) return;

        if (!handler.Add(powerUpInfo)) return;

        powerUpInfo.Initialize(other.gameObject);
        base.Release();
    }
}
