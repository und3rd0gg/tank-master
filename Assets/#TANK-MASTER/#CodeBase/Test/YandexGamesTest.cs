﻿using System.Collections;
using UnityEngine;

namespace TankMaster.Test
{
    public class YandexGamesTest : MonoBehaviour
    {
        public void Init()
        {
            //YandexGamesSdk.CallbackLogging = true;

            StartCoroutine(Go());
        }

        private IEnumerator Go()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            // yield return StartCoroutine(YandexGamesSdk.Initialize());
            //
            // if(PlayerAccount.IsAuthorized && !PlayerAccount.HasPersonalProfileDataPermission)
            //     PlayerAccount.RequestPersonalProfileDataPermission();
        }
    }
}