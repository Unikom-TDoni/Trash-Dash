using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshResolver : MonoBehaviour
{
    [SerializeField] NavMeshData data;

    void Start()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(data);
    }
}
