using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character {

    Vector3 aimTarget;

    protected override void Start()
    {
        base.Start();
        aimTarget = new Vector3();
    }

    protected override void UpdateControl()
    {
        base.UpdateControl();
        UpdateAimTarget();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (MagicPoint<=0)
        {
            MagicPoint = 0;
            return;
        }
        //if (healthPoint <= 0)
        //{
        //    Die();
        //    return;
        //}
        if (Input.GetMouseButtonDown(0)&& !Input.GetKey(KeyCode.LeftShift))
        {
           
            MagicPoint -=10;
            Debug.Log(MagicPoint);
            ThunderSkill();
            Attacking(true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            MagicPoint -= MagicPoint - 5;
            FireSkill();
            Attacking(true);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            MagicPoint -= MagicPoint - 5;
            wind = true;
            rigi.constraints = RigidbodyConstraints.FreezePosition;
            Attacking(true);
        }
        else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            MagicPoint -= MagicPoint - 5;
            IceSkill();
            Attacking(true);
        }
        else
        {
            Attacking(false);
            wind = false;
            rigi.constraints = RigidbodyConstraints.None |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
        }
        WindSkill();
        //移动向量
        var move = v * Vector3.forward + h * Vector3.right;
        Movement(move);


    }
    protected override void UpdateMovement()
    {
        base.UpdateMovement();
        if (isAttacking)
        {
            transform.LookAt(aimTarget,Vector3.up);
        }
    }
    private void UpdateAimTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,2000f))
        {
            Vector3 finalPos = new Vector3(hit.point.x,0,hit.point.z);

            aimTarget = finalPos;

        }
    }

}
