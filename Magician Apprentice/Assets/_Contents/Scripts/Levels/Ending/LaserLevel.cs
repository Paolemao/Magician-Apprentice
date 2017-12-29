using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLevel : MonoBehaviour {


    public GameObject beam;
    LineRenderer line;

    public Transform EndPosition;


    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
    public float textureLengthScale = 3; //Length of the beam texture

    private void OnEnable()
    {
        beam = Instantiate(beam, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        line = beam.GetComponent<LineRenderer>();
        
    }
    private void Start()
    {
        Shoot(transform.position);
    }
    public void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        //Debug.Log(line.gameObject == null);
        line.positionCount = 2;
        line.SetPosition(0, start);

        Vector3 end = Vector3.zero;
        RaycastHit hit;

        int lay = LayerMask.GetMask("Environment");

        if (Physics.Raycast(start, dir, out hit, 100f, lay))
        {
            Debug.Log(hit.point);
            end = hit.point;
            Debug.Log(hit.collider.name);
        }
        else
        {
            end = transform.position + (dir * 50);
        }
        line.SetPosition(1, end);

        float distance = Vector3.Distance(start, end);

        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);

    }

    public void Shoot(Vector3 start)
    {
        Vector3 tdir = EndPosition.position - transform.position;
        //Debug.Log(tdir);
        ShootBeamInDir(start, tdir);
    }
}
