using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Group8.TrashDash.Module.Detector;
using Group8.TrashDash.Item.Trash;
using Group8.TrashDash.Score;

public class DoNotLitterSign : AOEPowerUp
{
    ScoreManager scoreManager;

    [SerializeField]
    private Animator animator;

    [SerializeField, Range(0, 1)]
    private float animWait = .2f;

    private float animWaitDuration;

    protected override void Start()
    {
        base.Start();
        scoreManager = FindObjectOfType<ScoreManager>();

        animWaitDuration = animWait * aoePower.interval;
    }

    protected override IEnumerator DoEffect()
    {
        AnimClose();

        while (true)
        {
            yield return new WaitForSeconds(aoePower.interval);

            List<Trash> trashes = ColliderDetector.Find<Trash>(transform.position, aoePower.radius, LayerMask.GetMask("Pickup"));

            trashes.ForEach((trash) => trash.MoveToTarget(transform));

            StopCoroutine(AnimOpenClose());
            StartCoroutine(AnimOpenClose());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Trash trash = other.GetComponent<Trash>();
        if (!trash) return;

        trash.Release();

        if (!scoreManager) return;
        scoreManager.UpdateScore(ScoreState.Collect);
        //scoreManager.UpdateScore(ScoreState.CorrectNoCombo);
    }

    private IEnumerator AnimOpenClose()
    {
        AnimOpen();
        yield return new WaitForSeconds(animWaitDuration);
        AnimClose();
    }

    public void AnimOpen()
    {
        if (animator == null) return;
        animator.SetBool("isOpening", true);
        animator.SetBool("isClosing", false);
    }

    public void AnimClose()
    {
        if (animator == null) return;
        animator.SetBool("isClosing", true);
        animator.SetBool("isOpening", false);
    }
}
