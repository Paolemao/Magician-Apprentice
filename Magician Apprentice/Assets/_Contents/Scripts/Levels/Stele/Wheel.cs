using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    public bool levelStart;
    public bool level_2_Start;

    Brazier[] braziers;


    private void Start()
    {
        levelStart = false;
        level_2_Start = false;

        braziers = transform.GetComponentsInChildren<Brazier>();
        foreach (var bra in braziers)//激活火柱脚本
        {
            bra.enabled = false;
        }

        //控制旋转的时间
        StartCoroutine(StopRota());
        

    }
    private void Update()
    {
        if (levelStart)//控制本机关的旋转
        {
            Ro();
            foreach (var bra in braziers)//激活火柱脚本
            {
                bra.enabled = true;
            }
        }
        else
        {
            transform.Rotate(0, 0, 0, Space.Self);
            foreach (var bra in braziers)
            {
                bra.enabled = false;
            }
        }

        foreach (var bra in braziers)//控制下一个机关的访问
        {
            if (!bra.isFire)//所有子对象的isFire
            {
                
                return;
            }
        }

        Debug.Log("_____________________");
        level_2_Start = true;

    }

    //先实现功能，再考虑用线性插值来控制旋转的速度 
    void Ro()//旋转
    {
        //float scale = Mathf.Lerp(0,-25,-1);
        transform.Rotate(-25 * Time.deltaTime,0,0,Space.Self);
    }
    IEnumerator StopRota()
    {
        while (levelStart)
        {
            yield return new WaitForSeconds(10f);
            levelStart = false;
        }
    }

}
