using UnityEngine;
using UnityEngine.InputSystem.XR;

public class InputHandler : MonoBehaviour
{
    private Animator _animator;
    private WeaponController wpController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        wpController = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            wpController.TryShoot();            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            wpController.TryReload();     
        }      
    }
}
