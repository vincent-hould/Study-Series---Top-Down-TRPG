using System.Collections.Generic;
using TMPro;
using TopDownTRPG.Assets.Scripts.UISystem;
using UnityEngine;

namespace TopDownTRPG
{
    public class DialogueBox : MonoBehaviour, IDialogueBox
    {
        [SerializeField]
        private TMP_Text ActorTMP;
        [SerializeField]
        private TMP_Text TextTMP;

        public void DisplayDialogueBox(string actor)
        {
            ActorTMP.text = actor;
            gameObject.SetActive(true);
        }

        public void PrintText(string text)
        {
            TextTMP.text = text;
        }

        public void HideDialogueBox()
        {
            ActorTMP.text = "";
            TextTMP.text = "";
            gameObject.SetActive(false);
        }
    }
}
