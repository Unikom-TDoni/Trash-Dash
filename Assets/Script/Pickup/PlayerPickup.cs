using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Parameters")]
    public float pickUpRadius;
    [Range(0, 360)]
    public float pickUpAngle;

    public LayerMask targetMask;

    public List<GameObject> takenObjects;

    public Vector3 DirectionFromAngle(float angle, bool globalAngle)
    {
        if (!globalAngle) angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    private void FindTargets()
    {
        takenObjects.Clear();
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, pickUpRadius, targetMask);

        foreach(Collider targetCollider in targetsInRadius)
        {
            Transform target = targetCollider.transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, directionToTarget) < (pickUpAngle / 2))
            {
                takenObjects.Add(target.gameObject);
            }
        }
    }

    public void Pickup()
    {
        FindTargets();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
