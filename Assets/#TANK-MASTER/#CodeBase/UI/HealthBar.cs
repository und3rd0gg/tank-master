using System;
using Cysharp.Threading.Tasks;
using TankMaster.Gameplay;
using TankMaster.Gameplay.Actors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankMaster.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        [SerializeField] private DamageableBase _target;
        [SerializeField] private TMP_Text _HealthAmountText;

        private IActorAttribute<int> _observableHealth;

        [Range(0.01f, 1)] [SerializeField] private float _smoothness = 0.4f;

        private void Awake()
        {
            _observableHealth = _target.Health;
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

        private void OnValueChanged(int currentValue, int maxValue)
        {
            var normalizedValue = NormalizeValue(currentValue, maxValue);
            ChangeBarAmountAsync(normalizedValue);
            UpdateText(currentValue, maxValue);
        }

        private void UpdateText(int currentValue, int maxValue) => 
            _HealthAmountText.text = $"{currentValue}/{maxValue}";

        private float NormalizeValue(int value, int maxValue) =>
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