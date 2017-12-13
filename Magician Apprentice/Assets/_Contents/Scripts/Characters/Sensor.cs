using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

    public bool debugVisual;
    //视野参数
    public float sightDistance = 20;
    public float sightLineNum = 30;
    public float sightHeight=1.5f;
    public float sightAngle = 90f;

    public LayerMask layerMask;
    public string targetTag = "Player";

    bool isFindEnemy = false;

    [HideInInspector]
    public Transform target;

    public bool IsFindEnemy
    {
        get
        {
            return isFindEnemy;
        }

        set
        {
            isFindEnemy = value;
        }
    }

    private void Update()
    {
        FieldOfView();
    }
    void FieldOfView()
    {
        target = null;
        var sightStart = new Vector3(transform.position.x,transform.position.y+sightHeight,transform.position.z);
        //从第二象限开始
        var forwardLeft = Quaternion.Euler(0,-(sightAngle/2),0)*transform.forward*sightDistance;

        for (int i=0;i<=sightLineNum;i++)
        {
            var step = sightAngle / sightLineNum;
            var v = Quaternion.Euler(0, step * i, 0) * forwardLeft;
            var sightEnd = sightStart + v;

            RaycastHit hit;

            if (Physics.Linecast(sightStart,sightEnd,out hit,layerMask))
            {
                sightEnd = hit.point;
                if (hit.transform.CompareTag(targetTag))
                {
                    target = hit.transform;
                    IsFindEnemy = true;
                }
            }


            if (debugVisual)
            {

                Debug.DrawLine(sightStart, sightEnd, Color.red);
            }


        }
        

    }
}
