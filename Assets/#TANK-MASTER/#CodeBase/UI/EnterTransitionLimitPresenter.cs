using TMPro;
using UnityEngine;

namespace TankMaster.UI
{
    public class EnterTransitionLimitPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void UpdateText(int enemiesLeft, int maxEnemies)
        {
            _text.text = $"{enemiesLeft}/{maxEnemies}";
        }
    }
}