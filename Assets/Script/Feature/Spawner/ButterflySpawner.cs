using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawner;
    [SerializeField] Transform maxHeight, minHeight;
    [SerializeField] GameObject butterflyPrefab;
    [SerializeField] int maxSpawnLimit;
    [SerializeField] float minFlyTime, maxFlyTime;
    [SerializeField] Material[] wingsColors;
    public int butterflyCount;

    void Update()
    {
        if (butterflyCount < maxSpawnLimit)
        {
            int randomSpawner = Random.Range(0, maxSpawnLimit);
            var butterfly = Instantiate(butterflyPrefab, spawner[randomSpawner].transform.position, Quaternion.identity);
            var b = butterfly.GetComponent<ButterflyMovement>();
            b.butterflySpawner = this;
            b.maxHeight = maxHeight.position;
            b.minHeight = minHeight.position;
            int randomWingsColor = Random.Range(0, wingsColors.Length);
            b.wingsColor = wingsColors[randomWingsColor];
            float randomFlyTime = Random.Range(minFlyTime, maxFlyTime);
            b.flyTime = randomFlyTime;
            butterflyCount++;
            Debug.Log("butterfly spawned");
        }
    }
}
