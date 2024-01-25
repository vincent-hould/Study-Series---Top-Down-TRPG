using System.Collections;
using TopDownTRPG.Assets.Scripts.DialogueSystem;
using TopDownTRPG.Assets.Scripts.Framework;
using UnityEngine;

namespace TopDownTRPG
{
    public class DialogueManager : BaseMonoSingleton<DialogueManager>
    {
        private TypewriterEffect TypewriterEffect;

        protected override void Awake()
        {
            base.Awake();
            TypewriterEffect = GetComponent<TypewriterEffect>();
        }

        public Coroutine Execute(Dialogue dialogue)
        {
            return StartCoroutine(StartDialogue(dialogue));
        }

        public IEnumerator StartDialogue(Dialogue dialogue)
        {
            var dialogueBox = UIManager.Instance.GetDialogueBox();
            dialogueBox.DisplayDialogueBox(dialogue.Actor);

            foreach (var line in dialogue.Lines)
            {
                TypewriterEffect.Write(line, dialogueBox);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }

            dialogueBox.HideDialogueBox();
        }
    }
}
