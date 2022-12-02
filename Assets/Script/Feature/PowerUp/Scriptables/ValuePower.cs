using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Value", menuName = "PowerUp/Value")]
public class ValuePower : PowerUpSO
{
    public string parameterName;
    public float value;

    PowerUpHandler obj;

    public override void Initialize(GameObject go)
    {
        if (go == null) return;

        obj = go.GetComponentInChildren<PowerUpHandler>();
    }

    public override void Use()
    {
        if (obj == null) return;
        obj.StartPowerUp(this);
    }
}
