using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour
{

    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time,10),transform.position.y,transform.position.z);
    }
}
