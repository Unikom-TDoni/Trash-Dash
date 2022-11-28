using Group8.TrashDash.Level;
using Lnco.Unity.Module.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUpHandler : MonoBehaviour
{
    [SerializeField] private PowerUpSO[] powerUps;
    [SerializeField] private int maxSlot = 2;
    private PlayerAction playerControls;

    [SerializeField]
    private PowerUpLayoutGroupController powerUpLayoutGroupController = default;
    private void Awake()
    {
        powerUpLayoutGroupController.InitLayout(maxSlot);
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

        for(int i = 0; i < maxSlot; i++)
        {
            if (powerUps[i] != null) continue;
            powerUps[i] = _powerUp;
            powerUpLayoutGroupController.TryUpdateContent(powerUps[i], i);
            result = true;
            break;
        }

        return result;
    }

    #region PowerUp Coroutines
    Dictionary<PowerUpSO, Coroutine> powerUpCoroutines = new Dictionary<PowerUpSO, Coroutine>();

    public void StartPowerUp(MultiplyPower powerUp)
    {
        if (!powerUpCoroutines.ContainsKey(powerUp)) powerUpCoroutines.Add(powerUp, null);
        if (powerUpCoroutines[powerUp] != null)
        {
            StopCoroutine(powerUpCoroutines[powerUp]);
            if (powerUpVFXs.ContainsKey(powerUp.parameterName))
                powerUpVFXs[powerUp.parameterName].GetComponent<ParticleSystem>().Stop();
        }

        powerUpCoroutines[powerUp] = StartCoroutine(PowerUpMultiply(powerUp.parameterName, powerUp.multiplier, powerUp.duration, powerUp.VFX));
    }
    #endregion

    #region PowerUp Values

    // Serializeable (Agar lebih mudah ditrack di setiap script yang pakai)
    [SerializeField] private PowerUpValue[] powerUpParameters;
    public Dictionary<string, float> powerUpValues = new Dictionary<string, float>();
    // Mencegah terubahnya value akibat StopCoroutine()
    private Dictionary<string, float> initialPowerUpValues = new Dictionary<string, float>();

    private Dictionary<string, GameObject> powerUpVFXs = new Dictionary<string, GameObject>();

    public IEnumerator PowerUpMultiply(string parameterName, float multiplier, float duration, GameObject vfx)
    {
        if (vfx != null)
        {
            if (!powerUpVFXs.ContainsKey(parameterName))
                powerUpVFXs.Add(parameterName, Instantiate(vfx, transform));

            powerUpVFXs[parameterName].GetComponent<ParticleSystem>().Play();
        }

        if (powerUpValues[parameterName] != multiplier)
        initialPowerUpValues[parameterName] = powerUpValues[parameterName];

        powerUpValues[parameterName] = multiplier;

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
        if (powerUps[0] == null) return;
        powerUps[0].Use();
        powerUps[0] = null;
        powerUpLayoutGroupController.TryUpdateContent(powerUps[0], 0);
    }

    private void OnPowerUp2(InputAction.CallbackContext context)
    {
        if (powerUps[1] == null) return;
        powerUps[1].Use();
        powerUps[1] = null;
        powerUpLayoutGroupController.TryUpdateContent(powerUps[1], 1);
    }
    #endregion
}
