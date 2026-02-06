using UnityEngine;

namespace Game.Combat
{
    public interface IDamageSource
    {
        DamageData GetDamageValue();
    }
}
