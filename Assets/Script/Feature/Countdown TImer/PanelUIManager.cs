using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Group8.TrashDash.TimeManager;
using System;

public class PanelUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text countdownText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausedPanel;
    [SerializeField] GameObject pausedButton;

    private void OnEnable()
    {
        if(InputManager.playerAction != null)
            InputManager.playerAction.Gameplay.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        InputManager.playerAction.Gameplay.Pause.performed -= OnPause;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        pausedPanel.SetActive(false);

        if (InputManager.playerAction != null)
            InputManager.playerAction.Gameplay.Pause.performed += OnPause;
    }

    public void GameEnd()
    {
        if (gameOverPanel.activeSelf) return;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        pausedButton.SetActive(false);
    }

    public void OnTimeUpdate(float _time)
    {
        TimeSpan time = TimeSpan.FromSeconds(_time);
        string displayTime = time.ToString(@"hh\:mm");
        countdownText.text = displayTime;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        PauseGame();
        pausedPanel.SetActive(true);
        pausedButton.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        InputManager.ToggleActionMap(InputManager.playerAction.Panel);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        InputManager.ToggleActionMap(InputManager.playerAction.Gameplay);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
