using System.Collections;
using TMPro;
using UnityEngine;

namespace TopDownTRPG
{
    public class TextManager : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Display(string msg, float duration = 0f)
        {
            _text.text = msg;
            if (duration > 0f)
            {
                StartCoroutine(EraseAfterSeconds(duration));
            }
        }

        private IEnumerator EraseAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _text.text = "";
        }
    }
}
