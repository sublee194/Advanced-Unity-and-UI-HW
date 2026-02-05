using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    public RawImage crosshair;
    public Texture idle;
    public Texture target;
    public Texture shooting;

    private Vector3 screenCenter;
    private Ray ray;
    public float maxDistance;
    public LayerMask CrosshairMask;

    bool isShooting;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        bool targetInAim = Physics.Raycast(ray, out hit, maxDistance, CrosshairMask);

        if (isShooting)
        {
            SetState(CrosshairState.Shooting);
            //crosshair.color = Color.white;
        }
        else if (targetInAim)
        {
            SetState(CrosshairState.Target);
            //crosshair.color = Color.red;
        }
        else
        {
            SetState(CrosshairState.Idle);
            //crosshair.color = Color.white;
        }
    }

    public void SetState(CrosshairState state)
    {
        switch (state)
        {
            case CrosshairState.Shooting:
                crosshair.texture = shooting;
                crosshair.color = Color.white;
                break;
            case CrosshairState.Target:
                crosshair.texture = target;
                crosshair.color = Color.red;
                break;
            case CrosshairState.Idle:
                crosshair.texture = idle;
                crosshair.color = Color.white;
                break;
        }
    }

    public void ChangeIsShooting()
    {
        if (isShooting)
            isShooting = false;
        else
            isShooting = true;
    }
}

public enum CrosshairState
{
    Idle,
    Target,
    Shooting
}
