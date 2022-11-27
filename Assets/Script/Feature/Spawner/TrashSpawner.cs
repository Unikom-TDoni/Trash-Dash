using Group8.TrashDash.Item.Trash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Spawner
{
    using Group8.TrashDash.TrashBin;
    using Module.Spawner;
    using System.Linq;

    public class TrashSpawner : Spawner
    {
        [SerializeField] private TrashContentInfo[] trashInformations;

        protected override void Start()
        {
            base.Start();
            // Test Spawn
            //RepeatSpawn(new TrashBinTypes[] { TrashBinTypes.Organic }, transform, .1f, amount: 3);
        }

        public void InstantSpawn(TrashBinTypes[] trashTypes, Transform center, Vector3 offset = default, int amount = 1, Vector3 areaSize = default, bool randomizeRotation = false)
        {
            StartCoroutine(InstantSpawnCoroutine(trashTypes, center, offset, amount, areaSize, randomizeRotation));
        }

        protected IEnumerator InstantSpawnCoroutine(TrashBinTypes[] trashTypes, Transform center, Vector3 offset, int amount, Vector3 areaSize, bool randomizeRotation)
        {
            base.SpawnObjects(center, offset, amount, areaSize, randomizeRotation);

            yield return new WaitForFixedUpdate();

            AfterSpawn(trashTypes);
        }

        public void RepeatSpawn(TrashBinTypes[] trashTypes, Transform center, float interval, Vector3 offset = default, int amount = 1, Vector3 areaSize = default, bool randomizeRotation = false)
        {
            StartCoroutine(RepeatSpawnCoroutine(trashTypes, center, interval, offset, amount, areaSize, randomizeRotation));
        }

        protected IEnumerator RepeatSpawnCoroutine(TrashBinTypes[] trashTypes, Transform center, float interval, Vector3 offset, int amount, Vector3 areaSize, bool randomizeRotation)
        {
            yield return new WaitForSeconds(interval);

            base.SpawnObjects(center, offset, amount, areaSize, randomizeRotation);

            yield return new WaitForFixedUpdate();

            AfterSpawn(trashTypes);

            StartCoroutine(RepeatSpawnCoroutine(trashTypes, center, interval, offset, amount, areaSize, randomizeRotation));
        }

        protected void AfterSpawn(TrashBinTypes[] trashTypes)
        {
            foreach (GameObject go in obj)
            {
                if (go == null) continue;

                Trash trash = go.GetComponent<Trash>();

                if (trash == null)
                    trash = go.AddComponent<Trash>();

                TrashContentInfo[] filterTrashInformations = trashInformations.Where((info) => trashTypes.Contains(info.TrashBinType)).ToArray();

                TrashContentInfo randomTrashInfo = filterTrashInformations[Random.Range(0, filterTrashInformations.Length)];

                if (trash.trashContentInfo == randomTrashInfo)
                {
                    trash.Initialize();
                    continue;
                }

                trash.trashContentInfo = randomTrashInfo;

                if (randomTrashInfo.Mesh)
                    go.GetComponent<MeshFilter>().mesh = randomTrashInfo.Mesh;
                if (randomTrashInfo.Materials.Length > 0)
                    go.GetComponent<MeshRenderer>().materials = randomTrashInfo.Materials;

                BoxCollider[] colliders = go.GetComponents<BoxCollider>();
                if (colliders.Length == 1)
                {
                    go.AddComponent<BoxCollider>();

                    trash.Initialize();
                    continue;
                }
                // Resize Box Collider
                colliders[1].size = (randomTrashInfo.Mesh.bounds.size);
                colliders[1].center = (randomTrashInfo.Mesh.bounds.center);

                trash.Initialize();
            }

            base.AfterSpawn();
        }
    }
}