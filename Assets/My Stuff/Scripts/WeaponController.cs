using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int currentAmmo;
    public AmmoUI ammoUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpendAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo -= 1;
            ammoUI.UpdateAmmoDisplay(currentAmmo);
        }

        Debug.Log($"Ammo Count: {currentAmmo}");

    }
}
