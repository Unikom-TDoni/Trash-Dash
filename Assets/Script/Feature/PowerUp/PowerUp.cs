using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : SpawnObject
{
    public PowerUpSO powerUpInfo;

    private Collider m_collider;
    private ParticleSystem[] m_particleSystem;

    [SerializeField]
    private Image _imgIcon = default;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
        m_particleSystem = GetComponentsInChildren<ParticleSystem>();
        m_collider.enabled = false;
    }

    public void Initialize()
    {
        m_collider.enabled = true;
    }

    public void SetInfo(PowerUpSO powerUpSO)
    {
        if (powerUpSO == null) return;

        powerUpInfo = powerUpSO;

        foreach (ParticleSystem particle in m_particleSystem)
        {
            ParticleSystem.MainModule main = particle.main;
            main.startColor = powerUpInfo.Color;

            var colorOverLifetime = particle.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(powerUpInfo.Color, 0f),
                    new GradientColorKey(powerUpInfo.Color, 1f),
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(0f, 0f),
                    new GradientAlphaKey(1f, 0.4f),
                    new GradientAlphaKey(1f, 0.6f),
                    new GradientAlphaKey(0f, 1f),
                });
            colorOverLifetime.color = grad;
        }

        _imgIcon.sprite = powerUpInfo.Sprite;
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
