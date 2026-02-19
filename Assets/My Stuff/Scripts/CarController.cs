using UnityEngine;

public class CarController : MonoBehaviour
{
    //目前沒在使用中？
    int hp = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetDamaged(int damage)
    {
        hp = Mathf.Max(0, hp - damage);
    }
}
