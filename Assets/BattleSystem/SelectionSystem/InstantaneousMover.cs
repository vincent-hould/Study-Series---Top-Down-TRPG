using UnityEngine;

namespace TopDownTRPG
{
    public class InstantaneousMover: MonoBehaviour, IMover
    {
        public void Move(Vector3 destination)
        {
            transform.position = destination;
        }
    }
}
