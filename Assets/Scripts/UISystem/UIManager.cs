using System.Collections;
using UnityEngine;
using TMPro;

namespace TopDownTRPG
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text HeaderText;

        private BattleMenuManager battleMenuManager;

        private void Awake()
        {
            battleMenuManager = GetComponent<BattleMenuManager>();
            UIEventChannelSO.OnContextualMenuDisplayRequested += DisplayMenu;
            UIEventChannelSO.OnContextualMenuHideRequested += HideMenu;
            UIEventChannelSO.OnHeaderTextRequested += DisplayHeader;
        }

        private void OnDestroy()
        {
            UIEventChannelSO.OnContextualMenuDisplayRequested -= DisplayMenu;
            UIEventChannelSO.OnContextualMenuHideRequested -= HideMenu;
            UIEventChannelSO.OnHeaderTextRequested -= DisplayHeader;
        }

        private void DisplayMenu(Vector3 position, bool canAttack, bool canMove)
        {
            battleMenuManager.DisplayMenu(position, canAttack, canMove);
        }

        private void HideMenu() => battleMenuManager.HideMenu();

        private void DisplayHeader(string msg, float duration = 0f)
        {
            HeaderText.text = msg;
            if (duration > 0f)
                StartCoroutine(EraseAfterSeconds(duration));
        }

        private IEnumerator EraseAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            HeaderText.text = "";
        }
    }
}
