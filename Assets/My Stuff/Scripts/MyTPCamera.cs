using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86;

public class MyTPCamera : MonoBehaviour
{
    public Transform mFollowPoint;
    public Transform mFollowPointRef;

    public float mFollowDistance;
    //public float mMinFollowDistance;
    //public float mMaxFollowDistance;

    private float mVerticalDegree;
    public float mVerticalLimitUp;
    public float mVerticalLimitDown;

    private Vector3 mHorizontalVector;
    public float mMouseRotateSensitivity = 1.0f;
    public float followSpeed = 10.0f;
    //private Vector3 mCurrentVel = Vector3.zero;
    //public LayerMask mCheckLayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //同步 FollowPoint 和 RefPoint 的位置與轉向
        mFollowPoint.position = mFollowPointRef.position;
        mFollowPoint.rotation = mFollowPointRef.rotation;

        //camera 位置 = 角色位置往後 mFollowDistance 的距離
        transform.position = mFollowPoint.position - mFollowDistance * mFollowPoint.forward;

        //角色朝向 camarea 的向量 (雖然方便反推位置，但這樣方向不是與鏡頭方向相反嗎？)
        Vector3 vDir = transform.position - mFollowPoint.position;
        mHorizontalVector = vDir;
        mHorizontalVector.y = 0.0f;
        mHorizontalVector.Normalize();
    }

    public void UpdateFollowPt()
    {
        mFollowPoint.position = Vector3.Lerp(mFollowPoint.position, mFollowPointRef.position, 1.0f);
        Vector3 vDir = transform.position - mFollowPoint.position;
        vDir.y = 0.0f;
        vDir.Normalize();
        mHorizontalVector = Vector3.Lerp(mHorizontalVector, vDir, 10.0f * Time.deltaTime);
        mHorizontalVector.Normalize();
    }

    private void LateUpdate()
    {
        //取得滑鼠移動值
        float fMX = Input.GetAxis("Mouse X"); //滑鼠水平移動量：回傳 -1 ~ 1
        float fMY = Input.GetAxis("Mouse Y"); //滑鼠垂直移動量：回傳 -1 ~ 1

        //計算對 y 軸旋轉特定度數 (由滑鼠水平移動量決定) 的旋轉量 (單位為 Quaternion)
        Quaternion hRotation = Quaternion.AngleAxis(fMX * mMouseRotateSensitivity, Vector3.up);

        //讓 mHorizontalVector 對 y 軸旋轉該旋轉量 (目前只有水平旋轉，因為 mHorizontalVector.y = 0)
        mHorizontalVector = hRotation * mHorizontalVector;

        //讓 mHorizontalVector 進行垂直旋轉 ==> 不能直接對 x 軸旋轉，而是要對「水平旋轉過後的 mHorizontalVector 的 local x 軸旋轉」
        //呈上，藉由 mHorizontalVector 外積 y 軸取得
        Vector3 rotationAxis = Vector3.Cross(mHorizontalVector, Vector3.up);

        //計算垂直旋轉角度：由滑鼠垂直移動量決定，至於為什麼用 -= ：
        //滑鼠往上 = 角色抬頭 ==> 後方相機位置變「低」：由下往上 render，因此往「下」轉
        //滑鼠往下 = 角色低頭 ==> 後方相機位置變「高」：由上往下 render，因此往「上」轉
        mVerticalDegree = mVerticalDegree - fMY * mMouseRotateSensitivity;
        if (mVerticalDegree < -mVerticalLimitUp)
        {
            //限制抬頭角度 (也就是向下轉的角度)
            //抬頭角度為正，向下轉角度為負，而這裡只要比較角度大小，而非值的大小，因此在其中一個加上負號
            mVerticalDegree = -mVerticalLimitUp;
        }
        else if (mVerticalDegree > mVerticalLimitDown)
        {
            //限制低頭角度 (也就是向「上」轉的角度)
            mVerticalDegree = mVerticalLimitDown;
        }

        //計算「對水平旋轉過後的 mHorizontalVector」的 local 的 x 軸旋轉特定角度的旋轉量 (單位為 Quaternion)
        Quaternion vRotation = Quaternion.AngleAxis(mVerticalDegree, rotationAxis);

        //進行旋轉，得到最終向量 vFinalDir ==> 它是一個由角色看向新 camera 位置的向量
        Vector3 vFinalDir = vRotation * mHorizontalVector;

        //對其進行標準化，因為我們只需要它的「方向」，大小應由 distance 決定 (見下)
        vFinalDir.Normalize();

        //計算 camaera 位置：角色的 FollowPoint 往 vFinalDir 的方向移動 distance 的距離，就是 camera 的最終位置
        Vector3 vFinalPosition = mFollowPoint.position + vFinalDir * mFollowDistance;

        //計算 camera 朝向？
        Vector3 vDir = mFollowPoint.position - vFinalPosition;

        //把 camera 用線性內差的方式移動到先前計算的最終位置
        transform.position = Vector3.Lerp(transform.position, vFinalPosition, 1.0f);
        
        //重新指定 camera 方向
        vDir = mFollowPoint.position - transform.position;
        transform.forward = vDir;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
