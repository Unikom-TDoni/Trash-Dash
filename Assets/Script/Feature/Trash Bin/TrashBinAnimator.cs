using Group8.TrashDash.Player.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinAnimator : MonoBehaviour
{
    [SerializeField] private Animator trashBinAnim;

    private Transform playerTransform;
    private float openDistance = 4f;

    private bool checkAnim = false;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        playerTransform = player.transform;
        openDistance = player.GetComponent<PlayerInteraction>().interactRadius + 1f;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        if (Vector3.Distance(transform.position, playerTransform.position) < openDistance)
        {
            if (!checkAnim)
            {
                AnimOpen();
                checkAnim = true;
            }
        }
        else
        {
            if (checkAnim)
            {
                AnimClose();
                checkAnim = false;
            }
        }
    }

    public void AnimOpen()
    {
        if (trashBinAnim == null) return;
        trashBinAnim.SetBool("isOpening", true);
        trashBinAnim.SetBool("isClosing", false);
    }

    public void AnimClose()
    {
        if (trashBinAnim == null) return;
        trashBinAnim.SetBool("isClosing", true);
        trashBinAnim.SetBool("isOpening", false);
    }
}
