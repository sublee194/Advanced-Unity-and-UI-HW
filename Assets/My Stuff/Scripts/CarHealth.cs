using UnityEngine;
using Game.Combat;

public class CarHealth : MonoBehaviour, IDamageReceiver, IResettable
{
    [SerializeField] float hp = 100f;
    [SerializeField] float armor = 5f;

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
        Debug.Log($"Car get damaged, current hp: {hp}");

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
