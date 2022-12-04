using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Group8.TrashDash.Player.Pickup;
using Group8.TrashDash.TrashBin;
using Group8.TrashDash.Spawner;
using Group8.TrashDash.TimeManager;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] int sequence = 1;
    [SerializeField] GameObject[] tutorialText;
    [SerializeField] Transform trashSpawnRef;
    private PlayerAction playerControls;
    private PlayerPickup playerPickup;
    private TrashBinHandler trashBinHandler;
    private GameObject inventoryText;
    private PanelUIManager panelUIManager;
    private TrashSpawner trashSpawner;
    private TimeManager timeManager;
    private LightingManager lightingManager;
    private Light directionalLight;
    [SerializeField] private LightingPreset lightningPreset;
    private TMP_Text countdownTimerText;

    private Vector3 trashSpawnInitialPosition;
    Coroutine spawnCoroutine;

    void Start()
    {
        playerControls = InputManager.playerAction;
        playerControls.Gameplay.Disable();

        playerPickup = FindObjectOfType<PlayerPickup>();
        playerPickup.tutorialManager = this;
        trashBinHandler = FindObjectOfType<TrashBinHandler>();
        trashBinHandler.tutorialManager = this;
        panelUIManager = FindObjectOfType<PanelUIManager>();
        trashSpawner = FindObjectOfType<TrashSpawner>();
        trashSpawnInitialPosition = trashSpawnRef.position;

        timeManager = FindObjectOfType<TimeManager>();
        timeManager.enabled = false;
        lightingManager = FindObjectOfType<LightingManager>();
        lightingManager.enabled = false;
        directionalLight = GameObject.Find("Sun").GetComponent<Light>();
        countdownTimerText = GameObject.Find("Countdown Timer Text").GetComponent<TMP_Text>();
        countdownTimerText.text = "08:00";
        var currentTime = 8 * 3600;
        var timeOfDay = (currentTime / timeManager.TimePerDay) % timeManager.TimePerDay;
        RenderSettings.ambientLight = lightningPreset.AmbientColor.Evaluate(timeOfDay);
        RenderSettings.fogColor = lightningPreset.FogColor.Evaluate(timeOfDay);
        if (directionalLight == null) return;
        directionalLight.color = lightningPreset.DirectionalColor.Evaluate(timeOfDay);
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timeOfDay * 360f) - 90f, 170f, 0f));
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
                if (sequence == 1)
                {
                    playerControls.Gameplay.Move.Enable();
                    playerControls.Gameplay.Sprint.Enable();
                }
                if (sequence == 2)
                {
                    playerControls.Gameplay.Pickup.Enable();
                    StartCoroutine(SpawnSomeTrash());
                }
                if(sequence == 7)
                {
                    playerControls.Gameplay.Interact.Enable();
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
            if (sequence <= 7)
            {
                playerControls.Gameplay.PowerUp1.Disable();
                playerControls.Gameplay.PowerUp2.Disable();
                playerControls.Gameplay.Interact.Disable();
            }
            
            if(sequence <= 9)
            {
                playerControls.Gameplay.PowerUp1.Disable();
                playerControls.Gameplay.PowerUp2.Disable();
            }

            if (sequence == 5)
            {
                InputManager.ToggleActionMap(playerControls.Gameplay);
                NextSequence();
            }
            if(sequence == 9)
            {
                InputManager.ToggleActionMap(playerControls.Gameplay);
                NextSequence();
                playerControls.Gameplay.PowerUp1.Enable();
                playerControls.Gameplay.PowerUp2.Enable();
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
            playerControls.Gameplay.Inventory.Enable();
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
        if (sequence > tutorialText.Length) return;
        tutorialText[sequence - 1].SetActive(true);
    }
}
