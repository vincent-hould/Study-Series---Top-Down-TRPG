using UnityEngine;

namespace TopDownTRPG
{
    public class Unit : MonoBehaviour, ISelectable
    {
        public delegate void MovementDone(Unit unit);
        public event MovementDone OnMovementDone;

        [SerializeField] private bool Exhausted = false;
        [SerializeField] private int Health = 100;
        [SerializeField] private int Damage = 25;

        public int MovementRange = 4;
        public Faction Faction;
        public bool HasMoved = false;
        private IMover _mover;
        private Animator _animator;

        private void Awake()
        {
            _mover = GetComponent<IMover>();
            _animator = GetComponent<Animator>();
            BattleEventChannelSO.OnUnitRefreshed += OnRefresh;
        }

        private void OnDestroy() => BattleEventChannelSO.OnUnitRefreshed -= OnRefresh;

        public void Destroy() => Destroy(gameObject);

        public void Die() => _animator.SetTrigger("die");

        public void ReceiveHit(int damage)
        {
            _animator.SetTrigger("receiveHit");
            Health = Mathf.Max(Health - damage, 0);
            if (Health == 0)
                Die();
        }

        public void Attack(Unit opponent)
        {
            if (Exhausted)
                return;

            _animator.SetBool("isSelected", false);
            _animator.SetTrigger("attack");
            opponent.ReceiveHit(Damage);
            Exhaust();
        }

        public void Move(Vector3 position)
        {
            if (Exhausted)
                return;

            _animator.SetBool("isSelected", false);
            _animator.SetBool("isWalking", true);
            _mover.Move(position);
        }

        private void Exhaust()
        {
            _animator.SetBool("isExhausted", true);
            Exhausted = true;
        }

        public void OnRefresh()
        {
            _animator.SetBool("isExhausted", false);
            Exhausted = false;
            HasMoved = false;
        }

        public void OnMoveDone()
        {
            _animator.SetBool("isWalking", false);
            HasMoved = true;
            if (OnMovementDone != null)
                OnMovementDone(this);
        }

        public void SetSelected() => _animator.SetBool("isSelected", true);

        public bool CanBeSelected()
        {
            return !Exhausted;
        }
    }
}
