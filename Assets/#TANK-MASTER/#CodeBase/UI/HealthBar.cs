using System;
using AYellowpaper;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankMaster.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        [SerializeField] private InterfaceReference<IActor> _target;
        [SerializeField] private TMP_Text _HealthAmountText;

        private IСharacterСharacteristic _observableHealth;

        [Range(0.01f, 1)] [SerializeField] private float _smoothness = 0.4f;

        private void Awake()
        {
            _observableHealth = _target.Value.Health;
            OnValueChanged(_observableHealth.Value, _observableHealth.MaxValue);
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
            UpdateText(currentValue, maxValue);
        }

        private void UpdateText(uint currentValue, uint maxValue) => 
            _HealthAmountText.text = $"{currentValue}/{maxValue}";

        private float NormalizeValue(uint value, uint maxValue) =>
            Mathf.Abs((float) value / maxValue);

        private async UniTask ChangeBarAmountAsync(float normalizedValue)
        {
            while (Math.Abs(_slider.value - normalizedValue) > 0.05f)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, normalizedValue, Time.deltaTime * _smoothness);
                await UniTask.Yield();
            }
        }
    }
}