using System;
using TankMaster.Gameplay.Actors.MainPlayer;
using TankMaster.Infrastructure.Factory;
using TankMaster.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace TankMaster.UI.HUD
{
  public class DestinationPointerController : MonoBehaviour
  {
    [SerializeField] private TransformPointer _pointer;
    
    private IGameFactory _gameFactory;
    private Canvas _canvas;
    private Camera _mainCamera;
    private Player _player;
    private Transform _target;

    [Inject]
    internal void Construct(IGameFactory gameFactory, IPlayerService playerService) {
      _gameFactory = gameFactory;
      _canvas = _gameFactory.Interface.GetComponent<Interface>().Canvas;
      _mainCamera = gameFactory.GetMainCamera();
      _player = playerService.GetPlayer();
    }

    private void Awake() {
      _target = GameObject.Find("__waypoint").transform;
      var rTransform = (RectTransform)_pointer.transform;
      _pointer.Init(_canvas, _mainCamera, _player.transform, _target.transform, 
        new Vector2(rTransform.rect.width, rTransform.rect.height), true, false);
    }
  }
}