using UnityEngine;

namespace TankMaster.Gameplay.Enemies
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class EnemyProfile : ScriptableObject
    {
        [SerializeField] private ShootProfile _shootProfile;

        public EnemyProfile()
        {
            _shootProfile = CreateInstance<ShootProfile>();
        }
    }
}