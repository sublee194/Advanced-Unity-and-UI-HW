using Game.Combat;
using System.Collections;
using UnityEngine;

public class SpiderHealth : MonoBehaviour, IDamageReceiver
{   
    [SerializeField] float initalHp = 25f;
    private float hp;
    [SerializeField] float armor = 5f;

    [SerializeField] private PoolType poolType; //這隻蜘蛛的種類

    private Animator _animator;

    void Awake()
    {
        hp = initalHp;
        _animator = GetComponent<Animator>();
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
        Debug.Log($"Spider get damaged, current hp: {hp}");

        if (hp <= 0)
        {
            _animator.SetTrigger("Damaged");
            StartCoroutine(Die());
        }
        else
        {
            _animator.SetTrigger("Damaged");
        }
            
    }

    IEnumerator Die()
    {
        _animator.SetBool("Dead", true);
        yield return new WaitForSecondsRealtime(10.0f);
        ObjectPool.Instance().ReturnPrefabToPool(poolType, gameObject);
    }

    public void Reset()
    {
        hp = initalHp;
    }
}
