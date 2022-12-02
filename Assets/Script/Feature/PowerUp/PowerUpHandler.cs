using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Group8.TrashDash.Item.Audio;

public class PowerUpHandler : MonoBehaviour
{
    [SerializeField] private PowerUpSO[] powerUps;
    [SerializeField] private int maxSlot = 2;
    private PlayerAction playerControls;

    [SerializeField]
    private PowerUpLayoutGroupController powerUpLayoutGroupController = default;

    [SerializeField]
    private PlayerAudioController _playerAudioController = default;

    [SerializeField]
    private GameObject indicatorPanel;

    private void Awake()
    {
        powerUpLayoutGroupController.InitLayout(maxSlot);
        int i = 0;
        foreach(var item in powerUpLayoutGroupController.GetGroupItems())
        {
            item.SetIndex(++i);
        }
    }

    #region Input Callbacks
    private void Start()
    {
        powerUps = new PowerUpSO[maxSlot];
        playerControls = InputManager.playerAction;
        RegisterInputCallback();

        InitializePowerUpValues();
    }

    private void OnEnable()
    {
        RegisterInputCallback();
    }

    private void OnDisable()
    {
        UnregisterInputCallback();
    }

    private void RegisterInputCallback()
    {
        if (playerControls == null) return;
        playerControls.Gameplay.PowerUp1.performed += OnPowerUp1;
        playerControls.Gameplay.PowerUp2.performed += OnPowerUp2;
    }

    private void UnregisterInputCallback()
    {
        if (playerControls == null) return;
        playerControls.Gameplay.PowerUp1.performed -= OnPowerUp1;
        playerControls.Gameplay.PowerUp2.performed -= OnPowerUp2;
    }
    #endregion

    public bool Add(PowerUpSO _powerUp)
    {
        bool result = false;

        for (int i = 0; i < maxSlot; i++)
        {
            if (powerUps[i] != null) continue;
            powerUps[i] = _powerUp;
            powerUpLayoutGroupController.TryUpdateContent(powerUps[i], i);
            result = true;
            _playerAudioController.PlayPickupPowerupSfx();
            break;
        }

        return result;
    }

    private void Update()
    {
        if(indicatorPanel.activeSelf != (powerUpValues["trashIndicator"] == 1))
            indicatorPanel.SetActive((powerUpValues["trashIndicator"] == 1));
    }

    #region PowerUp Coroutines
    Dictionary<PowerUpSO, Coroutine> powerUpCoroutines = new Dictionary<PowerUpSO, Coroutine>();

    public void StartPowerUp(ValuePower powerUp)
    {
        if (!powerUpCoroutines.ContainsKey(powerUp)) powerUpCoroutines.Add(powerUp, null);
        if (powerUpCoroutines[powerUp] != null)
        {
            StopCoroutine(powerUpCoroutines[powerUp]);
            if (powerUpVFXs.ContainsKey(powerUp.parameterName))
                powerUpVFXs[powerUp.parameterName].GetComponent<ParticleSystem>().Stop();
        }

        powerUpCoroutines[powerUp] = StartCoroutine(PowerUpValue(powerUp.parameterName, powerUp.value, powerUp.duration, powerUp.VFX));
    }
    #endregion

    #region PowerUp Values

    // Serializeable (Agar lebih mudah ditrack di setiap script yang pakai)
    [SerializeField] private PowerUpValue[] powerUpParameters;
    public Dictionary<string, float> powerUpValues = new Dictionary<string, float>();
    // Mencegah terubahnya value akibat StopCoroutine()
    private Dictionary<string, float> initialPowerUpValues = new Dictionary<string, float>();

    private Dictionary<string, GameObject> powerUpVFXs = new Dictionary<string, GameObject>();

    public IEnumerator PowerUpValue(string parameterName, float value, float duration, GameObject vfx)
    {
        if (vfx != null)
        {
            if (!powerUpVFXs.ContainsKey(parameterName))
                powerUpVFXs.Add(parameterName, Instantiate(vfx, transform));

            powerUpVFXs[parameterName].GetComponent<ParticleSystem>().Play();
        }

        if (powerUpValues[parameterName] != value)
        initialPowerUpValues[parameterName] = powerUpValues[parameterName];

        powerUpValues[parameterName] = value;

        yield return new WaitForSeconds(duration);

        powerUpValues[parameterName] = initialPowerUpValues[parameterName];

        if (vfx != null)
        {
            powerUpVFXs[parameterName].GetComponent<ParticleSystem>().Stop();
        }
    }

    private void InitializePowerUpValues()
    {
        foreach (PowerUpValue powerUpValue in powerUpParameters)
        {
            powerUpValues.Add(powerUpValue.name, powerUpValue.value);
        }
    }
    #endregion

    #region Callbacks
    private void OnPowerUp1(InputAction.CallbackContext context)
    {
        UsePowerUp(0);
    }

    private void OnPowerUp2(InputAction.CallbackContext context)
    {
        UsePowerUp(1);
    }
    #endregion

    private void UsePowerUp(int index)
    {
        if (powerUps[index] == null) return;
        powerUps[index].Use();
        powerUps[index] = null;
        _playerAudioController.PlayUsePowerupSfx();
        powerUpLayoutGroupController.TryUpdateContent(powerUps[index], index);
    }

}
