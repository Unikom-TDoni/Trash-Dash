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
using Group8.TrashDash.TrashBin;
using Group8.TrashDash.Spawner;

public class PanelUIManager : MonoBehaviour
{
    public TMP_Text countdownText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausedPanel;
    [SerializeField] GameObject pausedButton;
    [SerializeField] TrashSpawner trashSpawner;

    private AudioSource _audioSource = default;
    private AudioSource _audioSourceStar = default;

    [SerializeField]
    private AudioSource _bgmAudioSource = default;

    [SerializeField]
    private AudioClip _gameOverAudioClip = default;

    [SerializeField]
    private CinemachineVirtualCamera cineCam;
    [SerializeField]
    private float camFOVValue = 45;
    [SerializeField]
    private float camChangeDuration = 3f;

    [SerializeField]
    private GameObject _inventory;

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
        foreach (var item in GetComponents<AudioSource>())
        {
            if (item.clip is null) _audioSource = item;
            else _audioSourceStar = item;
        }
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
        InputManager.playerAction.Gameplay.Disable();
        _inventory.SetActive(false);
        InputManager.playerAction.Panel.Disable();

        trashSpawner.StopAllCoroutines();
        StartCoroutine(CamFOVChange());
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

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        GameManager.Instance.LevelHandler.IncreaseLevel();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameManager.Instance.Scenes.MainMenu);
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

    public void PlayStarAudio() =>
        _audioSourceStar.Play();

    private void AfterCam()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        _bgmAudioSource.Stop();
        PlayAudioClip(_gameOverAudioClip);
    }

    private void PlayAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
