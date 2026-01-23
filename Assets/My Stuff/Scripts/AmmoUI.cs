using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [Header("UI Settings")]
    public Image[] ammoSlots;

    private int maxAmmo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAmmo = ammoSlots.Length;
        UpdateAmmoDisplay(maxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmoDisplay(int currentAmmo)
    {
        //確保 Ammo 不會出現奇怪的數量
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);

        for (int i = 0; i < maxAmmo; i++)
        {
            // (i < currentAmmo) 回傳一個 bool
            ammoSlots[i].enabled = (i < currentAmmo);
        }
    }
}
