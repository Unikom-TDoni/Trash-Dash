using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingFadingText : MonoBehaviour
{
    public GameObject correct, wrong;
    [SerializeField] float duration;

    void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
