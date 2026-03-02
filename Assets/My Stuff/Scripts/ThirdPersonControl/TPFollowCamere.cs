using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Unity.Burst.Intrinsics.X86;


public class TPFollowCamere : MonoBehaviour
{
    public Transform followTarget;
    public Transform lookTarget;
    public float mFollowDistance;
    public float mMinFollowDistance;
    public float mMaxFollowDistance;
    Vector3 vLastPosition = Vector3.zero;
    public float followSpeed;
    private float fLastDistance;

    public Vector3 targetOffset;
    public Vector3 damping;
    private Vector3 dampingCorrection;

    private float dampEps = 0.0001f;
    const float kLogNegligibleResidual = -4.605170186f; // math.Log(kNegligibleResidual=0.01f);
    private Quaternion followRotation = Quaternion.identity;


    public LayerMask mCheckLayer;
    // Start is called before the first frame update
    void Start()
    {
        vLastPosition = followTarget.position;
        fLastDistance = mFollowDistance;
        dampingCorrection = Vector3.zero;
        followRotation = transform.rotation;
    }

    float DampValue(float initialDamp, float dampTime, float deltaTime)
    {
        // if the delta time and the damp time is to small return the original value
        if (dampTime < dampEps || Mathf.Abs(initialDamp) < dampEps)
            return initialDamp;

        // if the delta time is very small then set it to no damping
        if (deltaTime < dampEps)
            return 0;


        // get constant to calculate exponential
        float k = -kLogNegligibleResidual / dampTime;

        // get the exponential from k*deltaTime and multiply with negative sign and the larger k (smaller damptime) value get a smaller exp result(0 ~ 1)
        float dampResidual = Mathf.Exp(-k * deltaTime);

        // inverse the result (e.g. 0.1 => 0.9) 
        float dampResult = (1 - dampResidual);

        return initialDamp * dampResult;
    }

    Vector3 Damp(Vector3 initial, Vector3 dampTime, float deltaTime)
    {
        for (int i = 0; i < 3; ++i)
            initial[i] = DampValue(initial[i], dampTime[i], deltaTime);
        return initial;
    }

    void CalculateVirtualTargetPoisiton(Vector3 targetPos, Quaternion targetRot, Quaternion targetHRot, out Vector3 hand)
    {
        var offset = targetOffset;
        offset.x = Mathf.Lerp(-targetOffset.x, targetOffset.x, 1);
        offset.x += dampingCorrection.x;
        offset.y += dampingCorrection.y;
        var shoulder = targetPos + targetHRot * offset;
        hand = shoulder + targetRot * new Vector3(0, 0.4f, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaTime = Time.deltaTime;
        var up = Vector3.up;
        var targetPos = followTarget.position;
        var targetRot = followTarget.rotation;
        var targetForward = targetRot * Vector3.forward; // followTarget.forward
        var horizontalTargetRot = GetTargetHRoation(targetRot, up);

        //Quaternion.Inverse(followTarget.rotation) * (vLastPosition - targetPos);

        dampingCorrection += Quaternion.Inverse(horizontalTargetRot) * (vLastPosition - targetPos);
        dampingCorrection -= Damp(dampingCorrection, damping, deltaTime);
        vLastPosition = targetPos;

        // calculate new target position with offset and damping
        Vector3 virtualPos = Vector3.zero;
        CalculateVirtualTargetPoisiton(targetPos, targetRot, horizontalTargetRot, out virtualPos);

        // Place the camera at the correct distance from the hand
        var camPos = virtualPos - (targetForward * (mFollowDistance - dampingCorrection.z));
       
        transform.position = camPos;
        transform.rotation = targetRot;

        Vector3 rayOrigin = virtualPos;
        if (lookTarget != null)
        {
            transform.LookAt(lookTarget);
            rayOrigin = lookTarget.position;
        }

        Vector3 vDir = transform.forward;

        vDir.Normalize();
        RaycastHit rh;
        Ray r = new Ray(rayOrigin, -vDir);

        if (Physics.SphereCast(r, 0.1f, out rh, mFollowDistance, mCheckLayer))
        {
            transform.position = rayOrigin - vDir * (rh.distance - 0.1f);
        }
    }

    internal Quaternion GetTargetHRoation(Quaternion targetRot, Vector3 up)
    {
        var targetForward = targetRot * Vector3.forward;

        Vector3 horizontalForward = targetForward;
        horizontalForward.y = 0.0f;
        horizontalForward.Normalize();
        return Quaternion.LookRotation(horizontalForward, up);
    }

    private void OnDrawGizmos()
    { 

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(vLastPosition, 1.0f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
}
