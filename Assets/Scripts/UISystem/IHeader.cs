using UnityEngine;

namespace TopDownTRPG
{
    public interface IHeader
    {
        public Coroutine Display(string msg, float duration = 0f);
    }
}
