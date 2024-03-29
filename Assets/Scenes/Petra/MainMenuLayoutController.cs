using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Group8.TrashDash.Core;
using UnityEngine.SceneManagement;
using Group8.TrashDash.Setting;

namespace Group8.TrashDash.MainMenu
{
    public class MainMenuLayoutController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _quitConfirmLayout = default;

        [SerializeField]
        private GameObject _mainMenuLayout = default;

        [SerializeField]
        private GameObject _creditLayout = default;

        [SerializeField]
        private GameObject _levelSelectLayout = default;

        [SerializeField]
        private GameObject _settingLayout = default;

        [SerializeField]
        private Button _btnExit = default;

        [SerializeField]
        private Button _btnStart = default;

        [SerializeField]
        private Button _btnSetting = default;

        [SerializeField]
        private Button _btnConfirm = default;

        [SerializeField]
        private Button _btnCancel = default;

        [SerializeField]
        private Button _btnTutorial = default;

        [SerializeField]
        private Button _btnCredit = default;

        [SerializeField]
        private Button _btnCloseCredit = default;

        [SerializeField]
        private SettingHandler _settingHandler = default;

        [SerializeField]
        private Button _btnExitLevel = default;

        private PlayerAction _input = default;

        private void OnEnable()
        {
            _input = new();
            _input.Enable();
            _input.Panel.Cancel.performed += PerformedEvent;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Panel.Cancel.performed -= PerformedEvent;
        }

        private void PerformedEvent(InputAction.CallbackContext context)
        {
            if (_mainMenuLayout.activeInHierarchy)
            {
                _quitConfirmLayout.SetActive(true);
                _mainMenuLayout.SetActive(false);
                return;
            }

           _mainMenuLayout.SetActive(true);
            _creditLayout.SetActive(default);
            _levelSelectLayout.SetActive(default);
            _quitConfirmLayout.SetActive(default);
            if (_settingLayout.activeInHierarchy)
            {
                _settingHandler.ResetValue();
                _settingLayout.SetActive(default);
            }
        }

        private void Awake()
        {
            _btnConfirm.onClick.AddListener(() => Application.Quit());
            _btnExit.onClick.AddListener(() =>
            {
                _mainMenuLayout.SetActive(default);
                _quitConfirmLayout.SetActive(true);
            });

            _btnCancel.onClick.AddListener(() =>
            {
                _mainMenuLayout.SetActive(true);
                _quitConfirmLayout.SetActive(default);
            });

            _btnSetting.onClick.AddListener(() =>
            {
                _settingLayout.SetActive(true);
                _mainMenuLayout.SetActive(default);
                _settingHandler.LastBgmVolume = GameManager.Instance.SettingDataPersistence.PersistenceData.BgmVolume;
            });

            _btnStart.onClick.AddListener(() => 
            {
                _levelSelectLayout.SetActive(true);
                _mainMenuLayout.SetActive(default);
            });

            _btnTutorial.onClick.AddListener(() =>
            {
                GameManager.Instance.LevelHandler.SelectLevel(default);
                SceneManager.LoadScene(GameManager.Instance.Scenes.Gameplay);
            });

            _btnCredit.onClick.AddListener(() => {
                _mainMenuLayout.SetActive(default);
                _creditLayout.SetActive(true);
            });

            _btnCloseCredit.onClick.AddListener(() =>
            {
                _mainMenuLayout.SetActive(true);
                _creditLayout.SetActive(default);
            });

            _btnExitLevel.onClick.AddListener(() =>
            {
                _mainMenuLayout.SetActive(true);
                _levelSelectLayout.SetActive(default);
            });

            _btnStart.interactable = GameManager.Instance.LevelHandler.IsPlayModeEnable();
        }

        private void OnDestroy()
        {
            _btnExit.onClick.RemoveAllListeners();
            _btnStart.onClick.RemoveAllListeners();
            _btnCancel.onClick.RemoveAllListeners();
            _btnSetting.onClick.RemoveAllListeners();
            _btnConfirm.onClick.RemoveAllListeners();
            _btnExitLevel.onClick.RemoveAllListeners();
        }
    }
}