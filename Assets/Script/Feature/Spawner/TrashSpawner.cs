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

        protected override IEnumerator Spawn()
        {
            StartCoroutine(base.Spawn());
            foreach(GameObject go in obj)
            {
                if (go == null) continue;

                Trash trash = go.GetComponent<Trash>();

                if (trash == null)
                    trash = go.AddComponent<Trash>();

                TrashContentInfo randomTrashInfo = trashInformations[Random.Range(0, trashInformations.Length)];

                if (trash.trashContentInfo == randomTrashInfo) break;

                trash.trashContentInfo = randomTrashInfo;

                if(randomTrashInfo.Mesh)
                    go.GetComponent<MeshFilter>().mesh = randomTrashInfo.Mesh;
                if(randomTrashInfo.Materials.Length > 0)
                    go.GetComponent<MeshRenderer>().materials = randomTrashInfo.Materials;

                //if (go.GetComponent<Collider>())
                //{
                //    Collider[] allColliders = go.GetComponents<Collider>();
                //    foreach (Collider c in allColliders)
                //    {
                //        Destroy(c);
                //    }
                //}
                if(go.GetComponents<Collider>().Length == 1)
                    go.AddComponent<BoxCollider>();
            }
            yield return null;
        }
    }
}