namespace Group8.TrashDash.Level
{
    public struct SettingEntity
    {
        public bool IsVsyncOn;

        public float SfxVolume;

        public float BgmVolume;

        public int Resolution;

        public int DisplayMode;

        public SettingEntity(bool isVsyncOns, float sfxVolume, float bgmVolume, int resolution, int displayMode)
        {
            IsVsyncOn = isVsyncOns;
            SfxVolume = sfxVolume;
            BgmVolume = bgmVolume;
            Resolution = resolution;
            DisplayMode = displayMode;
        }
    }
}
