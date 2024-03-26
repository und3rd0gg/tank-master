using DG.Tweening;
using UnityEngine;

namespace TankMaster.Gameplay.Traps
{
  public class PatrolSaw : MonoBehaviour
  {
    [SerializeField] private Transform _endPos;
    [SerializeField] private Transform _blade;
    [SerializeField] private float _moveDur;
    [SerializeField] private float _rotDur;
    
    private Vector3 _pointA;
    private Vector3 _pointB;

    private Tween _moveTween;
    
    private void Awake() {
      _pointA = transform.position;
      _pointB = _endPos.position;
    }

    private void OnEnable() {
      // _moveTween = transform
      //   .DOMove(_pointB, _moveDur)
      //   .SetEase(Ease.Linear)
      //   .SetLoops(-1, LoopType.Yoyo)
      //   .From(_pointA);

      transform
        .DORotate(new Vector3(0, 180, 0), _rotDur)
        .SetSpeedBased()
        .SetEase(Ease.Linear);
    }

    private void OnDisable() {
      _moveTween.Kill();
    }
  }
}