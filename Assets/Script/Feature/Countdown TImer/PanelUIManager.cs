using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Group8.TrashDash.TimeManager;
using System;
using Group8.TrashDash.Core;
using Cinemachine;

public class PanelUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text countdownText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausedPanel;
    [SerializeField] GameObject pausedButton;

    private AudioSource _audioSource = default;

    [SerializeField]
    private AudioClip _gameOverAudioClip = default;

    [SerializeField]
    private AudioClip _starAudioClip = default;

    [SerializeField]
    private Button _btnPauseToMainMenu = default;

    [SerializeField]
    private CinemachineVirtualCamera cineCam;
    [SerializeField]
    private float camFOVValue = 45;
    [SerializeField]
    private float camChangeDuration = 3f;

    private void OnEnable()
    {
        if (InputManager.playerAction == null) return;

        InputManager.playerAction.Gameplay.Pause.performed += OnPause;
        InputManager.playerAction.Panel.Cancel.performed += OnResume;
    }

    private void OnDisable()
    {
        InputManager.playerAction.Gameplay.Pause.performed -= OnPause;
        InputManager.playerAction.Panel.Cancel.performed -= OnResume;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _btnPauseToMainMenu.onClick.AddListener(() => SceneManager.LoadScene(GameManager.Instance.Scenes.MainMenu));
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        pausedPanel.SetActive(false);

        if (InputManager.playerAction == null) return;
        
        InputManager.playerAction.Gameplay.Pause.performed += OnPause;
        InputManager.playerAction.Panel.Cancel.performed += OnResume;
    }

    public void GameEnd()
    {
        if (gameOverPanel.activeSelf) return;
        StartCoroutine(CamFOVChange());
        //pausedButton.SetActive(false);
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
        //pausedButton.SetActive(false);
    }
    
    private void OnResume(InputAction.CallbackContext context)
    {
        ResumeGame();
        pausedPanel.SetActive(false);
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

    private IEnumerator CamFOVChange()
    {
        float step = Mathf.Abs(cineCam.m_Lens.FieldOfView - camFOVValue) / camChangeDuration;
        float t = 0;
        while (t < camChangeDuration) {
            t += Time.deltaTime;
            cineCam.m_Lens.FieldOfView = Mathf.MoveTowards(cineCam.m_Lens.FieldOfView, camFOVValue, Time.deltaTime * step);
            yield return new WaitForFixedUpdate();
        }

        cineCam.m_Lens.FieldOfView = camFOVValue;

        AfterCam();
    }

    private void AfterCam()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        PlayAudioClip(_gameOverAudioClip);
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (_audioSource.clip == clip) return;
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
