using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class DialogueManager : MonoBehaviour
    {
        private void Awake()
        {
            DialogueEventChannelSO.OnDialogueStartRequested += StartDialogue;
        }

        public void StartDialogue(string actor, string text)
        {
            UIEventChannelSO.RaiseDialogueTextRequested(actor);
            UIEventChannelSO.RaisePrintLineRequested(text);
            // UIEventChannelSO.RaiseEndDialogueRequested();
        }
    }
}
