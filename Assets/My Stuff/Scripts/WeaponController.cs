using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int ammo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammo = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpendAmmo()
    {
        if (ammo > 0)
        {
            ammo -= 1;
        }

        Debug.Log($"Ammo Count: {ammo}");

    }
}
