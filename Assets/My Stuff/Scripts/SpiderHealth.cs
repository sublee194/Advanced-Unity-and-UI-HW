using Game.Combat;
using UnityEngine;

public class SpiderHealth : MonoBehaviour, IDamageReceiver
{
    [SerializeField] float hp = 50f;
    [SerializeField] float armor = 5f;

    public DefenseData GetDefenseValue()
    {
        return new DefenseData
        {
            Armor = armor
        };
    }

    public void ReceiveDamage(DamageResult result)
    {
        hp -= result.FinalDamage;
        Debug.Log($"Spider get damaged, current hp: {hp}");

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Reset()
    {
        //重置生命？
    }
}
