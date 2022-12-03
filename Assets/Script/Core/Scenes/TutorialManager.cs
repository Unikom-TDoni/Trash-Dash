using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Group8.TrashDash.Player.Pickup;

public class TutorialManager : MonoBehaviour
{
    private PlayerAction playerControls;
    private PlayerPickup playerPickup;
    [SerializeField] int sequence = 1;
    [SerializeField] GameObject[] tutorialText;

    void Start()
    {
        playerControls = InputManager.playerAction;
        playerPickup = GameObject.FindWithTag("Player").GetComponent<PlayerPickup>();
        playerPickup.tutorialManager = this;
    }

    void Update()
    {
        if (sequence > 11)
        {
            // TEMPORARY : Kembali ke level select
            Debug.Log("Kembali ke level select");
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (sequence == 1 || sequence == 2 || sequence == 6 || sequence == 7 || sequence == 10 || sequence == 11)
            {
                if (sequence == 2)
                {
                    // TEMPORARY : Spawn beberapa sampah dari semua tipe
                    Debug.Log("Spawn beberapa sampah dari semua tipe");
                }
                NextSequence();
            }
        }

        if (playerControls.Gameplay.Inventory.WasPressedThisFrame())
        {
            if (sequence == 4)
            {
                NextSequence();
            }
        }

        if (playerControls.Gameplay.Pause.WasPressedThisFrame())
        {
            if (sequence == 5 || sequence == 9)
            {
                NextSequence();
            }
        }

        if (playerControls.Gameplay.Interact.WasPressedThisFrame())
        {
            if (sequence == 8)
            {
                NextSequence();
            }
        }



        // TEMPORARY (DELETE LATER) - START
        if (playerControls.Gameplay.Pickup.WasPressedThisFrame())
        {
            if (sequence == 3)
            {
                NextSequence();
            }
        }
        // TEMPORARY (DELETE LATER) - END
    }

    public void PickupTrash()
    {
        if (sequence == 3)
        {
            NextSequence();
        }
    }

    private void NextSequence()
    {
        tutorialText[sequence - 1].SetActive(false);
        sequence++;
        tutorialText[sequence - 1].SetActive(true);
    }
}
