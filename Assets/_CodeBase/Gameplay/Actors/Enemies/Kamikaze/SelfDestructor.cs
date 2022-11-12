using Dythervin.AutoAttach;
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Actors.Enemies.Kamikaze
{
    public class SelfDestructor : MonoBehaviour
    {
        [SerializeField] private KamikazeAnimator _kamikazeAnimator;
        [SerializeField] private AttackProfile _attackProfile;
        [SerializeField][Attach] private Destroyer _destroyer;

        private Transform _target;

        private void OnEnable()
        {
            _kamikazeAnimator.Attacked += KamikazeAnimatorOnAttacked;
        }

        private void OnDisable()
        {
            _kamikazeAnimator.Attacked -= KamikazeAnimatorOnAttacked;
        }

        public void Explode()
        {
            _kamikazeAnimator.SetSelfDestruction();
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void KamikazeAnimatorOnAttacked()
        {
            _destroyer.Destroy();
        }

        public bool IsInEffectiveDistance() =>
            Vector3.Distance(transform.position, _target.transform.position) <
            _attackProfile.EffectiveDistance;
    }
}