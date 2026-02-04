using UnityEngine;

public class CameraCullingMask : MonoBehaviour
{
    Camera camera;
    bool IsBelowGround;
    float camheight;
    int layerToIgnore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<Camera>();
        IsBelowGround = false;
        camheight = camera.transform.position.y;
        layerToIgnore = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        camheight = camera.transform.position.y;
        IsBelowGround = CameraYCheck(camheight);
        if (IsBelowGround)
        {
            camera.cullingMask = ~layerToIgnore;
        }
        else
        {
            camera.cullingMask = -1;
        }
        
    }

    bool CameraYCheck(float camheight)
    {
        if (camheight > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
