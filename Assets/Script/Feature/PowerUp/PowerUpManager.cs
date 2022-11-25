using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get => instance; }
    private static PowerUpManager instance;

    Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();

    public void StartPowerUp(string powerUpName, IEnumerator enumerator)
    {
        if (!coroutines.ContainsKey(powerUpName)) coroutines.Add(powerUpName, null);
        if (coroutines[powerUpName] != null) StopCoroutine(coroutines[powerUpName]);

        coroutines[powerUpName] = StartCoroutine(enumerator);
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }

        Debug.Log("There is more than one PowerUpManager detected.");
        Destroy(this);
    }
}
