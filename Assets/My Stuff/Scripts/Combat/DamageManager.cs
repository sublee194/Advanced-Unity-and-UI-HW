using UnityEngine;

namespace Game.Combat
{
    public static class DamageManager
    {
        public static void InflictDamage(IDamageSource source, IDamageReceiver receiver)
        {
            var damage = source.GetDamageValue();
            var defense = receiver.GetDefenseValue();

            float final = Mathf.Max(0, damage.BaseDamage - defense.Armor);

            bool crit = Random.value < damage.CritChance;
            if (crit)
                final *= damage.CritMultiplier;

            receiver.ReceiveDamage(new DamageResult
            {
                FinalDamage = final,
                IsCritical = crit
            });
        }
    }
}
