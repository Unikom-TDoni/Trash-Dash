using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelUIManager : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime = 185f; //3menit

    [SerializeField] TMP_Text countdownText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausedPanel;
    [SerializeField] GameObject pausedButton;

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

        if (currentTime <= 0)
        {
            currentTime = 0;
            // Game berhenti dan muncul game over panel
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            pausedButton.SetActive(false);

        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ReloadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
