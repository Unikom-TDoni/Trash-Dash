using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Group8.TrashDash.Module.Detector;
using Group8.TrashDash.Item.Trash;
using Group8.TrashDash.Score;

public class DoNotLitterSign : AOEPowerUp
{
    ScoreManager scoreManager;
    protected override void Start()
    {
        base.Start();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    protected override IEnumerator DoEffect()
    {
        yield return new WaitForSeconds(aoePower.interval);

        List<Trash> trashes = ColliderDetector.Find<Trash>(transform.position, aoePower.radius, LayerMask.GetMask("Pickup"));

        trashes.ForEach((trash) => trash.MoveToTarget(transform));

        StartCoroutine(DoEffect());
    }

    private void OnTriggerEnter(Collider other)
    {
        Trash trash = other.GetComponent<Trash>();
        if (!trash) return;

        trash.Release();

        if (!scoreManager) return;
        scoreManager.UpdateScore(ScoreState.Collect);
        scoreManager.UpdateScore(ScoreState.CorrectNoCombo);
    }
}
