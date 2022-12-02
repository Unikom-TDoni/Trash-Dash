using UnityEngine;
using UnityEngine.UI;
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

        private void Awake()
        {
            _btnConfirm.onClick.AddListener(() => Application.Quit());
            _btnExit.onClick.AddListener(() => _quitConfirmLayout.SetActive(true));
            _btnCancel.onClick.AddListener(() => _quitConfirmLayout.SetActive(default));
            _btnSetting.onClick.AddListener(() => _settingLayout.SetActive(true));
            _btnStart.onClick.AddListener(() => SceneManager.LoadScene(GameManager.Instance.Scenes.LevelSelector));
        }

        public void ShowQuitBox()
        {
            _mainMenuLayout.SetActive(false);
            _quitConfirmLayout.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}