
using UnityEngine;

namespace TankMaster._CodeBase.Gameplay.Projectiles
{
    public class ETFXProjectileScript : MonoBehaviour
    {
        [SerializeField]private Projectile _projectile;
        [SerializeField] private LayerMask _layerMask;

        public GameObject impactParticle; // Effect spawned when projectile hits a collider
        public GameObject projectileParticle; // Effect attached to the gameobject as child
        public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned
        [Header("Adjust if not using Sphere Collider")]
        public float colliderRadius = 1f;
        [Range(0f, 1f)] // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
        public float collideOffset = 0.15f;

        private SphereCollider _sphereCollider;
        private Rigidbody _rigidbody;
        private ParticleSystem[] _trails;

        private void Awake()
        {
            _trails = GetComponentsInChildren<ParticleSystem>();
            _rigidbody = transform.GetComponent<Rigidbody>();
            _sphereCollider = transform.GetComponent<SphereCollider>();
        }

        void Start()
        {
            projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
            projectileParticle.transform.parent = transform;
            if (muzzleParticle)
            {
                muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
                Destroy(muzzleParticle, 1.5f); // 2nd parameter is lifetime of effect in seconds
            }
        }
		
        void FixedUpdate()
        {	
            if (_rigidbody.velocity.magnitude != 0)
            {
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity); // Sets rotation to look at direction of movement
            }
			
            RaycastHit hit;
			
            float radius; // Sets the radius of the collision detection
            if (_sphereCollider)
                radius = _sphereCollider.radius;
            else
                radius = colliderRadius;

            Vector3 direction = _rigidbody.velocity; // Gets the direction of the projectile, used for collision detection
            if (_rigidbody.useGravity)
                direction += Physics.gravity * Time.deltaTime; // Accounts for gravity if enabled
            direction = direction.normalized;

            float detectionDistance = _rigidbody.velocity.magnitude * Time.deltaTime; // Distance of collision detection for this frame

            if (Physics.SphereCast(transform.position, radius, direction, out hit, detectionDistance, layerMask: _layerMask)) // Checks if collision will happen
            {
                transform.position = hit.point + (hit.normal * collideOffset); // Move projectile to point of collision

                _projectile.DoImpact();

                GameObject impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject; // Spawns impact effect

                //Component at [0] is that of the parent i.e. this object (if there is any)
                for (int i = 1; i < _trails.Length; i++) // Loop to cycle through found particle systems
                {
                    ParticleSystem trail = _trails[i];

                    if (trail.gameObject.name.Contains("Trail"))
                    {
                        trail.transform.SetParent(null); // Detaches the trail from the projectile
                        Destroy(trail.gameObject, 2f); // Removes the trail after seconds
                    }
                }

                Destroy(projectileParticle, 3f); // Removes particle effect after delay
                Destroy(impactP, 3.5f); // Removes impact effect after delay
                Destroy(gameObject); // Removes the projectile
            }
        }
    }
}