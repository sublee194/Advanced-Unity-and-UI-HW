using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    public BoxCollider worldBoundsCollider;
    Vector2 worldMin;
    Vector2 worldMax;

    public Transform player;
    Vector3 p; //player

    float u;
    float v;

    float windowSize = 0.25f;

    public RawImage mapImage;

    public Image iconRect;

    public Transform cameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bounds b = worldBoundsCollider.bounds;

        worldMin = new Vector2(b.min.x, b.min.z);
        worldMax = new Vector2(b.max.x, b.max.z);

        p = player.position;

        float u = Mathf.InverseLerp(worldMin.x, worldMax.x, p.x);
        float v = Mathf.InverseLerp(worldMin.y, worldMax.y, p.z);

    }

    // Update is called once per frame
    void Update()
    {
        p = player.position;
        u = Mathf.InverseLerp(worldMin.x, worldMax.x, p.x);
        v = Mathf.InverseLerp(worldMin.y, worldMax.y, p.z);
    }

    void LateUpdate()
    {
        float half = windowSize * 0.5f;

        Rect r = mapImage.uvRect;

        r.x = Mathf.Clamp(u - half, 0, 1 - windowSize);
        r.y = Mathf.Clamp(v - half, 0, 1 - windowSize);
        r.width = windowSize;
        r.height = windowSize;

        mapImage.uvRect = r;
        //iconRect.transform.rotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);
        float yaw = cameraTransform.eulerAngles.y;
        mapImage.rectTransform.localRotation = Quaternion.Euler(0, 0, yaw);
    }
}
