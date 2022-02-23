using UnityEngine;

namespace TopDownTRRPG.BattleSystem.SelectionSystem
{
    public class InstantaneousMover: MonoBehaviour, IMover
    {
        public void Move(Vector2 destination)
        {
            transform.position = destination;
        }
    }
}
