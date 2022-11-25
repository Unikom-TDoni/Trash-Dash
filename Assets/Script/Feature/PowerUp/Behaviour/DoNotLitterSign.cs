using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Group8.TrashDash.Module.Detector;
using Group8.TrashDash.Item.Trash;

public class DoNotLitterSign : AOEPowerUp
{
    protected override IEnumerator DoEffect()
    {
        yield return new WaitForSeconds(aoePower.interval);

        ColliderDetector.Find<Trash>(transform.position, aoePower.radius, LayerMask.GetMask("Pickup")).ForEach(
            (trash) => trash.MoveToTarget(transform));

        StartCoroutine(DoEffect());
    }
}
