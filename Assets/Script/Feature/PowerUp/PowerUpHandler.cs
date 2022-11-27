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
    Dictionary<string, Coroutine> powerUpCoroutines = new Dictionary<string, Coroutine>();

    public void StartPowerUp(string powerUpName, IEnumerator enumerator)
    {
        if (!powerUpCoroutines.ContainsKey(powerUpName)) powerUpCoroutines.Add(powerUpName, null);
        if (powerUpCoroutines[powerUpName] != null) StopCoroutine(powerUpCoroutines[powerUpName]);

        powerUpCoroutines[powerUpName] = StartCoroutine(enumerator);
    }
    #endregion

    #region PowerUp Values

    // Serializeable (Agar lebih mudah ditrack di setiap script yang pakai)
    [SerializeField] private PowerUpValue[] powerUpParameters;
    public Dictionary<string, float> powerUpValues = new Dictionary<string, float>();
    // Mencegah terubahnya value akibat StopCoroutine()
    private Dictionary<string, float> initialPowerUpValues = new Dictionary<string, float>();

    public IEnumerator PowerUpMultiply(string parameterName, float multiplier, float duration)
    {
        if (powerUpValues[parameterName] != multiplier)
        initialPowerUpValues[parameterName] = powerUpValues[parameterName];

        powerUpValues[parameterName] = multiplier;

        yield return new WaitForSeconds(duration);

        powerUpValues[parameterName] = initialPowerUpValues[parameterName];
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
