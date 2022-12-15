using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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

        [SerializeField]
        private TMP_Text _txtApplySettingDesc = default;

        [SerializeField]
        private AudioSource _bgmAudioSource = default;

        private AudioSource _audioSource = default;

        private float _lastBgmVolume = default;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            InitDropdownOptions();
            InitUiValue();
            InitUiEvent();
        }

        private void InitUiEvent()
        {
            Coroutine coroutine = default;

            _sliderSfx.onValueChanged.AddListener(value =>
            {
                if(coroutine is not null) StopCoroutine(coroutine);
                coroutine = StartCoroutine(CheckSound(value));
                ChangeApplyTextButton(!value.Equals(GameManager.Instance.SettingDataPersistence.PersistenceData.SfxVolume));
            });

            _sliderBgm.onValueChanged.AddListener(value =>
            {
                ChangeApplyTextButton(!value.Equals(GameManager.Instance.SettingDataPersistence.PersistenceData.BgmVolume));
                _lastBgmVolume = value;
                GameManager.Instance.SettingDataPersistence.ChangeAudioMixerBgm(value);
            });

            _dropdownDisplayMode.onValueChanged.AddListener(value =>
                ChangeApplyTextButton(!value.Equals(GameManager.Instance.SettingDataPersistence.PersistenceData.DisplayMode))
            );

            _dropdownResolution.onValueChanged.AddListener(value =>
                ChangeApplyTextButton(!value.Equals(GameManager.Instance.SettingDataPersistence.PersistenceData.Resolution))
            );

            _toggleVsync.onValueChanged.AddListener(value =>
                ChangeApplyTextButton(!value.Equals(GameManager.Instance.SettingDataPersistence.PersistenceData.IsVsyncOn))
            );

            _btnApplySetting.onClick.AddListener(ApplySetting);
        }

        private void ApplySetting()
        {
            _settingLayoutObj.SetActive(default);
            _mainMenuLayoutObj.SetActive(true);
            if (_txtApplySettingDesc.text.Equals("CLOSE")) return;
            GameManager.Instance.SettingDataPersistence.Save(new(
                _toggleVsync.isOn,
                _sliderSfx.value,
                _sliderBgm.value,
                _dropdownResolution.value,
                _dropdownDisplayMode.value
            ));
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

        public void ResetValue()
        {
            var persistenceData = GameManager.Instance.SettingDataPersistence.PersistenceData;
            ChangeApplyTextButton(default);
            _sliderSfx.value = persistenceData.SfxVolume;
            _sliderBgm.value = persistenceData.BgmVolume;
            _toggleVsync.isOn = persistenceData.IsVsyncOn;
            _dropdownResolution.value = persistenceData.Resolution;
            _dropdownDisplayMode.value = persistenceData.DisplayMode;
            GameManager.Instance.SettingDataPersistence.ChangeAudioMixerBgm(_lastBgmVolume);
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

        private IEnumerator CheckSound(float value)
        {
            _bgmAudioSource.Pause();
            _audioSource.volume = value;
            _audioSource.Play();
            yield return new WaitForSeconds(1.5f);
            _bgmAudioSource.UnPause();
        }

        private void ChangeApplyTextButton(bool isChange)
        {
            if (isChange) _txtApplySettingDesc.text = "APPLY";
            else _txtApplySettingDesc.text = "CLOSE";
        }
    }
}