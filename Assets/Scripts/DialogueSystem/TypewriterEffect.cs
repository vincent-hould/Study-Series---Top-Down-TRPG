using System.Collections;
using TopDownTRPG.Assets.Scripts.UISystem;
using UnityEngine;

namespace TopDownTRPG.Assets.Scripts.DialogueSystem
{
    public class TypewriterEffect : MonoBehaviour
    {
        [SerializeField] private float writeSpeed = 20f;

        public void Write(string text, IDialogueBox dialogueBox)
        {
            StartCoroutine(TypeText(text, dialogueBox));
        }

        private IEnumerator TypeText(string text, IDialogueBox dialogueBox)
        {
            float t = 0;
            int charIndex = 0;
            while (charIndex < text.Length)
            {
                t += Time.deltaTime * writeSpeed;
                charIndex = Mathf.FloorToInt(t);
                charIndex = Mathf.Clamp(charIndex, 0, text.Length);

                dialogueBox.PrintText(text.Substring(0, charIndex));

                yield return null;
            }

            dialogueBox.PrintText(text);
        }
    }
}