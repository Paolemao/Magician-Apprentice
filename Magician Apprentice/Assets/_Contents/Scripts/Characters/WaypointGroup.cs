using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointGroup : MonoBehaviour {

    public List<Transform> waypoints = new List<Transform>();

    private void OnEnable()
    {
        waypoints.Clear();
        foreach (Transform tr in transform)
        {
            if (!waypoints.Contains(tr))
            {
                waypoints.Add(tr);
            }
        }
        
    }

}
