using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ActiveState : StateMachineBehaviour
{
    [Tooltip("开始产生伤害的时间（normalizedTime）")]
    public float startDamageTime = 0.05f;
    [Tooltip("结束产生伤害的时间（normalizedTime）")]
    public float endDamageTime = 0.9f;

    [Tooltip("退出状态时是否清空输入池")]
    public bool resetTrigger;

    private bool isActive;

 

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (stateInfo.normalizedTime % 1 >= startDamageTime && stateInfo.normalizedTime % 1 <= endDamageTime)
        {
            isActive = true;
            ActiveDamage(animator, true);
        }
        else if (stateInfo.normalizedTime % 1 > endDamageTime && isActive)
        {
            isActive = false;
            ActiveDamage(animator, false);
        }


    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (animator.gameObject.GetComponent<Ai_SpearEnemy>())
        {
            var weapon = (AxeWeapon)animator.gameObject.GetComponent<Ai_SpearEnemy>().equipedAssistWeapon;
            Debug.Log(animator.gameObject.GetComponent<Ai_SpearEnemy>().equipedAssistWeapon);
            weapon.ShootSpear();
        }
 
        if (isActive)
        {
            isActive = false;
            ActiveDamage(animator,false);
        }
        if (resetTrigger)
        {
            animator.ResetTrigger("Shoot");
        }


    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    void ActiveDamage(Animator animator ,bool value)
    {
        var melee = animator.GetComponent<Character>();
        if (melee)
        {
            melee.SetActiveMelee(value);
        }
    }

}
