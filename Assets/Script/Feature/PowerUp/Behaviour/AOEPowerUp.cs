using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEPowerUp : MonoBehaviour
{
    public AOEPower aoePower;
    private float timer = 0;

    protected virtual void Start()
    {
        StartCoroutine(DoEffect());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > aoePower.duration)
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    protected virtual IEnumerator DoEffect()
    {
        yield return new WaitForSeconds(aoePower.interval);
    }

    private void OnDrawGizmos()
    {
        if (!aoePower) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aoePower.radius);
    }
}
