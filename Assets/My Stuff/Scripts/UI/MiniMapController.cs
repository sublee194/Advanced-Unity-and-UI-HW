using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    public BoxCollider worldBoundsCollider;
    Vector2 worldMin;
    Vector2 worldMax;

    public Transform player;
    Vector3 playerPos; //player

    //Minimap's UV
    float u;
    float v;

   public float windowSize; // 0.25 ~ 0.4

    public RawImage mapImage;

    public Image avatarIcon;

    public Transform cameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bounds bounds = worldBoundsCollider.bounds;

        worldMin = new Vector2(bounds.min.x, bounds.min.z);
        worldMax = new Vector2(bounds.max.x, bounds.max.z);

        playerPos = player.position;

        //InverseLerp: calculates the fraction with give start point, end point, and a point between them
        //For example: start ponit: 0, end point: 100, point in-between: 70 --> return 0.7 (because 70 / (100 - 0) )
        //Use it to calucate the U nad V coordinate according to where the avatar is at
        float u = Mathf.InverseLerp(worldMin.x, worldMax.x, playerPos.x);
        float v = Mathf.InverseLerp(worldMin.y, worldMax.y, playerPos.z);

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.position;
        u = Mathf.InverseLerp(worldMin.x, worldMax.x, playerPos.x);
        v = Mathf.InverseLerp(worldMin.y, worldMax.y, playerPos.z);
    }

    void LateUpdate()
    {
        float half = windowSize * 0.5f;

        //Rect in a struct with 4 parameters:
        //1st and 2nd: (x, y) : the left (x) and bottom (y) portion of the UV texture that will be "cut out"
        //For example (0.25, 025) --> the left 25% of the texture will be cut only showing the 75% of the right part
        //3rd and 4th: (width, height) : the size of the window --> the center of the winder is the player's (u, v)
        //Player's (u, v) is not a part of the parameter!!! You use it and (width, height) to calcuate (x, y), not the other way round 
        Rect r = mapImage.uvRect;

        r.x = Mathf.Clamp(u - half, 0, 1 - windowSize);
        r.y = Mathf.Clamp(v - half, 0, 1 - windowSize);
        r.width = windowSize;
        r.height = windowSize;

        mapImage.uvRect = r;

        //The reason that setting Rect r and then assign it to mapImage.uvRect is necessary is : C# doesn't allow this:
        //mapIamge.uvRect.x = blahblahblah; mapIamge.uvRect.height = blahblahblah
        //Its property's setter blocks it ==> only a Rect value type can be assigned to something.uvRect, but something.uvRect.parameters cannot be set

        float cameraYaw = cameraTransform.eulerAngles.y;
        mapImage.rectTransform.localRotation = Quaternion.Euler(0, 0, cameraYaw);

        // playerYaw 之所以是顛倒的 (負值)，是因為：角色 y 軸朝上，而 canvas 的 z 軸朝內 (下)
        // 所以當角色轉對 y 軸轉 X 度 = 角色 icon 對 z 軸轉 -X 度
        float playerYaw = player.eulerAngles.y;
        avatarIcon.rectTransform.localRotation = Quaternion.Euler(0, 0, -playerYaw);


    }

    void OnDrawGizmos()
    {
        if (!worldBoundsCollider) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(
            worldBoundsCollider.bounds.center,
            worldBoundsCollider.bounds.size
        );
    }
}
