using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamEffect : EffectsScrip
{

    [Header("Prefabs")]
    public GameObject beamStart;
    public GameObject beamEnd;
    LineRenderer line;

    [Header("可调变量")]
    public float beamEndOffset = 1f;
    public float textureScrollSpeed = 8f;//纹理刷新速度
    public float textureLengthScale = 3;//纹理长度

    protected override void Start()
    {
        line = transform.GetComponent<LineRenderer>();
    }
    protected override void Update()
    {

        base.Update();
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("++++++++++++++++++++++++");
            Destroy(beamStart);
            Destroy(beamEnd);
            Destroy(gameObject);
        }
    }
    public void StartInst()
    {
        beamStart = Instantiate(beamStart, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        beamEnd = Instantiate(beamEnd, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }

    public void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        line.positionCount=2;
        line.SetPosition(0, start);

        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit))
        {
            end = hit.point - (dir.normalized * beamEndOffset);
        }
        else
        {
            end = transform.position + (dir * 100);
        }

        beamEnd.transform.position = end;
        line.SetPosition(1,end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector3.Distance(start,end);

        line.sharedMaterial.mainTextureScale = new Vector2(distance/textureLengthScale,1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime*textureScrollSpeed,0);

    }
    

}
