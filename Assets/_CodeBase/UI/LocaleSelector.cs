using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace TankMaster
{
    public class LocaleSelector : MonoBehaviour
    {
        public void SetLocale(int localeIndex) => 
            ChangeLocale(localeIndex);

        private async UniTask ChangeLocale(int localeIndex)
        {
            await LocalizationSettings.InitializationOperation.ToUniTask();
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        }
    }
}
