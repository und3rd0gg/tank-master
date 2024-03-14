using System;
using Dreamteck.Splines;
using TankMaster.Infrastructure.Factory;
using TankMaster.Logic;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.HUD
{
    public class NavigatorArrow : MonoBehaviour
    {
        [SerializeField] private RectTransform _arrow;

        private Transform _player;
        private SplineComputer _spline;
        private SplineSample _splineSample;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _player = gameFactory.PlayerGameObject.transform;
            _spline = gameFactory.Transition.Path;
            
            Debug.Assert(_spline != null);
            Debug.Assert(_player != null);
        }

        private void Update() { 
            _spline.Project(_player.position, ref _splineSample);
        }
    }
}