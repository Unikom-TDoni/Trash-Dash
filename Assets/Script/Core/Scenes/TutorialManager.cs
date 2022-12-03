using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Group8.TrashDash.Player.Pickup;
using Group8.TrashDash.TrashBin;
using Group8.TrashDash.Spawner;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] int sequence = 1;
    [SerializeField] GameObject[] tutorialText;
    [SerializeField] Transform trashSpawnRef;
    private PlayerAction playerControls;
    private PlayerPickup playerPickup;
    private TrashBinHandler trashBinHandler;
    private GameObject inventoryText;
    private PowerUpSpawner powerUpSpawner;
    private PanelUIManager panelUIManager;
    private TrashSpawner trashSpawner;
    private Vector3 trashSpawnInitialPosition;
    Coroutine spawnCoroutine;

    void Start()
    {
        playerControls = InputManager.playerAction;
        playerPickup = FindObjectOfType<PlayerPickup>();
        playerPickup.tutorialManager = this;
        trashBinHandler = FindObjectOfType<TrashBinHandler>();
        trashBinHandler.tutorialManager = this;
        powerUpSpawner = FindObjectOfType<PowerUpSpawner>();
        powerUpSpawner.enabled = false;
        panelUIManager = FindObjectOfType<PanelUIManager>();
        trashSpawner = FindObjectOfType<TrashSpawner>();
        trashSpawnInitialPosition = trashSpawnRef.position;
    }

    void Update()
    {
        if (sequence > 11)
        {
            panelUIManager.BackToMainMenu();
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (sequence == 1 || sequence == 2 || sequence == 6 || sequence == 7 || sequence == 10 || sequence == 11)
            {
                if (sequence == 2)
                {
                    StartCoroutine(SpawnSomeTrash());
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

        if (playerControls.Panel.Cancel.WasPressedThisFrame())
        {
            if (sequence == 5 || sequence == 9)
            {
                NextSequence();
            }
        }

        if (inventoryText == null)
        {
            inventoryText = GameObject.Find("InventoryTitle");
        }
        else
        {
            inventoryText.SetActive(false);
        }
    }

    public void PickupTrash()
    {
        if (sequence == 3)
        {
            NextSequence();
        }
    }

    public void OpenTrashBin()
    {
        if (sequence == 8)
        {
            NextSequence();
        }
    }

    IEnumerator SpawnSomeTrash()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i == 4)
            {
                trashSpawnRef.position = trashSpawnInitialPosition;
                trashSpawnRef.position += Vector3.back * 2;
            }

            spawnCoroutine = trashSpawner.InstantSpawn(trashSpawnRef);

            trashSpawnRef.position += Vector3.left * 2;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void NextSequence()
    {
        tutorialText[sequence - 1].SetActive(false);
        sequence++;
        tutorialText[sequence - 1].SetActive(true);
    }
}
