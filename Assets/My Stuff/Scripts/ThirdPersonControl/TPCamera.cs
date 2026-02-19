using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCamera : MonoBehaviour
{
    public Transform mFollowPoint;
    public Transform mFollowPointRef;

    public float mFollowDistance;
    public float mMinFollowDistance;
    public float mMaxFollowDistance;

    private float mVerticalDegree;
    public float mVerticalLimitUp;
    public float mVerticalLimitDown;

    private Vector3 mHorizontalVector;
    public float mMouseRotateSensitivity = 1.0f;
    public float followSpeed = 10.0f;
    private Vector3 mCurrentVel = Vector3.zero;
    public LayerMask mCheckLayer;
    // Start is called before the first frame update
    void Start()
    {
        mFollowPoint.position = mFollowPointRef.position;
        mFollowPoint.rotation = mFollowPointRef.rotation;
        transform.position = mFollowPoint.position - mFollowDistance * mFollowPoint.forward;
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
        mHorizontalVector = Vector3.Lerp(mHorizontalVector, vDir, 10.0f*Time.deltaTime);
        mHorizontalVector.Normalize();
    }

    private void LateUpdate()
    {
        float fMX = Input.GetAxis("Mouse X");
        float fMY = Input.GetAxis("Mouse Y");

        mHorizontalVector = Quaternion.AngleAxis(fMX*mMouseRotateSensitivity, Vector3.up) * mHorizontalVector;
        Vector3 rotationAxis = Vector3.Cross(mHorizontalVector, Vector3.up);
        mVerticalDegree -= fMY * mMouseRotateSensitivity;
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
        //mFollowPoint.position = Vector3.SmoothDamp(mFollowPoint.position, mFollowPointRef.position, ref mCurrentVel, 1.0f, 10.0f);
        //mFollowPoint.position = Vector3.Lerp(mFollowPoint.position, mFollowPointRef.position, 1.0f);
        Vector3 vFinalPosition = mFollowPoint.position + vFinalDir * mFollowDistance;
        Vector3 vDir = mFollowPoint.position - vFinalPosition;
       
        vDir.Normalize();
        RaycastHit rh;
        Ray r = new Ray(mFollowPoint.position, -vDir);

        if (Physics.SphereCast(r, 0.1f, out rh, mFollowDistance, mCheckLayer))
        {
            vFinalPosition = mFollowPoint.position - vDir * (rh.distance - 0.1f);
        }

        //if (Physics.Linecast(mFollowPoint.position, vFinalPosition, out rh, mCheckLayer))
        //{
        //    //if(rh.distance < 2.0f)
        //    //{ddd

        //    //}
        //    Vector3 vHit = rh.point + vDir * 0.1f;
        //    vFinalPosition = vHit;
        //}
        transform.position = Vector3.Lerp(transform.position, vFinalPosition,1.0f);
        //transform.position = Vector3.SmoothDamp(transform.position, vFinalPosition, ref mCurrentVel, 0.001f, 10.0f);
        //transform.position = vFinalPosition;
        vDir = mFollowPoint.position - transform.position;
        transform.forward = vDir;

    }
}
