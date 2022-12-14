using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization.Settings;

// ReSharper disable HeuristicUnreachableCode
#pragma warning disable CS0162

namespace TankMaster._CodeBase.Infrastructure.Services.YandexGames
{
    public class YandexGamesService : IYandexGamesService
    {
        public YandexGamesService()
        {
            YandexGamesSdk.CallbackLogging = true;
            Initialize();
        }

        private async UniTask Initialize()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
            await YandexGamesSdk.Initialize().ToUniTask();

            if (PlayerAccount.IsAuthorized && !PlayerAccount.HasPersonalProfileDataPermission)
                PlayerAccount.RequestPersonalProfileDataPermission();

            LoadDefaultLocale();
        }

        private async void LoadDefaultLocale()
        {
            await LocalizationSettings.InitializationOperation.ToUniTask();
            var browserLang = YandexGamesSdk.Environment.i18n.lang;

            var localeIndex = browserLang switch
            {
                "en" => 0,
                "ru" => 1,
                "tr" => 2,
                _ => 0
            };
            
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        }
    }
}