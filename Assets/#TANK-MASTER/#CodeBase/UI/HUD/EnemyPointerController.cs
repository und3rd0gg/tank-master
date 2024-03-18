using System.Collections.Generic;
using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.HUD
{
    public class EnemyPointerController : MonoBehaviour
    {
        [SerializeField] private RectTransform _pointerPrefab;

        private Player _player;
        private Dictionary<Collider, EnemyPointer> _enemyPointers = new();
        private IGameFactory _gameFactory;
        private Canvas _canvas;
        private Camera _mainCamera;

        [Inject]
        internal void Construct(IGameFactory gameFactory) {
            _gameFactory = gameFactory;
            _canvas = _gameFactory.Interface.GetComponent<Interface>().Canvas;
            _mainCamera = gameFactory.GetMainCamera();
            _player = gameFactory.PlayerGameObject.GetComponent<Player>();
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
            var pointer = Instantiate(_pointerPrefab, transform).gameObject
                .AddComponent<EnemyPointer>()
                .Init(_canvas, _mainCamera, _player.transform, obj.transform, 
                    new Vector2(_pointerPrefab.rect.width, _pointerPrefab.rect.height));
            _enemyPointers.Add(obj, pointer);
        }
    }
}