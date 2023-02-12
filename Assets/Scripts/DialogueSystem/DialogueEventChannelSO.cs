using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class DialogueEventChannelSO : ScriptableObject
    {
        public delegate void DialogueStartRequested(string actor, string text);
        public static event DialogueStartRequested OnDialogueStartRequested;
        public static void RaiseDialogueStartRequested(string actor, string text)
        {
            if (OnDialogueStartRequested != null)
                OnDialogueStartRequested(actor, text);
        }

        public delegate void DialogueEnded();
        public static event DialogueEnded OnDialogueEnded;
        public static void RaiseDialogueEnded()
        {
            if (OnDialogueEnded != null)
                OnDialogueEnded();
        }
    }
}
