using UnityEngine;

namespace TankMaster.UI.HUD
{
  public class OffscreenPointer
  {
    private readonly Canvas _canvas;
    private Camera _mainCamera;
    private Transform _playerTransform;
    private RectTransform _pointer;
    private bool _rotate;
    private Vector2 _pointerHalfSize;

    public OffscreenPointer(Canvas canvas, Camera mainCamera, Transform playerTransform, RectTransform pointer,
      Vector2 pointerSize, bool rotate = false) {
      _pointerHalfSize = pointerSize / 2f;
      _rotate = rotate;
      _pointer = pointer;
      _playerTransform = playerTransform;
      _canvas = canvas;
      _mainCamera = mainCamera;

      Debug.Assert(_pointer != null);
      Debug.Assert(_playerTransform != null);
      Debug.Assert(canvas != null);
      Debug.Assert(_mainCamera != null);
    }

    public void UpdatePointer(Vector3 targetPos) {
      Vector3 playerPos = _playerTransform.position;
      Vector3 toTarget = targetPos - playerPos;
      var ray = new Ray(playerPos, toTarget);
      //[0] = Left, [1] = Right, [2] = Down, [3] = Up, [4] = Near, [5] = Far
      Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);

      var minDistance = float.MaxValue;
      int planeIndex = 0;

      for (var i = 0; i < planes.Length; i++) {
        if (planes[i].Raycast(ray, out float dist)) {
          if (dist < minDistance) {
            minDistance = dist;
            planeIndex = i;
          }
        }
      }

      var toTargetMagnitude = toTarget.magnitude;

      if (toTargetMagnitude > minDistance) {
        _pointer.gameObject.SetActive(true);
      }
      else {
        _pointer.gameObject.SetActive(false);
        return;
      }

      minDistance = Mathf.Clamp(minDistance, 0, toTargetMagnitude);
      Vector3 worldPos = ray.GetPoint(minDistance);
      Vector3 screenPos = _mainCamera.WorldToScreenPoint(worldPos);
      RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, screenPos,
        _canvas.worldCamera, out var canvasPos);
      _pointer.anchoredPosition = canvasPos + GetOffset(planeIndex);

      if (_rotate) {
        _pointer.rotation = GetRotation(planeIndex);
      }
    }

    private Quaternion GetRotation(int planeIndex) {
      return planeIndex switch {
        0 => Quaternion.Euler(0, 0, 90),
        1 => Quaternion.Euler(0, 0, -90),
        2 => Quaternion.Euler(0, 0, 180),
        3 => Quaternion.Euler(0, 0, 0),
        _ => Quaternion.identity,
      };
    }

    private Vector2 GetOffset(int planeIndex) {
      return planeIndex switch {
        0 => new Vector2(_pointerHalfSize.x, 0),
        1 => new Vector2(-_pointerHalfSize.x, 0),
        2 => new Vector2(0, _pointerHalfSize.y),
        3 => new Vector2(0, -_pointerHalfSize.y),
        _ => default,
      };
    }
  }
}