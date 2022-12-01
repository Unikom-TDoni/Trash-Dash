using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraObscure : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask layerMask;
    [SerializeField, Range(0, 1)] private float radius = .2f;

    private Transform obstruction;
    private MeshRenderer mesh;

    private Vector3[] origins = new Vector3[5];

    float maxDistance;

    private void Start()
    {
        obstruction = target;
        origins = new Vector3[5];
    }

    private void SetOrigins()
    {
        //origins = new Vector3[5];
        origins[0] = transform.position;
        origins[1] = transform.position + new Vector3(radius, 0f, 0f);
        origins[2] = transform.position - new Vector3(radius, 0f, 0f);
        origins[3] = transform.position + new Vector3(0f, radius, 0f);
        origins[4] = transform.position - new Vector3(0f, radius, 0f);
    }

    private void LateUpdate()
    {
        SetOrigins();
        foreach (var origin in origins)
        {
            maxDistance = Mathf.Sqrt((origin - target.position).sqrMagnitude);
            if (Physics.Raycast(origin, (target.position - transform.position).normalized, out var hit, maxDistance, layerMask))
            {
                if (!hit.collider.CompareTag("Player"))
                {
                    if (obstruction != hit.transform)
                    {
                        ChangeMeshMode(ShadowCastingMode.On);
                    }

                    obstruction = hit.transform;
                    ChangeMeshMode(ShadowCastingMode.ShadowsOnly);
                    break;
                }
                else
                {
                    ChangeMeshMode(ShadowCastingMode.On);
                }
            }
        }
    }

    private void ChangeMeshMode(ShadowCastingMode mode)
    {
        mesh = obstruction.GetComponent<MeshRenderer>();

        if (!mesh) return;
        mesh.shadowCastingMode = mode;
    }

    private void OnDrawGizmos()
    {
        SetOrigins();
        foreach (var origin in origins)
        {
            Gizmos.DrawRay(origin, (target.position - transform.position).normalized * Mathf.Sqrt((origin - target.position).sqrMagnitude));
        }
    }
}
