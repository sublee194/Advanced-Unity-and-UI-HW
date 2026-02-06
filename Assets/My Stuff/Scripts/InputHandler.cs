using UnityEngine;
using UnityEngine.InputSystem.XR;
using Game.Character;

public class InputHandler : MonoBehaviour
{
    private Animator _animator;
    private WeaponController wpController;
    private MyTPController tpContoller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        wpController = GetComponent<WeaponController>();
        tpContoller = GetComponent<MyTPController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(tpContoller.CurrentMode == ControlMode.AimMove)
            {
                wpController.TryShoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            wpController.TryReload();     
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //切換 Animator 撥放的動作
            bool aimMode = _animator.GetBool("AimMove");
            if(aimMode)
            {
                _animator.SetBool("AimMove", false);
            }
            else
            {
                _animator.SetBool("AimMove", true);
            }

            //控制實際腳本中的模式
            //與上面的結合？？
            if (tpContoller.CurrentMode == ControlMode.FreeMove)
            {
                tpContoller.SetMode(ControlMode.AimMove);
            }
            else
            {
                tpContoller.SetMode(ControlMode.FreeMove);
            }
        }
    }
}
