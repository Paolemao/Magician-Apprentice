using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLaser : SkilData {

    [Header("Prefabs")]
    public GameObject beamLineRendererPrefab;
    public GameObject beamStartPrefab;
    public GameObject beamEndPrefab;


    private GameObject beamStart;
    private GameObject beamEnd;
    private GameObject beam;
    private LineRenderer line;

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
    public float textureLengthScale = 3; //Length of the beam texture

    private void OnEnable()
    {
        beamStart = Instantiate(beamStartPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        beamEnd = Instantiate(beamEndPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        beam = Instantiate(beamLineRendererPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        line = beam.GetComponent<LineRenderer>();
    }

    protected override void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(beamStart);
            Destroy(beamEnd);
            Destroy(beam);
            Destroy(gameObject);
        }

    }
    public void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        //Debug.Log(line.gameObject == null);
        line.positionCount = 2;
        line.SetPosition(0, start);

        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;

        int lay = LayerMask.GetMask("Environment");

        if (Physics.Raycast(start, dir, out hit,100f,lay))
        {
            Debug.Log(hit.point);
            end = hit.point;
            Debug.Log(hit.collider.name);
        }
        else
        {
            end = transform.position + (dir * 50);
        }

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector3.Distance(start, end);

        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);

    }
    public void Shoot(Vector3 start)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {

            Vector3 tdir = hit.point - transform.position;
            //Debug.Log(tdir);
            ShootBeamInDir(start, tdir);
        }
    }

}
