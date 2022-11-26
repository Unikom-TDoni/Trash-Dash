using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : SpawnObject
{
    public PowerUpSO powerUpInfo;

    private Collider m_collider;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
        m_collider.enabled = false;
    }

    public void Initialize()
    {
        m_collider.enabled = true;
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

        m_collider.enabled = false;
        base.Release();
    }
}
