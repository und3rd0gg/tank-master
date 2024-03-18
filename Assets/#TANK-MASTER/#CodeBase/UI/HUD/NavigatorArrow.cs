using Dreamteck.Splines;
using TankMaster.Infrastructure.Factory;
using Unity.Collections.LowLevel.Unsafe;
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

        // private void Start() {
        //     Debug.Log("start");
        //     worldPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        //     _offscreenPointer = new OffscreenPointer(FindObjectOfType<Canvas>(), _camera, _player, _arrow);
        // }
        //
        // private void Update() {
        //     _spline.Project(_player.position, ref _splineSample);
        //     var nearestPos = _splineSample.percent;
        //     var targetPos = nearestPos + (15 / _envFactory.Path.CalculateLength());
        //     var pos = _spline.EvaluatePosition(targetPos);
        //     _offscreenPointer.UpdatePointer(pos);
        // }
    }
}