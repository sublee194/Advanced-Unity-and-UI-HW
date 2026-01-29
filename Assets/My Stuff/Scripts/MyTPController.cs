using UnityEngine;
using UnityEngine.TextCore.Text;
using Game.Character;

public class MyTPController : MonoBehaviour
{
    public MyTPCamera tpCamera;
    public Camera myCamera;
    public float moveSpeed;
    public float rotateSensitivity;

    private Animator _animator;
    private CharacterController _cc;

    [SerializeField] private ControlMode currentMode = ControlMode.FreeMove;
    public ControlMode CurrentMode => currentMode;
    public void SetMode(ControlMode mode)
    {
        currentMode = mode;
    }

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

        if (currentMode == ControlMode.AimMove)
        {
            AimMoveControl(fH, fV, camTransform);
            //UpdateAnimatorMoveNew(fH, fV);
        }
    }

    void AimMoveControl(float fH, float fV, Transform camTransform)
    {
        //鎖定角色轉向：使角色永遠面向 Camera 方向
        Vector3 avatarFacing = camTransform.forward;
        avatarFacing.y = 0;
        transform.rotation = Quaternion.LookRotation(avatarFacing);


        //依照角色前進方向，讀取前後移動量來設定 Animator 裡的 MoveZ、左右移動量來設定 Animator 裡的 MoveX、
        Vector3 moveDir = myCamera.transform.forward * fV + myCamera.transform.right * fH;
        moveDir.y = 0;
        moveDir.Normalize();
        Vector3 localMove = Quaternion.Inverse(transform.rotation) * moveDir;
        localMove.y = 0;
        _animator.SetFloat("MoveX", localMove.x);
        _animator.SetFloat("MoveZ", localMove.z);
    }

    void AimMoveControlNew(float fH, float fV)
    {        
      
        _animator.SetFloat("MoveX", fH);
        _animator.SetFloat("MoveZ", fV);
    }
}
