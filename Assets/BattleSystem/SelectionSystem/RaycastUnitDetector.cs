using UnityEngine;

namespace TopDownTRRPG.BattleSystem.SelectionSystem
{
    public class RaycastUnitDetector : MonoBehaviour
    {
        public GameObject FindUnit(Vector3 position)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, -Vector2.up);
            if (hit.collider != null)
            {
                return hit.transform.gameObject;
            }

            return null;
        }
    }
}
