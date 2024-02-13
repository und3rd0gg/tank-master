using System;
using UnityEngine;
using UnityEngine.Playables;

namespace TankMaster.Logic.FirstStartCutscene
{
    public class CutscenePlayableDirector : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _playableDirector;

        public event Action CutsceneFinished;

        public void StartCutscene()
        {
            _playableDirector.Play();
        }

        public void FinishCutscene()
        {
            CutsceneFinished?.Invoke();
        }
    }
}