using UnityEngine;

namespace TopDownTRPG
{
    public class InstantaneousMover: MonoBehaviour, IMover
    {
        public void Move(Vector2 destination)
        {
            transform.position = destination;
        }
    }
}
