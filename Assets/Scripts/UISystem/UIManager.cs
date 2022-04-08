using System.Collections;
using UnityEngine;
using TMPro;

namespace TopDownTRPG
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text HeaderText;
        [SerializeField]
        private GameObject ContextualMenuPanel;

        private void Awake()
        {
            UIEventChannelSO.OnContextualMenuDisplayRequested += DisplayMenu;
            UIEventChannelSO.OnContextualMenuHideRequested += HideMenu;
            UIEventChannelSO.OnHeaderTextRequested += DisplayHeader;
        }

        private void DisplayMenu(Vector3 position)
        {
            var menuPosition = position + Vector3.right * 2;
            ContextualMenuPanel.SetActive(true);
            ContextualMenuPanel.transform.position = Camera.main.WorldToScreenPoint(menuPosition);
        }

        private void HideMenu()
        {
            ContextualMenuPanel.SetActive(false);
        }

        private void DisplayHeader(string msg, float duration = 0f)
        {
            HeaderText.text = msg;
            if (duration > 0f)
            {
                StartCoroutine(EraseAfterSeconds(duration));
            }
        }

        private IEnumerator EraseAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            HeaderText.text = "";
        }
    }
}
