using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PanelUIManager : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime = 185f; //3menit

    [SerializeField] TMP_Text countdownText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausedPanel;
    [SerializeField] GameObject pausedButton;

    private void OnEnable()
    {
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
        currentTime = startingTime;
    }
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime > 0) return;
        if (gameOverPanel.activeSelf) return;

        currentTime = 0;
        // Game berhenti dan muncul game over panel
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        pausedButton.SetActive(false);
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
