using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace TankMaster.UI
{
    public class LocaleSelector : MonoBehaviour
    {
        public void SetLocale(int localeIndex) => 
            ChangeLocale(localeIndex).Forget();

        private async UniTaskVoid ChangeLocale(int localeIndex)
        {
            await LocalizationSettings.InitializationOperation.ToUniTask();
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        }
    }
}
