using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [Header("UI Settings")]
    public Image[] ammoSlots;
    private int maxAmmo;
    private int maxMagazine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAmmo = ammoSlots.Length;
        maxMagazine = 5; //之後改掉
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

    public void ShowNoAmmoMessage()
    {
        Debug.Log("No Ammo!");
    }

    public void UpdateMagazineDisplay(int currentMagazine)
    {        
        currentMagazine = Mathf.Clamp(currentMagazine, 0, maxMagazine);

        Debug.Log("Update Magazine Display");

        // 等有 magazineSlots 再加上來
        //for (int i = 0; i < maxMagazine; i++)
        //{       
        //    magazineSlots[i].enabled = (i < currentMagazine); 
        //}
    }

    public void ShowNoMagazineMessage()
    {
        Debug.Log("No Magazine!");
    }
}
