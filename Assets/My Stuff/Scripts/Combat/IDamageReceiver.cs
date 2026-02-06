using UnityEngine;

namespace Game.Combat
{
    public interface IDamageReceiver
    {
        DefenseData GetDefenseValue();
        void ReceiveDamage(DamageResult result);
    }
}
