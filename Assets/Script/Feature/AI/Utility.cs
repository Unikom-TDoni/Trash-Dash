using UnityEngine;

public static class Utility {
    public static void LerpLookTowardsTarget(Transform transform, Vector3 targetPos, float? speed = null) {
        Quaternion originalRot = transform.rotation;

        transform.LookAt(targetPos, Vector3.up);
        Quaternion targetRot = transform.rotation;

        transform.rotation = originalRot;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, (speed ?? 5) * Time.deltaTime);
    }
}