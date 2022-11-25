using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUpMultiply
{
    public abstract IEnumerator OnPowerUpMultiply(string name, float multiplier, float duration = 0);
}
