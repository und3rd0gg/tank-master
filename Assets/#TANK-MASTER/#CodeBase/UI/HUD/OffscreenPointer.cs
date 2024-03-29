using UnityEngine;

namespace TankMaster.UI.HUD
{
  public class OffscreenPointer
  {
    private readonly Canvas _canvas;
    private readonly Camera _mainCamera;
    private readonly Transform _playerTransform;
    private readonly RectTransform _pointer;
    private readonly bool _rotate;
    private readonly Vector2 _pointerHalfSize;
    private readonly TransformPointer _transformPointer;
    private readonly bool _hideable;

    public OffscreenPointer(Canvas canvas, Camera mainCamera, Transform playerTransform, RectTransform pointer,
      Vector2 pointerSize, TransformPointer transformPointer, bool rotate = false, bool hideable = true) {
      _transformPointer = transformPointer;
      _pointerHalfSize = pointerSize / 2f;
      _rotate = rotate;
      _hideable = hideable;
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

      if (_hideable || toTargetMagnitude > minDistance) {
        _transformPointer.Show();
      }
      else {
        _transformPointer.Hide();
      }

      minDistance = Mathf.Clamp(minDistance, 0, toTargetMagnitude);
      Vector3 worldPos = ray.GetPoint(minDistance);
      Vector3 screenPos = _mainCamera.WorldToScreenPoint(worldPos);
      RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, screenPos,
        _canvas.worldCamera, out var canvasPos);
      _pointer.anchoredPosition = canvasPos + GetOffset(planeIndex);

      if (_rotate) {
        _pointer.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(toTarget.normalized));
      }
    }

    private float GetAngleFromVectorFloat(Vector3 dir) {
      dir = dir.normalized;
      float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      if (n < 0) n += 360;

      return n;
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