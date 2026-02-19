using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPController : MonoBehaviour
{
    public TPCamera tpCamera;
    public float moveSpeed;
    public float rotateSensitivity;

    private Animator _animator;
    private CharacterController _cc;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float fH = Input.GetAxis("Horizontal");
        float fV = Input.GetAxis("Vertical");
        
        Transform camTransform = tpCamera.transform;

        // get move direction
        Vector3 moveDirection = camTransform.right * fH + camTransform.forward * fV;

        // remove y element to keep horizontal
        moveDirection.y = 0;

        // for uniychan sample
        _animator.SetFloat("Direction", 0.0f);

        // check if there is move direction
        if (moveDirection != Vector3.zero)
        {
            // for uniychan sample
            _animator.SetFloat("Speed", moveDirection.magnitude);

            // rotate the character to align the moveDirection
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSensitivity * Time.deltaTime);//Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSensitivity * Time.deltaTime);


        } else
        {
            // for uniychan sample
            _animator.SetFloat("Speed", 0.0f);
        }

        // for character controller
        _cc.Move(moveDirection * moveSpeed * Time.deltaTime);
         tpCamera.UpdateFollowPt();

    }
}
