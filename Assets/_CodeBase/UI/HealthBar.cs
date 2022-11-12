using AYellowpaper;
using Cysharp.Threading.Tasks;
using Dythervin.AutoAttach;
using TankMaster._CodeBase.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace TankMaster._CodeBase.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<IActor> _target;

        [SerializeField] [Attach(Attach.Child)]
        private Slider _slider;

        private IСharacterСharacteristic _observableHealth;

        [Range(0.01f, 1)] [SerializeField] private float _smoothness = 0.4f;

        private void Awake()
        {
            _observableHealth = _target.Value.Health;
        }

        private void OnEnable()
        {
            _observableHealth.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _observableHealth.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(uint currentValue, uint maxValue)
        {
            var normalizedValue = NormalizeValue(currentValue, maxValue);
            ChangeBarAmountAsync(normalizedValue);
        }

        private float NormalizeValue(uint value, uint maxValue) =>
            Mathf.Abs((float) value / maxValue);

        private async UniTask ChangeBarAmountAsync(float normalizedValue)
        {
            while (!Mathf.Approximately(_slider.value, normalizedValue))
            {
                _slider.value = Mathf.MoveTowards(_slider.value, normalizedValue, Time.deltaTime * _smoothness);
                await UniTask.Yield();
            }
        }
    }
}