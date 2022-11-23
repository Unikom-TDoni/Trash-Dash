using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.Interactable
{
    using Module.TransformMod;
    using Player.Interaction;
    public class InteractNPC : MonoBehaviour, IInteractable
    {
        [SerializeField] private float rotateSpeed = 100;
        Animator anim;
        private Vector3 originalLookPos;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            originalLookPos = transform.position + transform.forward;
        }

        public void Interact(GameObject other)
        {
            anim.SetBool("isTalking", true);
            StartCoroutine(TransformModule.FaceTarget(transform, other.transform.position, rotateSpeed));
        }

        public void ExitInteract()
        {
            anim.SetBool("isTalking", false);
            StartCoroutine(TransformModule.FaceTarget(transform, originalLookPos, rotateSpeed));
        }
    }
}