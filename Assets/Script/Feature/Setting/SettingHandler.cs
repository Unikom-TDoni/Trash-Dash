using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Group8.TrashDash.Core;

namespace Group8.TrashDash.Setting
{
    public sealed class SettingHandler : MonoBehaviour
    {
        [SerializeField]
        private Slider _sliderSfx = default;

        [SerializeField]
        private Slider _sliderBgm = default;

        [SerializeField]
        private Toggle _toggleVsync = default;

        [SerializeField]
        private TMP_Dropdown _dropdownResolution = default;

        [SerializeField]
        private TMP_Dropdown _dropdownDisplayMode = default;

        [SerializeField]
        private Button _btnApplySetting = default;

        [SerializeField]
        private GameObject _settingLayoutObj = default;

        [SerializeField]
        private GameObject _mainMenuLayoutObj = default;

        private void Awake()
        {
            InitDropdownOptions();
            InitUiValue();
            InitUiEvent();
        }

        private void InitUiEvent()
        {
            _sliderSfx.onValueChanged.AddListener(value =>
            {

            });

            _sliderBgm.onValueChanged.AddListener(value =>
            {

            });

            _btnApplySetting.onClick.AddListener(ApplySetting);
        }

        private void ApplySetting()
        {
            GameManager.Instance.SettingDataPersistence.Save(new(
                _toggleVsync.isOn,
                _sliderSfx.value,
                _sliderBgm.value,
                _dropdownResolution.value,
                _dropdownDisplayMode.value
            ));
            _settingLayoutObj.SetActive(default);
            _mainMenuLayoutObj.SetActive(true);
        }

        private void InitUiValue()
        {
            var persistenceData = GameManager.Instance.SettingDataPersistence.PersistenceData;
            _sliderSfx.value = persistenceData.SfxVolume;
            _sliderBgm.value = persistenceData.BgmVolume;
            _toggleVsync.isOn = persistenceData.IsVsyncOn;
            _dropdownResolution.value = persistenceData.Resolution;
            _dropdownDisplayMode.value = persistenceData.DisplayMode;
        }

        private void InitDropdownOptions()
        {
            foreach (var item in GameManager.Instance.SettingDataPersistence.DefaultResolutionOptions)
                _dropdownResolution.options.Add(new($"{item.Width} X {item.Height}"));

            foreach (var item in GameManager.Instance.SettingDataPersistence.DefaultDisplayModeOptions)
            {
                var option = string.Empty;
                switch (item)
                {
                    case FullScreenMode.FullScreenWindow:
                        option = "Fullscreen";
                        break;
                    case FullScreenMode.MaximizedWindow:
                        option = "Maximized Window";
                        break;
                    case FullScreenMode.Windowed:
                        option = "Windowed";
                        break;
                    case FullScreenMode.ExclusiveFullScreen:
                        option = "Exclusive FullScreen";
                        break;
                }
                _dropdownDisplayMode.options.Add(new(option));
            }
        }
    }
}