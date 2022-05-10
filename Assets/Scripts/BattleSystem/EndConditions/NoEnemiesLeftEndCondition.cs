using UnityEngine;
using System.Linq;

namespace TopDownTRPG
{
    public class NoEnemiesLeftEndCondition : EndCondition
    {
        [SerializeField] private Faction Faction;

        public override bool IsConditionMet(BattleStateMachine battleStateMachine)
        {
            foreach(Faction enemyFaction in Faction.Enemies)
            {
                // TODO this is not optimal at all. I would need to keep track of existing units of each faction instead
                if (FindObjectsOfType<Unit>().Where(unit => Faction.Enemies.Contains(unit.Faction)).Any())
                    return false;
            }
            
            return true;
        }
    }
}
