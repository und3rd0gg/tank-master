using System.Collections.Generic;
using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.HUD
{
    public class EnemyPointerController : MonoBehaviour
    {
        [SerializeField] private TransformPointer _pointerPrefab;

        private Player _player;
        private Dictionary<Collider, TransformPointer> _enemyPointers = new();
        private IGameFactory _gameFactory;
        private Canvas _canvas;
        private Camera _mainCamera;

        [Inject]
        internal void Construct(IGameFactory gameFactory, IPlayerService playerService) {
            _gameFactory = gameFactory;
            _canvas = _gameFactory.Interface.GetComponent<Interface>().Canvas;
            _mainCamera = gameFactory.GetMainCamera();
            _player = playerService.GetPlayer();
        }

        private void OnEnable() {
            _player.OuterRadiusDetector.OnDetected += OnDetected;
            _player.OuterRadiusDetector.OnDetectionExit += OnDetectionExit;
        }

        private void OnDisable() {
            _player.OuterRadiusDetector.OnDetected -= OnDetected;
            _player.OuterRadiusDetector.OnDetectionExit -= OnDetectionExit;
        }

        private void OnDetectionExit(Collider obj) {
            var pointer = _enemyPointers[obj];
            Destroy(pointer.gameObject);
            _enemyPointers.Remove(obj);
        }

        private void OnDetected(Collider obj) {
            var pointer = Instantiate(_pointerPrefab, transform);
            var rTransform = (RectTransform)pointer.transform;
                pointer.Init(_canvas, _mainCamera, _player.transform, obj.transform, 
                    new Vector2(rTransform.rect.width, rTransform.rect.height), false, true);
            _enemyPointers.Add(obj, pointer);
        }
    }
}