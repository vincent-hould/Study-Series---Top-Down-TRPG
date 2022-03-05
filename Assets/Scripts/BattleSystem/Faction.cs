using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    [CreateAssetMenu(fileName = "NewFaction", menuName = "Create a faction")]
    public class Faction : ScriptableObject
    {
        public string Name;
        public Color Color;
        public bool Controllable;

        public List<Faction> Allies;
        public List<Faction> Enemies;

        public bool CanAttack(Faction faction)
        {
            return Enemies.Contains(faction);
        }
    }
}
