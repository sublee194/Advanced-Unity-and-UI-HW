using UnityEngine;

namespace Game.Combat
{
    public enum DamageType { Physical, Explosive }

    public struct DamageData
    {
        public float BaseDamage;
        public DamageType Type;
        public float CritChance;
        public float CritMultiplier;
    }
}
