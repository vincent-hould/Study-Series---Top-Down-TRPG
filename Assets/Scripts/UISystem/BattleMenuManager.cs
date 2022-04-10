using UnityEngine;
using UnityEngine.UI;

namespace TopDownTRPG
{
    public class BattleMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject ContextualMenuPanel;
        [SerializeField] private Button AttackButton;
        [SerializeField] private Button MoveButton;
        [SerializeField] private Button EndTurnButton;
        [SerializeField] private float DistanceFromOrigin = 50f;

        private bool attackButtonEnabled;
        private bool moveButtonEnabled;

        public void DisplayMenu(Vector3 origin, bool canAttack, bool canMove)
        {
            attackButtonEnabled = canAttack;
            moveButtonEnabled = canMove;
            AttackButton.interactable = canAttack;
            MoveButton.interactable = canMove;
            ContextualMenuPanel.transform.position = GetMenuPosition(origin);
            ContextualMenuPanel.SetActive(true);
            if (canAttack)
                AttackButton.Select();
            else if (canMove)
                MoveButton.Select();
            else
                EndTurnButton.Select();
        }

        private Vector3 GetMenuPosition(Vector3 worldOrigin)
        {
            Camera camera = Helpers.Camera;
            Vector3 originScreenPoint = camera.WorldToScreenPoint(worldOrigin);
            Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
            originScreenPoint.x += originScreenPoint.x >= screenCenter.x ? -DistanceFromOrigin : DistanceFromOrigin;
            originScreenPoint.y += originScreenPoint.y >= screenCenter.y ? -DistanceFromOrigin : DistanceFromOrigin;
            return originScreenPoint;
        }

        public void HideMenu() => ContextualMenuPanel.SetActive(false);

        public void Attack()
        {
            if (attackButtonEnabled)
                BattleEventChannelSO.RaiseAttackRequested();
        }

        public void Move()
        {
            if (moveButtonEnabled)
                BattleEventChannelSO.RaiseMoveRequested();
        }

        public void EndTurn() => BattleEventChannelSO.RaiseEndTurnRequested();
    }
}
