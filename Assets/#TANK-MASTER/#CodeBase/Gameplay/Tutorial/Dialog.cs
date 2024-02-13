using TMPro;
using UnityEngine;

namespace TankMaster.Gameplay.Tutorial
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] private DialogNode[] _nodes;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _dialogWindow;

        private int _currentNode = 0;

        private void Awake()
        {
            UpdateText();
        }

        public void ShowDialog(int nodeId)
        {
            _currentNode = nodeId;
            UpdateText();
            _dialogWindow.SetActive(true);
        }

        public void SwitchToNextNode()
        {
            _nodes[_currentNode].OnClose?.Invoke();
            
            if (_nodes[_currentNode].LastNode)
            {
                _dialogWindow.SetActive(false);
                return;    
            }

            _currentNode++;

            UpdateText();
        }

        private void UpdateText() => 
            _text.text = _nodes[_currentNode].Text.GetLocalizedString();
    }
}