using TankMaster._CodeBase.Infrastructure.Services;
using TMPro;
using UnityEngine;

namespace TankMaster._CodeBase.UI.Panels
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _counterSound;

        private const string CountDown = "CountDown";

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
            AllServices.Container.Single<IInputService>().ShowVisuals();
            AllServices.Container.Single<IAudioService>().UnmuteSound();
        }
    }
}