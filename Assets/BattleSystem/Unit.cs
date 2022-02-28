using UnityEngine;

namespace TopDownTRPG
{
    public class Unit : MonoBehaviour
    {
        public delegate void MovementDone(Unit unit);
        public event MovementDone OnMovementDone;

        [SerializeField]
        private bool _exhausted = false;

        private IMover _mover;
        private Animator _animator;

        private void Awake()
        {
            _mover = GetComponent<IMover>();
            _animator = GetComponent<Animator>();
            BattleEventChannelSO.OnUnitRefreshed += OnRefresh;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void Die()
        {
            _animator.SetTrigger("die");
        }

        public void ReceiveHit()
        {
            _animator.SetTrigger("receiveHit");
        }

        public void Attack(Unit opponent)
        {
            if (_exhausted) return;

            _animator.SetBool("isSelected", false);
            _animator.SetTrigger("attack");
            opponent.ReceiveHit();
            Exhaust();
        }

        public void Move(Vector3 position)
        {
            if (_exhausted) return;

            _animator.SetBool("isSelected", false);
            _animator.SetBool("isWalking", true);
            _mover.Move(position);
        }

        private void Exhaust()
        {
            _animator.SetBool("isExhausted", true);
            _exhausted = true;
        }

        public void OnRefresh()
        {
            _animator.SetBool("isExhausted", false);
            _exhausted = false;
        }

        public void OnMoveDone()
        {
            _animator.SetBool("isWalking", false);
            Exhaust();
            if (OnMovementDone != null)
            {
                OnMovementDone(this);
            }
        }

        public void SetSelected()
        {
            _animator.SetBool("isSelected", true);
        }
    }
}
