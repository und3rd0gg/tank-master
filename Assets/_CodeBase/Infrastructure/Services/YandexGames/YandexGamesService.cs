using Agava.YandexGames;
using Cysharp.Threading.Tasks;
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
        
        private async void Initialize()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
            await YandexGamesSdk.Initialize().ToUniTask();
            
            if(PlayerAccount.IsAuthorized && !PlayerAccount.HasPersonalProfileDataPermission)
                PlayerAccount.RequestPersonalProfileDataPermission();
        }
    }
}