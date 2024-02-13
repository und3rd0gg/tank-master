using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TankMaster.Gameplay
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField]private CinemachineVirtualCamera _virtualCamera;

        private CinemachineBasicMultiChannelPerlin _perlin;

        private void Awake()
        {
            InitializePerlinComponent();
        }

        public void ShakeCamera(float duration, float amplitudeGain = 2f, float frequencyGain = 2f)
        {
            CameraShakeAsync(duration, amplitudeGain, frequencyGain).Forget();
        }

        private void InitializePerlinComponent() =>
            _perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        private async UniTaskVoid CameraShakeAsync(float duration, float amplitudeGain = 2f, float frequencyGain = 2f)
        {
            _perlin.m_AmplitudeGain = amplitudeGain;
            _perlin.m_FrequencyGain = frequencyGain;
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            _perlin.m_AmplitudeGain = 0;
            _perlin.m_FrequencyGain = 0;
        }
    }
}