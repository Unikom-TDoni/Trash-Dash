using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUpHandler : MonoBehaviour
{
    [SerializeField] private PowerUpSO[] powerUps = new PowerUpSO[2];
    private PlayerAction playerControls;

    #region Input Callbacks
    private void Start()
    {
        powerUps = new PowerUpSO[2];
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

        for(int i = 0; i < powerUps.Length; i++)
        {
            if (powerUps[i] != null) continue;
            powerUps[i] = _powerUp;
            result = true;
            break;
        }

        return result;
    }

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
    }

    private void OnPowerUp2(InputAction.CallbackContext context)
    {
        if (powerUps[1] == null) return;
        powerUps[1].Use();
        powerUps[1] = null;
    }
    #endregion
}
