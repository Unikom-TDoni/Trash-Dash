using System;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Audio;
using Group8.TrashDash.Level;

namespace Group8.TrashDash.Setting
{
    [Serializable]
    public sealed class SettingDataPersistence
    {
        private const string DataKey = "KEY_#$Setting";

        [SerializeField]
        private AudioMixer _gameAudioMixer = default;

        [field:SerializeField]
        public Resolution[] DefaultResolutionOptions { get; private set; } = default;

        [field: SerializeField]
        public FullScreenMode[] DefaultDisplayModeOptions { get; private set; } = default;

        public SettingEntity PersistenceData { get; private set; } = new SettingEntity(true, .5f, .5f, 2, default);

        public void OnStart()
        {
            Load();
            ApplyDisplaySetting();
            ApplyAudioMixerSetting();
        }

        public void Save(SettingEntity settingEntity)
        {
            PersistenceData = settingEntity;
            ApplyDisplaySetting();
            ApplyAudioMixerSetting();
            PlayerPrefs.SetString(DataKey, JsonConvert.SerializeObject(PersistenceData, Formatting.Indented));
        }

        private void ApplyDisplaySetting()
        {
            var resolution = DefaultResolutionOptions[PersistenceData.Resolution];
            Screen.SetResolution(resolution.Width, resolution.Height, DefaultDisplayModeOptions[PersistenceData.DisplayMode]);
            QualitySettings.vSyncCount = PersistenceData.IsVsyncOn ? 1 : default;
        }

        private void ApplyAudioMixerSetting()
        {
            _gameAudioMixer.SetFloat("BGM", Mathf.Log10(PersistenceData.BgmVolume) * 20);
            _gameAudioMixer.SetFloat("SFX", Mathf.Log10(PersistenceData.SfxVolume) * 20);
        }

        private void Load()
        {
            var json = PlayerPrefs.GetString(DataKey);
            if (string.IsNullOrEmpty(json)) return;
            PersistenceData = JsonConvert.DeserializeObject<SettingEntity>(json);
        }
    }
}
