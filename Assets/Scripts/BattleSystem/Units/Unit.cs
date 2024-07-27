using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class Unit : MonoBehaviour, ISelectable
    {
        public static List<Unit> SpawnedUnits = new List<Unit>();

        public delegate void MovementDone(Unit unit);
        public event MovementDone OnMovementDone;

        public Faction Faction;
        public UnitType Type;
        public UnitTrait Trait;
        public UnitTrait CaptainTrait;

        public Node CurrentNode { get; set; }
        public int MovementRange => Type.MovementRange;
        public int Health { get; private set; }
        public int Damage => Type.Damage;
        public bool IsCaptain => CaptainTrait != null;

        private bool _exhausted = false;
        private bool _hasMoved = false;
        private IMover _mover;
        private Animator _animator;

        private void Awake()
        {
            _mover = GetComponent<IMover>();
            _animator = GetComponent<Animator>();
            BattleEventChannelSO.OnUnitRefreshed += OnRefresh;
        }

        private void Start()
        {
            Health = Type.MaxHealth;
            SpawnedUnits.Add(this);
            BattleEventChannelSO.RaiseUnitSpawned(this);
        }

        private void OnDestroy()
        {
            BattleEventChannelSO.OnUnitRefreshed -= OnRefresh;
            BattleEventChannelSO.RaiseUnitKilled(this);
            SpawnedUnits.Remove(this);
        }

        public bool CanBeSelected() => !_exhausted;

        public bool CanMove() => CanBeSelected() && !_hasMoved;

        public void SetSelected(bool selected = true) => _animator.SetBool("isSelected", selected);

        public void Destroy() => Destroy(gameObject);

        public void ReceiveHit(int damage)
        {
            _animator.SetTrigger("receiveHit");
            Health = Mathf.Max(Type.MaxHealth - damage, 0);
            if (Health == 0)
                Die();
        }

        private void Die() => _animator.SetTrigger("die");

        public void Attack(Unit opponent)
        {
            if (_exhausted)
                return;

            _animator.SetBool("isSelected", false);
            _animator.SetTrigger("attack");
            opponent.ReceiveHit(Type.Damage);
            Exhaust();
        }

        public void Move(Vector3 position)
        {
            if (_exhausted)
                return;

            _animator.SetBool("isSelected", false);
            _animator.SetBool("isWalking", true);
            BattleEventChannelSO.RaiseUnitMoveStarted(transform);
            _mover.Move(position);
        }

        public void Exhaust()
        {
            _animator.SetBool("isExhausted", true);
            _exhausted = true;
        }

        public void OnRefresh()
        {
            _animator.SetBool("isExhausted", false);
            _animator.SetBool("isSelected", false);
            _exhausted = false;
            _hasMoved = false;
        }

        public void OnMoveDone()
        {
            _animator.SetBool("isWalking", false);
            BattleEventChannelSO.RaiseUnitMoveEnded(this);
            _hasMoved = true;
            OnMovementDone?.Invoke(this);
        }

        public CursorConstraint GetAttackCursorConstraint()
        {
            return Type.GetAttackCursorConstraint(this);
        }

        public CursorConstraint GetMoveCursorConstraint()
        {
            return Type.GetMoveCursorConstraint(this);
        }
    }
}
