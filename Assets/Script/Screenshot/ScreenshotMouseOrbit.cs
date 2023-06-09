using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenshotMouseOrbit : MonoBehaviour
{
    [Header("Main Settings")]
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float distanceMin = .5f;
    public float distanceMax = 15f;
    //private Rigidbody rigidbody;
    float x = 0.0f;
    float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //rigidbody = GetComponent<Rigidbody>();

        // Make the rigidbody not change rotation
        //if (rigidbody != null)
        //{
        //    rigidbody.freezeRotation = true;
        //}
    }

    void LateUpdate()
    {
        if (target)
        {
            x += Mouse.current.delta.x.ReadValue() * xSpeed * distance * 0.02f;
            y -= Mouse.current.delta.y.ReadValue() * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
        {
            angle += 360F;
        }
        if (angle > 360F)
        {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
