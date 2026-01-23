using UnityEngine;

public class ReloadStateBlocker : StateMachineBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Start Shoot");
        animator.GetComponent<WeaponController>().cannotReload = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("End Shoot");
        animator.GetComponent<WeaponController>().cannotReload = false;
    }
}
