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

        [SerializeField]
        private AudioClip _successDropAudioClip = default;

        private AudioSource _audioSource = default;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayPickupSfx() =>
            PlayAudioClip(_pickupAudioClip);

        public void PlayOnDropSfx() =>
            PlayAudioClip(_dropAudioClip);

        public void PlaySuccessOnDropSfx() =>
            PlayAudioClip(_successDropAudioClip);

        public void PlayUsePowerupSfx() =>
            PlayAudioClip(_usePowerUp);

        public void PlayPickupPowerupSfx() =>
            PlayAudioClip(_pickupPowerUpAudioClip);

        private void PlayAudioClip(AudioClip clip)
        {
            if (_audioSource.clip == clip) return;
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}