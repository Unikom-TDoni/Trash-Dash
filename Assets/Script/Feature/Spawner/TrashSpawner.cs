using Group8.TrashDash.Item.Trash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Spawner
{
    using Module.Spawner;

    public class TrashSpawner : Spawner
    {
        [SerializeField] private TrashContentInfo[] trashInformations;

        protected override void AfterSpawn()
        {
            foreach (GameObject go in obj)
            {
                if (go == null) continue;

                Trash trash = go.GetComponent<Trash>();

                if (trash == null)
                    trash = go.AddComponent<Trash>();

                TrashContentInfo randomTrashInfo = trashInformations[Random.Range(0, trashInformations.Length)];

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
                    go.AddComponent<BoxCollider>().enabled = true;

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