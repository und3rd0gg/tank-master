using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField][Attach] private CinemachineVirtualCamera _virtualCamera;

        private CinemachineBasicMultiChannelPerlin _perlin;

        private void Awake()
        {
            InitializePerlinComponent();
        }

        public void ShakeCamera(float duration, float amplitudeGain = 2f, float frequencyGain = 2f)
        {
            CameraShakeAsync(duration, amplitudeGain, frequencyGain);
        }

        private void InitializePerlinComponent() => 
            _perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>() ??
                      _virtualCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        private async UniTask CameraShakeAsync(float duration, float amplitudeGain = 2f, float frequencyGain = 2f)
        {
            _perlin.m_AmplitudeGain = amplitudeGain;
            _perlin.m_FrequencyGain = frequencyGain;
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            _perlin.m_AmplitudeGain = 0;
            _perlin.m_FrequencyGain = 0;
        }
    }
}