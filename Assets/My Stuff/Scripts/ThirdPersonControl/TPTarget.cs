using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPTarget : MonoBehaviour
{
    public Transform target;
    public Transform refTarget;
    public Vector3 vLastForward = Vector3.zero;
    private float mVerticalDegree;
    public float mVerticalLimitUp;
    public float mVerticalLimitDown;

    private Vector3 mHorizontalVector;
    public float mMouseRotateSensitivity = 100.0f;

    public Transform lookTarget;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        target.position = refTarget.position;
        target.forward = refTarget.forward;
        Vector3 vDir = target.forward;
        vLastForward = vDir;
        mHorizontalVector = vDir;
        mHorizontalVector.y = 0.0f;
        mHorizontalVector.Normalize();
    }



    // Update is called once per frame
    void Update()
    {
        target.position = refTarget.position;


        float fMX = Input.GetAxis("Mouse X");
        float fMY = Input.GetAxis("Mouse Y");
       // Debug.Log(fMX);

        mHorizontalVector = Quaternion.AngleAxis(fMX * mMouseRotateSensitivity /** Time.deltaTime*/, Vector3.up) * mHorizontalVector;


        if (lookTarget != null)
        {
            mHorizontalVector = lookTarget.position - target.position;
            mHorizontalVector.y = 0.0f;
            mHorizontalVector.Normalize();
        }

        Vector3 rotationAxis = Vector3.Cross(mHorizontalVector, Vector3.up);
        mVerticalDegree += fMY * mMouseRotateSensitivity /** Time.deltaTime*/;
        if (mVerticalDegree < -mVerticalLimitUp)
        {
            mVerticalDegree = -mVerticalLimitUp;
        }
        else if (mVerticalDegree > mVerticalLimitDown)
        {
            mVerticalDegree = mVerticalLimitDown;
        }
        Vector3 vFinalDir = Quaternion.AngleAxis(mVerticalDegree, rotationAxis) * mHorizontalVector;
        vFinalDir.Normalize();

        Quaternion qRot = Quaternion.LookRotation(vFinalDir);
        target.rotation = Quaternion.RotateTowards(target.rotation, qRot, 500 * Time.deltaTime);
        // transform.rotation = Quaternion.Lerp(transform.rotation, qRot, );

        //在這裡加入角色旋轉？
        //Vector3 avatarFacing;
        //avatarFacing = transform.forward;
        //avatarFacing.y = 0.0f;
        //player.transform.rotation = Quaternion.LookRotation(avatarFacing);

        //vLastForward = target.forward;
        //target.forward = vFinalDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(target.position, target.position + target.forward*2.0f);
        if(lookTarget != null)
        {
            Gizmos.DrawWireSphere(lookTarget.position, 0.1f);
        }
    }
}
