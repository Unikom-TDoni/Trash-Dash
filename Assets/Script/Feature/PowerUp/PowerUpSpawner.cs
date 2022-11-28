using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Spawner
{
    using Module.Spawner;
    public class PowerUpSpawner : Spawner
    {
        [SerializeField] private float totalSpawnWeight;
        [SerializeField] private PowerUpWeight[] powerUps;

        [SerializeField] private Vector3 size = Vector3.one;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private int amount = 1;
        [SerializeField] private float minInterval = 3f;
        [SerializeField] private float maxInterval = 5f;
        [SerializeField] private bool randomizeRotation;

        protected override void Start()
        {
            OnValidate();

            base.Start();
            RepeatSpawn(transform, minInterval, maxInterval, offset, amount, size, randomizeRotation);
        }

        protected override void AfterSpawn()
        {
            foreach (GameObject go in obj)
            {
                if (go == null) continue;

                PowerUp powerUp = go.GetComponent<PowerUp>();
                if (powerUp == null) go.AddComponent<PowerUp>();

                PowerUpSO randomPowerUp = GetRandomPowerUp();

                powerUp.Initialize();
                if (powerUp.powerUpInfo == randomPowerUp) continue;

                powerUp.SetInfo(randomPowerUp);
                if (randomPowerUp.Mesh) go.GetComponent<MeshFilter>().mesh = randomPowerUp.Mesh;
                if (randomPowerUp.Materials.Length > 0) go.GetComponent<MeshRenderer>().materials = randomPowerUp.Materials;
            }

            base.AfterSpawn();
        }

        private void OnValidate()
        {
            totalSpawnWeight = 0f;
            foreach (var item in powerUps)
            {
                totalSpawnWeight += item.Weight;
            }
        }

        private PowerUpSO GetRandomPowerUp()
        {
            int index = 0;
            float pick = Random.value * totalSpawnWeight;
            float cumulativeWeight = powerUps[0].Weight;

            while (pick > cumulativeWeight && index < powerUps.Length - 1)
            {
                index++;
                cumulativeWeight += powerUps[index].Weight;
            }

            return powerUps[index].Value;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + offset, size);
        }
    }
}