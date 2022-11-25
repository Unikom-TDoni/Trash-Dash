using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Spawner
{
    using Module.Spawner;
    public class PowerUpSpawner : Spawner
    {
        [SerializeField] private PowerUpSO[] powerUps;

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
    }
}