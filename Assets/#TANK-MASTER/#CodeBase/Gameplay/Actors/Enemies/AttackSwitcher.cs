using System;
using AYellowpaper;

using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies
{
    public class AttackSwitcher : MonoBehaviour
    {
        [SerializeField] private AggroSwitcher _aggroSwitcher;

        [SerializeField] private InterfaceReference<IAttacker> _attacker;
        [SerializeField][Min(0)] private float _minAttackDistance;
        [SerializeField][Min(0)] private float _maxAttackDistance;
        [SerializeField] private bool _stopAgentAfterSwitch;
        [SerializeField] private LayerMask _attackLayerMask;

        private int _attackZoneBufferSize = 1;
        private Collider[] _AttackZoneBuffer;

        private Action _currentBehavior;

        private void Start()
        {
            InitializeAttackZoneBuffer();
            SetBehavior(CheckAttackZoneEnter);
        }

        private void FixedUpdate()
        {
            _currentBehavior?.Invoke();
        }

        private void CheckAttackZoneEnter()
        {
            ClearAttackZoneBuffer();
            Physics.OverlapSphereNonAlloc(transform.position, _minAttackDistance, _AttackZoneBuffer, _attackLayerMask);

            foreach (var collider in _AttackZoneBuffer)
            {
                if (collider != null)
                {
                    OnAttackZoneEnter(collider);
                }
            }
        }

        private void OnAttackZoneEnter(Collider player)
        {
            _aggroSwitcher.StopAggro();
            _attacker.Value.SetTarget(player.transform);
            _attacker.Value.enabled = true;
            SetBehavior(CheckAttackZoneExit);
        }

        private void CheckAttackZoneExit()
        {
            ClearAttackZoneBuffer();
            Physics.OverlapSphereNonAlloc(transform.position, _maxAttackDistance, _AttackZoneBuffer, _attackLayerMask);

            foreach (var collider in _AttackZoneBuffer)
            {
                if (collider == null)
                {
                    Debug.Log("exitted attack zone");
                    OnAttackZoneExit();
                }
            }
        }

        private void OnAttackZoneExit()
        {
            _aggroSwitcher.StartAggro();
            _attacker.Value.enabled = false;
            SetBehavior(CheckAttackZoneEnter);
        }
        
        private void SetBehavior(Action newBehavior) => 
            _currentBehavior = newBehavior;

        private void InitializeAttackZoneBuffer() =>
            _AttackZoneBuffer = new Collider[_attackZoneBufferSize];

        private void ClearAttackZoneBuffer()
        {
            for (var i = 0; i < _AttackZoneBuffer.Length; i++)
                _AttackZoneBuffer[i] = null;
        }

        private void OnDrawGizmos()
        {
            DrawWireSphere(transform.position, _maxAttackDistance, Color.cyan);
            DrawWireSphere(transform.position, _minAttackDistance, Color.red);

            void DrawWireSphere(Vector3 center, float radius, Color color)
            {
                Gizmos.color = color;
                Gizmos.DrawWireSphere(center, radius);
            }
        }
    }
}