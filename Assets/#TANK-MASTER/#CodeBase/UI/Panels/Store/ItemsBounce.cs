using DG.Tweening;

using UnityEngine;

namespace TankMaster._CodeBase.UI.Panels.Store
{
    public class ItemsBounce : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        
        [SerializeField] private float _amplitude;
        [SerializeField] private float _loopDuration;

        private Tween _bounceTween;

        private void OnEnable()
        {
            _bounceTween = _rectTransform.DOAnchorPosY(_rectTransform.anchoredPosition.y + _amplitude, _loopDuration)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable() => 
            KillBounceTween();

        private void OnDestroy() => 
            KillBounceTween();

        private void KillBounceTween() => 
            _bounceTween.Kill();
    }
}