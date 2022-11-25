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
