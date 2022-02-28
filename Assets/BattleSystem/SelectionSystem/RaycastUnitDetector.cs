using UnityEngine;

namespace TopDownTRPG
{
    public class RaycastUnitDetector : MonoBehaviour, IUnitDetector
    {
        public Unit FindUnit(Vector3 position)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.up, 0f);
            if (hit.collider != null)
            {
                return hit.transform.gameObject.GetComponent<Unit>();
            }

            return null;
        }
    }
}
