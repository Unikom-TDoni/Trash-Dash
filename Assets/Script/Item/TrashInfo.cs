using UnityEngine;

namespace Group8.TrashDash.Item.Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class TrashInfo : SpawnObject
    {
        public TrashContentInfo trashContentInfo;

        public override void Release()
        {
            base.Release();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}