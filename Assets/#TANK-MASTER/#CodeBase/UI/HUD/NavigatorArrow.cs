using Dreamteck.Splines;
using TankMaster.Infrastructure.Factory;
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
        
            Debug.Assert(_envFactory != null);
            Debug.Assert(_player != null);
            Debug.Assert(_spline != null);
            Debug.Assert(_camera != null);
            Debug.Assert(_canvas != null);
        }

        private void Start() {
            Debug.Log("start");
            worldPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            _offscreenPointer = new OffscreenPointer(FindObjectOfType<Canvas>(), _camera, _player, _arrow);
        }

        private void Update() {
            _spline.Project(_player.position, ref _splineSample);
            var nearestPos = _splineSample.percent;
            var targetPos = nearestPos + (15 / _envFactory.Path.CalculateLength());
            var pos = _spline.EvaluatePosition(targetPos);
            _offscreenPointer.UpdatePointer(pos);
        }
    }

    public class OffscreenPointer
    {
        private readonly Canvas _canvas;
        private Camera _mainCamera;
        private Transform _playerTransform;
        private RectTransform _pointer;

        public OffscreenPointer(Canvas canvas, Camera mainCamera, Transform playerTransform, RectTransform pointer) {
            _pointer = pointer;
            _playerTransform = playerTransform;
            _canvas = canvas;
            _mainCamera = mainCamera;
            
            Debug.Assert( _pointer != null);
            Debug.Assert(_playerTransform != null);
            Debug.Assert(canvas != null);
            Debug.Assert(_mainCamera != null);
        }

        public void UpdatePointer(Vector3 targetPos) {
            Vector3 playerPos = _playerTransform.position;
            Vector3 toTarget = targetPos - playerPos;
            var ray = new Ray(playerPos, toTarget);
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);

            var minDistance = float.MaxValue;

            for (var i = 0; i < planes.Length; i++) {
                if (planes[i].Raycast(ray, out float dist)) {
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