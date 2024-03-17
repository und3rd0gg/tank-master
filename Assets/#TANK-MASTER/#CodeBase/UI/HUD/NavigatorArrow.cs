using System;
using Dreamteck.Splines;
using TankMaster.Infrastructure.Factory;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.HUD
{
    public class NavigatorArrow : MonoBehaviour
    {
        [SerializeField] private RectTransform _arrow;

        private const float STEP = 5f;

        private Transform _player;
        private SplineComputer _spline;
        private SplineSample _splineSample;
        private IEnvFactory _envFactory;
        private Camera _camera;

        private Transform worldPointer;
        private Canvas _canvas;
        private OffscreenPointer _offscreenPointer;

        [Inject]
        internal void Construct(IGameFactory gameFactory, IEnvFactory envFactory) {
            _envFactory = envFactory;
            _player = gameFactory.PlayerGameObject.transform;
            _spline = envFactory.Path;
            _camera = gameFactory.GetMainCamera();
            _canvas = gameFactory.Interface.GetComponent<Interface>().Canvas;
            _canvas = FindObjectOfType<Canvas>();
            
            Debug.Assert(_spline != null);
            Debug.Assert(_player != null);
        }

        private void Start() {
            worldPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            _offscreenPointer = new OffscreenPointer(FindObjectOfType<Canvas>(), _camera, _player, _arrow);
        }

        private void Update() {
            _canvas = FindObjectOfType<Canvas>();
            _spline.Project(_player.position, ref _splineSample);
            var nearestPos = _splineSample.percent;
            var targetPos = nearestPos + (15 / _envFactory.Path.CalculateLength());
            var pos = _spline.EvaluatePosition(targetPos);
            _offscreenPointer.UpdatePointer(pos);
            // var toTarget = pos - _player.position;
            // var ray = new Ray(_player.position, toTarget);
            // var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
            //
            // var minDistance = float.MaxValue;
            //
            // for (var i = 0; i < planes.Length; i++) {
            //     if (planes[i].Raycast(ray, out var dist)) {
            //         if (dist < minDistance) {
            //             minDistance = dist;
            //         }
            //     }
            // }
            //
            // minDistance = Mathf.Clamp(minDistance, 0, toTarget.magnitude);
            // var worldPos = ray.GetPoint(minDistance);
            // worldPointer.position = worldPos;
            //
            // var screenPos = _camera.WorldToScreenPoint(worldPointer.position);
            // RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, screenPos, _canvas.worldCamera, out var canvasPos);
            // _arrow.anchoredPosition = canvasPos;
        }
    }

    public class OffscreenPointer
    {
        private readonly Canvas _canvas;
        private readonly Camera _mainCamera;
        private Transform _playerTransform;
        private RectTransform _pointer;

        public OffscreenPointer(Canvas canvas, Camera mainCamera, Transform playerTransform, RectTransform pointer) {
            _pointer = pointer;
            _playerTransform = playerTransform;
            _canvas = canvas;
            _mainCamera = mainCamera;
            Debug.Log( _pointer == null);
            Debug.Log(_playerTransform == null);
            Debug.Log(canvas == null);
            Debug.Log(_mainCamera == null);
        }

        public void UpdatePointer(Vector3 targetPos) {
            Vector3 playerPos = _playerTransform.position;
            Vector3 toTarget = targetPos - playerPos;
            var ray = new Ray(playerPos, toTarget);
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);

            var minDistance = float.MaxValue;

            for (var i = 0; i < planes.Length; i++) {
                if (planes[i].Raycast(ray, out var dist)) {
                    if (dist < minDistance) {
                        minDistance = dist;
                    }
                }
            }
            
            minDistance = Mathf.Clamp(minDistance, 0, toTarget.magnitude);
            Vector3 worldPos = ray.GetPoint(minDistance);
            Vector3 screenPos = _mainCamera.WorldToScreenPoint(worldPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, screenPos, _canvas.worldCamera, out var canvasPos);
            _pointer.anchoredPosition = canvasPos;
        }
    }
}