using System;
using TankMaster.Gameplay.Actors.MainPlayer.Sound;
using TankMaster.Infrastructure.Services;
using TankMaster.Logic;
using UnityEngine;
using VContainer;

namespace TankMaster.Gameplay.Actors.MainPlayer
{
    public class MovementAlongSurface : MonoBehaviour
    {
        [SerializeField] private CollisionObserver _collisionObserver;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _speed;
        [Header("Sound")] 
        [SerializeField] private CharacterSound _soundPlayer;

        private IInputService _inputService;
        private Vector3 _normal;

        public event Action EngineStarted;
        public event Action EngineStopped;
        public event Action<float> SpeedChanged;

        [Inject]
        internal void Construct(IInputService inputService) {
            _inputService = inputService;
        }

        private void OnEnable()
        {
            _collisionObserver.CollisionEntered += GetNormal;
            EngineStarted?.Invoke();
        }

        private void OnDisable()
        {
            EngineStopped?.Invoke();
        }

        private void FixedUpdate()
        {
            if (_inputService.IsActive)
            {
                _soundPlayer.EngineAccelerate();
                var motionVector = new Vector3(_inputService.MovementAxis.y, 0,
                    -_inputService.MovementAxis.x);
                transform.rotation =
                    Quaternion.LookRotation(motionVector);
                var motion = motionVector *
                             (Time.fixedDeltaTime * _speed);
                motion.y = Physics.gravity.y * Time.deltaTime;
                _controller.Move(motion);
            }
            else
            {
                _soundPlayer.EngineDeccelerate();
            }

            var offset = Physics.gravity * Time.fixedDeltaTime;
            _controller.Move(offset);
        }

        private Vector3 Project(Vector3 forward)
        {
            return forward - Vector3.Dot(forward, _normal) * _normal;
        }

        private void GetNormal(Collision obj) =>
            _normal = obj.contacts[0].normal;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + _normal * 10f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Project(transform.forward) * 3f);
        }
    }
}