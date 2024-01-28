using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace TankMaster
{
    public class LocalizationCheckbox : MonoBehaviour
    {
        [SerializeField] private Locale _locale;
        [SerializeField] private GameObject _checkbox;

        private void OnEnable()
        {
            LocalizationSettings.Instance.OnSelectedLocaleChanged += OnSelectedLocaleChanged;
            OnSelectedLocaleChanged(LocalizationSettings.SelectedLocale);
        }

        private void OnDisable() => 
            LocalizationSettings.Instance.OnSelectedLocaleChanged -= OnSelectedLocaleChanged;

        private void OnSelectedLocaleChanged(Locale obj) => 
            _checkbox.gameObject.SetActive(obj == _locale);
    }
}