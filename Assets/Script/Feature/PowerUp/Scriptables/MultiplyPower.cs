using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Multiplier", menuName = "PowerUp/Multiplier")]
public class MultiplyPower : PowerUpSO
{
    [SerializeField] private string parameterName;
    [SerializeField] private float multiplier;
    [SerializeField] private float duration;

    IPowerUpMultiply obj;

    public override void Initialize(GameObject go)
    {
        if (go == null) return;

        obj = go.GetComponent<IPowerUpMultiply>();
    }

    public override void Use()
    {
        if (obj == null) return;
        PowerUpManager.Instance.StartPowerUp(name, obj.OnPowerUpMultiply(parameterName, multiplier, duration));
    }
}
