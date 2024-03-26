using UnityEngine;

namespace TankMaster.Gameplay.Projectiles
{
  public class ETFXProjectile : MonoBehaviour
  {
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _impactParticle; // Effect spawned when projectile hits a collider
    [SerializeField] private GameObject _projectileParticle; // Effect attached to the gameobject as child
    [SerializeField] private GameObject _muzzleParticle; // Effect instantly spawned when gameobject is spawned

    // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
    [SerializeField] [Range(0f, 1f)] private float _collideOffset = 0.15f;
    
    private ParticleSystem[] _trails;

    private void Awake()
    {
      _trails = GetComponentsInChildren<ParticleSystem>();
    }

    private void Start() {
      _projectileParticle = Instantiate(_projectileParticle, transform.position, transform.rotation);
      _projectileParticle.transform.parent = transform;
      
      if (_muzzleParticle)
      {
        _muzzleParticle = Instantiate(_muzzleParticle, transform.position, transform.rotation);
        Destroy(_muzzleParticle, 1.5f); // 2nd parameter is lifetime of effect in seconds
      }
    }
  }
}