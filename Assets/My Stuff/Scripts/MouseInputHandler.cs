using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    private Animator _animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("IsShooting");
        }
    }


}
