using DG.Tweening;

using UnityEngine;
using Random = UnityEngine.Random;

namespace TankMaster._CodeBase.Gameplay
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _rotationTime;
        [SerializeField] private float _moveTime;
        [SerializeField] private float _moveOffsetY;
        [SerializeField] private float _offset;

        private int _environmentLayer;
        private Sequence _coinSequence;
        private Quaternion _defaultRotation;

        private void Awake()
        {
            _environmentLayer = LayerMask.NameToLayer("Environment");
            CreateSequence();
            _defaultRotation = transform.rotation;
        }

        private void OnEnable()
        {
            PerformSideJump();
        }

        private void OnDisable() =>
            _coinSequence.Pause();

        private void OnDestroy() =>
            _coinSequence.Kill();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _environmentLayer)
            {
                transform.rotation = _defaultRotation;
                _rigidbody.velocity = Vector3.zero;
                _coinSequence.Restart();
            }
        }

        private void PerformSideJump()
        {
            var randomVector = new Vector3(Random.Range(-_offset, _offset), 0, Random.Range(-_offset, _offset));
            _rigidbody.AddForce(Vector3.up * 5 + randomVector, ForceMode.Impulse);
        }

        private void CreateSequence()
        {
            _coinSequence = DOTween.Sequence();
            _coinSequence.Append(transform
                .DOLocalRotate(new Vector3(0, 360, 0), _rotationTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(int.MaxValue));
            _coinSequence.Insert(0, transform
                .DOMoveY(transform.position.y + _moveOffsetY, _moveTime)
                .SetLoops(int.MaxValue, LoopType.Yoyo));
            _coinSequence.SetLoops(-1);
            _coinSequence.Pause();
        }
    }
}