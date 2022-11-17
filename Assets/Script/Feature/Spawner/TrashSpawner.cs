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

                TrashInfo trashInfo = go.GetComponent<TrashInfo>();

                if (trashInfo == null)
                    trashInfo = go.AddComponent<TrashInfo>();

                trashInfo.trashContentInfo = trashInformations[Random.Range(0, trashInformations.Length)];
            }
            yield return null;
        }
    }
}