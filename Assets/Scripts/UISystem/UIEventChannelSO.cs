using UnityEngine;

namespace TopDownTRPG
{
    public class UIEventChannelSO : ScriptableObject
    {
        public delegate void ContextualMenuDisplayRequested(Vector3 position, bool canAttack, bool canMove);
        public static event ContextualMenuDisplayRequested OnContextualMenuDisplayRequested;
        public static void RaiseContextualMenuDisplayRequest(Vector3 position, bool canAttack, bool canMove)
        {
            if (OnContextualMenuDisplayRequested != null)
                OnContextualMenuDisplayRequested(position, canAttack, canMove);
        }

        public delegate void ContextualMenuHideRequested();
        public static event ContextualMenuHideRequested OnContextualMenuHideRequested;
        public static void RaiseContextualMenuHideRequest()
        {
            if (OnContextualMenuHideRequested != null)
                OnContextualMenuHideRequested();
        }

        public delegate void HeaderTextRequested(string text, float duration);
        public static event HeaderTextRequested OnHeaderTextRequested;
        public static void RaiseHeaderTextRequest(string text, float duration)
        {
            if (OnHeaderTextRequested != null)
                OnHeaderTextRequested(text, duration);
        }

        public delegate void DialogueTextRequested(string actor);
        public static event DialogueTextRequested OnDialogueTextRequested;
        public static void RaiseDialogueTextRequested(string actor)
        {
            if (OnDialogueTextRequested != null)
                OnDialogueTextRequested(actor);
        }

        public delegate void PrintLineRequested(string line);
        public static event PrintLineRequested OnPrintLineRequested;
        public static void RaisePrintLineRequested(string line)
        {
            if (OnPrintLineRequested != null)
                OnPrintLineRequested(line);
        }

        public delegate void EndDialogueRequested();
        public static event EndDialogueRequested OnEndDialogueRequested;
        public static void RaiseEndDialogueRequested()
        {
            if (OnEndDialogueRequested != null)
                OnEndDialogueRequested();
        }
    }
}
