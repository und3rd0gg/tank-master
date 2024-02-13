using TankMaster.Infrastructure.Services;
using TMPro;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.Panels
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _counterSound;
        private IAudioService _audioService;
        private IInputService _inputService;

        private const string CountDown = "CountDown";

        [Inject]
        internal void Construct(IInputService inputService, IAudioService audioService) {
            _inputService = inputService;
            _audioService = audioService;
        }

        public void StartCount()
        {
            gameObject.SetActive(true);
            _animator.Play(CountDown);
        }

        public void ChangeText(int number)
        {
            _text.text = number.ToString();
            _audioSource.PlayOneShot(_counterSound);
        }

        public void ContinueGame()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            _inputService.ShowVisuals();
            _audioService.UnmuteSound();
        }
    }
}