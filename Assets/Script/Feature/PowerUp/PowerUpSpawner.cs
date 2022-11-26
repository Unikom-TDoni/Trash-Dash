using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Spawner
{
    using Module.Spawner;
    public class PowerUpSpawner : Spawner
    {
        [SerializeField] private PowerUpSO[] powerUps;

        [SerializeField] private Vector3 size = Vector3.one;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private int amount = 1;
        [SerializeField] private float interval = 5f;
        [SerializeField] private bool randomizeRotation;

        protected override void Start()
        {
            base.Start();
            RepeatSpawn(transform, interval, offset, amount, size, randomizeRotation);
        }

        protected override void AfterSpawn()
        {
            foreach (GameObject go in obj)
            {
                if (go == null) continue;

                PowerUp powerUp = go.GetComponent<PowerUp>();
                if (powerUp == null) go.AddComponent<PowerUp>();

                PowerUpSO randomPowerUp = powerUps[Random.Range(0, powerUps.Length)];

                powerUp.Initialize();
                if (powerUp.powerUpInfo == randomPowerUp) continue;

                powerUp.powerUpInfo = randomPowerUp;
                if (randomPowerUp.Mesh) go.GetComponent<MeshFilter>().mesh = randomPowerUp.Mesh;
                if (randomPowerUp.Materials.Length > 0) go.GetComponent<MeshRenderer>().materials = randomPowerUp.Materials;
            }

            base.AfterSpawn();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + offset, size);
        }
    }
}