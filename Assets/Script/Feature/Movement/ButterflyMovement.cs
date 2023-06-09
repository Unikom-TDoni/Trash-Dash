using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    public ButterflySpawner butterflySpawner;
    public Vector3 maxHeight, minHeight;
    public float flyTime;
    public Material wingsColor;
    bool goingDown, goingUp, flyAround;

    private Quaternion targetRotation;
    private float changeCounter = 0f;
    [SerializeField] float counterLimit = 3f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Renderer wingLeft, wingRight;

    void Start()
    {
        goingDown = true;
        targetRotation = Random.rotation;
        targetRotation.x = 0.0f;
        targetRotation.z = 0.0f;
        transform.rotation = targetRotation;
        wingLeft.material = wingRight.material = wingsColor;
    }

    void Update()
    {
        if (goingDown)
        {
            transform.position += Vector3.down * Time.deltaTime;

            if (transform.position.y <= minHeight.y)
            {
                goingDown = false;
                flyAround = true;
            }
        }

        if (goingUp)
        {
            transform.position += Vector3.up * Time.deltaTime;

            if (transform.position.y >= maxHeight.y)
            {
                butterflySpawner.butterflyCount--;
                Destroy(gameObject);
            }
        }

        if (flyAround)
        {
            flyTime -= Time.deltaTime;
        }

        if (flyTime <= 0)
        {
            flyAround = false;
            goingUp = true;
        }

        changeCounter += Time.deltaTime;

        if (changeCounter >= counterLimit)
        {
            targetRotation = Random.rotation;
            targetRotation.x = 0.0f;
            targetRotation.z = 0.0f;
            changeCounter = 0f;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
    }
}
