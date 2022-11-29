using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject carPrefab;
    [SerializeField] float minSpawnDuration;
    [SerializeField] float maxSpawnDuration;
    [SerializeField] bool isRight;
    float timer;

    void Start()
    {
        timer = Random.Range(minSpawnDuration, maxSpawnDuration);
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        timer = Random.Range(minSpawnDuration, maxSpawnDuration);

        var car = Instantiate(carPrefab, transform.position, Quaternion.Euler(0, isRight ? 90 : -90, 0));
        car.GetComponent<CarMovement>().movingRight = isRight ? true : false;
    }
}
