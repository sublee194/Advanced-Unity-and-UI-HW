using UnityEngine;
using Game.Combat;

public class CarHealth : MonoBehaviour, IDamageReceiver
{
    [SerializeField] float hp = 100f;
    [SerializeField] float armor = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
