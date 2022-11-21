using UnityEngine;

namespace Group8.TrashDash.Item.Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class Trash : SpawnObject
    {
        public TrashContentInfo trashContentInfo;

        public override void Release()
        {
            base.Release();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}