using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Multiplier", menuName = "PowerUp/Multiplier")]
public class MultiplyPower : PowerUpSO
{
    [SerializeField] private string parameterName;
    [SerializeField] private float multiplier;

    PowerUpHandler obj;

    public override void Initialize(GameObject go)
    {
        if (go == null) return;

        obj = go.GetComponentInChildren<PowerUpHandler>();
    }

    public override void Use()
    {
        if (obj == null) return;
        PowerUpManager.Instance.StartPowerUp(name, obj.PowerUpMultiply(parameterName, multiplier, duration));
    }
}
