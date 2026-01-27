using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WeaponController : MonoBehaviour
{
    private Animator _animator;
    private int currentAmmo;
    private int currentMagazine;
    public AmmoUI ammoUI;
    public bool cannotShoot;
    public bool cannotReload;

    public CrosshairUI crosshair;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        currentAmmo = 10;
        currentMagazine = 5;
        cannotShoot = false;
        cannotReload = false;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void TryShoot()
    {
        if (currentAmmo > 0)
        {
            if (cannotShoot)
            {
                Debug.Log("Can't Shoot Now!");
                return;
            }

            _animator.SetTrigger("IsShooting");
            currentAmmo -= 1;
            ammoUI.UpdateAmmoDisplay(currentAmmo);
        }
        else
        {
            ammoUI.ShowNoAmmoMessage();
        }

        //Debug.Log($"Ammo Count: {currentAmmo}");
    }
    public void StartShoot()
    {
        //這個函式被 Shoot_SingleShot_copy animation 的事件呼叫
        Debug.Log("Start Shoot!");
        cannotReload = true;
        crosshair.ChangeIsShooting();
    }

    public void EndShoot()
    {
        //這個函式被 Shoot_SingleShot_copy animation 的事件呼叫
        Debug.Log("End Shoot!");
        cannotReload = false;
        crosshair.ChangeIsShooting();
    }
    public void TryReload()
    {
        if (currentMagazine > 0)
        {
            if (cannotReload)
            {
                _animator.ResetTrigger("IsReloading");
                Debug.Log("Can't Reload Now!");
                return;
            }

            _animator.SetTrigger("IsReloading");

            currentMagazine -= 1;
            currentAmmo = 10;
            ammoUI.UpdateMagazineDisplay(currentMagazine);
            ammoUI.UpdateAmmoDisplay(currentAmmo);
        }
        else
        {
            ammoUI.ShowNoMagazineMessage();
        }

        Debug.Log($"Magazine Count: {currentMagazine}");
    }

    public void StartReload()
    {
        //這個函式被 Reload_copy animation 的事件呼叫
        Debug.Log("Start Reload");
        cannotShoot = true;
    }

    public void EndReload()
    {
        //這個函式被 Reload_copy animation 的事件呼叫
        Debug.Log("End Reload");
        cannotShoot = false;
    }
} 
