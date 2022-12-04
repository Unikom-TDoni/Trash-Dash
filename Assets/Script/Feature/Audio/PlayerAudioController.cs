using UnityEngine;

namespace Group8.TrashDash.Item.Audio
{
    public sealed class PlayerAudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _usePowerUp = default;

        [SerializeField]
        private AudioClip _pickupPowerUpAudioClip = default;

        [SerializeField]
        private AudioClip _pickupAudioClip = default;

        [SerializeField]
        private AudioClip _dropAudioClip = default;

        private AudioSource _audioSource = default;

        private AudioSource _audioSourceSuccess = default;

        private void Awake()
        {
            foreach (var item in GetComponents<AudioSource>())
            {
                if (item.clip is null) _audioSource = item;
                else _audioSourceSuccess = item;
            }
        }

        public void PlayPickupSfx() =>
            PlayAudioClip(_pickupAudioClip);

        public void PlayOnDropSfx() =>
            PlayAudioClip(_dropAudioClip);

        public void PlaySuccessOnDropSfx() =>
            _audioSourceSuccess.Play();

        public void PlayUsePowerupSfx() =>
            PlayAudioClip(_usePowerUp);

        public void PlayPickupPowerupSfx() =>
            PlayAudioClip(_pickupPowerUpAudioClip);

        private void PlayAudioClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}