using System.Collections;
using TMPro;
using UnityEngine;

namespace TopDownTRPG
{
    public class Header : MonoBehaviour, IHeader
    {
        [SerializeField] private TMP_Text HeaderText;

        public Coroutine Display(string msg, float duration = 0f)
        {
            HeaderText.text = msg;
            return StartCoroutine(EraseAfterSeconds(duration));
        }

        private IEnumerator EraseAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            HeaderText.text = "";
        }
    }
}
