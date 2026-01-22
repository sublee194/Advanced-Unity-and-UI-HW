using UnityEngine;

public class MyTPController : MonoBehaviour
{
    public MyTPCamera tpCamera;
    public float moveSpeed;
    public float rotateSensitivity;

    private Animator _animator;
    private CharacterController _cc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //鍵盤上下左右 input
        float fH = Input.GetAxis("Horizontal"); //回傳 -1 ~ 1
        float fV = Input.GetAxis("Vertical"); //回傳 -1 ~ 1

        //取得攝影機位置
        Transform camTransform = tpCamera.transform;

        //取得移動方向
        Vector3 moveDirection = camTransform.right * fH + camTransform.forward * fV;

        //禁止垂直移動
        moveDirection.y = 0;

        if (moveDirection != Vector3.zero)
        {
            // 改變模型的 Speed 參數 (調整動畫用)：
            _animator.SetFloat("Speed", moveDirection.magnitude); 

            // 將角色轉向移動方向
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSensitivity * Time.deltaTime);//Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSensitivity * Time.deltaTime);
        }
        else
        {
            // 改變模型的 Speed 參數 (調整動畫用)： 
            _animator.SetFloat("Speed", 0.0f);
        }

        _cc.Move(moveDirection * moveSpeed * Time.deltaTime);
        tpCamera.UpdateFollowPt();        
    }


}
