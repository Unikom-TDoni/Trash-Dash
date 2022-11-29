using UnityEngine;

public static class Utility {
    public static void LerpLookTowardsTarget(Transform transform, Vector3 targetPos, float? speed = null) {
        Quaternion originalRot = transform.rotation;

        transform.LookAt(targetPos, Vector3.up);
        Quaternion targetRot = transform.rotation;

        transform.rotation = originalRot;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, (speed ?? 5) * Time.deltaTime);
    }

    public static void LerpLookTowardsDirection(Transform transform, Vector3 direction, float? speed = null) {
        Quaternion originalRot = transform.rotation;

        transform.LookAt(transform.position + direction, Vector3.up);
        Quaternion targetRot = transform.rotation;

        transform.rotation = originalRot;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, (speed ?? 5) * Time.deltaTime);
    }
}