using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float speed;
    public bool movingRight;

    [SerializeField] Renderer carRenderer;
    [SerializeField] Material[] carColors;

    void Awake()
    {
        int randomColor = Random.Range(0, carColors.Length);
        carRenderer.material = carColors[randomColor];

        if (Random.value > 0.5f)
        {
            speed *= 1.25f;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
