using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Spawner
{
    using Group8.TrashDash.Core;
    using Module.Spawner;
    using System;
    using UnityEngine.AI;
    using UnityEngine.InputSystem.HID;
    using UnityEngine.UIElements;

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

        private Bounds bounds;

        protected override void Start()
        {
            OnValidate();

            if (GameManager.Instance != null)
                spawnPrefab.maxObjectInPool = GameManager.Instance.LevelHandler.GetMaxAmountPowerUpSpawn();

            base.Start();
            RepeatSpawn(transform, minInterval, maxInterval, offset, amount, size, randomizeRotation);

            bounds = new Bounds(transform.position + offset, size + Vector3.up * 2);
        }

        protected override void AfterSpawn()
        {
            foreach (GameObject go in obj)
            {
                if (go == null) continue;

                CheckPosition(go);

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

        protected void CheckPosition(GameObject go)
        {
            if (GameManager.Instance == null) return;

            NavMeshHit hit;
            while(!NavMesh.SamplePosition(go.transform.position, out hit, 5.0f, NavMesh.AllAreas) || !IsInBound(hit.position))
            {
                go.transform.position = RandomSpawnPosition(transform, offset, size);
            }

            go.transform.position = hit.position + spawnOffset;
            //if true, is it in bound? if not random and check again
            //if false, random and check again
        }

        protected bool IsInBound(Vector3 point)
        {
            if (point == Vector3.positiveInfinity) return false;
            return bounds.Contains(point);
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
            float pick = UnityEngine.Random.value * totalSpawnWeight;
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