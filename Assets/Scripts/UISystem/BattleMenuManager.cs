using UnityEngine;

namespace TopDownTRPG
{
    public class BattleMenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject ContextualMenuPanel;

        public void DisplayMenu(Vector3 position)
        {
            var menuPosition = position + Vector3.right * 2;
            ContextualMenuPanel.transform.position = Camera.main.WorldToScreenPoint(menuPosition);
            ContextualMenuPanel.SetActive(true);
        }

        public void HideMenu()
        {
            ContextualMenuPanel.SetActive(false);
        }

        public void Attack()
        {
            BattleEventChannelSO.RaiseAttackRequested();
        }

        public void Move()
        {
            BattleEventChannelSO.RaiseMoveRequested();
        }

        public void EndTurn()
        {
            BattleEventChannelSO.RaiseEndTurnRequested();
        }
    }
}
