using Game.Character;
using Game.Combat;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    public RawImage crosshair;
    public Texture texTargetHit;
    public Texture texTargetMissed;
    public Texture texTargetInRanged;
    public Texture texIdle;

    private Vector3 screenCenter;
    private Ray ray;
    public float maxDistance;
    public LayerMask CrosshairMask;

    public MyTPController controller;

    bool isShooting;
    CrosshairState state;

    public WeaponController wpController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        isShooting = false;
        state = CrosshairState.Hidden;
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        SetState();
    }

    public void CheckState()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        bool targetInAim = Physics.Raycast(ray, out hit, maxDistance, CrosshairMask);

        if(controller.CurrentMode == ControlMode.AimMove)
        {
            crosshair.enabled = true;
            if (isShooting)
            {
                if(targetInAim)
                {
                    state = CrosshairState.TargetHit;
                }
                else
                {
                    state = CrosshairState.TargetMissed;
                }
            }
            else
            {
                if (targetInAim)
                {
                    state = CrosshairState.TargetInRanged;
                }
                else
                {
                    state = CrosshairState.Idle;
                }
            }
        }
        else
        {
            state = CrosshairState.Hidden;
        }
    }

    public void SetState()
    {
        switch (state)
        {
            case CrosshairState.Hidden:
                crosshair.enabled = false;
                break;
            case CrosshairState.TargetHit:
                crosshair.enabled = true;
                crosshair.texture = texTargetHit;
                crosshair.color = Color.red;
                break;
            case CrosshairState.TargetMissed:
                crosshair.enabled = true;
                crosshair.texture = texTargetMissed;
                crosshair.color = Color.white;
                break;
            case CrosshairState.TargetInRanged:
                crosshair.enabled = true;
                crosshair.texture = texTargetInRanged;
                crosshair.color = Color.yellow;
                break;
            case CrosshairState.Idle:
                crosshair.enabled = true;
                crosshair.texture = texIdle;
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
    Hidden,
    TargetHit,
    TargetMissed,
    TargetInRanged,
    Idle,    
}

