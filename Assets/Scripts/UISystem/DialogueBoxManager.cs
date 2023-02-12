using TMPro;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class DialogueBoxManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject DialoguePanel;
        [SerializeField]
        private TMP_Text ActorTMP;
        [SerializeField]
        private TMP_Text TextTMP;

        private string _currentActor = "";
        private List<string> _currentLines = new List<string>();

        void Awake()
        {
            UIEventChannelSO.OnDialogueTextRequested += DisplayDialogueBox;
            UIEventChannelSO.OnPrintLineRequested += PrintLine;
            UIEventChannelSO.OnEndDialogueRequested += HideDialogueBox;
        }

        private void DisplayDialogueBox(string actor)
        {
            _currentActor = actor;
            ActorTMP.text = _currentActor;
            DialoguePanel.SetActive(true);
        }

        private void PrintLine(string line)
        {
            _currentLines.Add(line);
            if (_currentLines.Count >= 5)
            {
                _currentLines.RemoveAt(0);
            }
            TextTMP.text = string.Join("\n", _currentLines);
        }

        private void HideDialogueBox()
        {
            _currentActor = "";
            _currentLines.Clear();
            DialoguePanel.SetActive(false);
        }
    }
}
