using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraObscure : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius = .2f;

    private Transform obstruction;
    private MeshRenderer mesh;

    private Vector3[] origins;

    private void Start()
    {
        obstruction = target;
        origins = new Vector3[5];
    }

    private void SetOrigins()
    {
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
            if (Physics.Raycast(origin, target.position - transform.position, out var hit, (target.position - transform.position).sqrMagnitude, layerMask))
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
}
