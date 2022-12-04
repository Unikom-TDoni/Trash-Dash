using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Group8.TrashDash.Core;
using UnityEngine.SceneManagement;

namespace Group8.TrashDash.MainMenu
{
    public class MainMenuLayoutController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _quitConfirmLayout = default;

        [SerializeField]
        private GameObject _mainMenuLayout = default;

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

        private PlayerAction _input;

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
           _mainMenuLayout.SetActive(true);
            _levelSelectLayout.SetActive(default);
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

            _btnSetting.onClick.AddListener(() => _settingLayout.SetActive(true));
            _btnStart.onClick.AddListener(() => {
                _levelSelectLayout.SetActive(true);
                _mainMenuLayout.SetActive(default);
            });

            _btnTutorial.onClick.AddListener(() =>
            {
                GameManager.Instance.LevelHandler.SelectLevel(default);
                SceneManager.LoadScene(GameManager.Instance.Scenes.Gameplay);
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
        }
    }
}