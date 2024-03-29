using UnityEngine;
using UnityEngine.UI;

namespace TankMaster.UI.HUD
{
  public class TransformPointer : MonoBehaviour
  {
    [SerializeField] private Image _pointerImage;

    protected OffscreenPointer OffscreenPointer;
    
    private Transform _target;

    public void Init(Canvas canvas, Camera mainCamera, Transform playerTransform, Transform target,
      Vector2 pointerSize, bool rotate, bool hideable) {
      _target = target;
      OffscreenPointer = new OffscreenPointer(canvas, mainCamera, playerTransform,
        (RectTransform)transform, pointerSize, this, rotate, hideable);
    }

    protected virtual void LateUpdate() {
      if (_target != null)
        OffscreenPointer.UpdatePointer(_target.position);
    }

    public void Show() {
      _pointerImage.enabled = true;
    }

    public void Hide() {
      _pointerImage.enabled = false;
    }
  }
}