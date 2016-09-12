using UnityEngine;
using System.Collections.Generic;

public class FleePoints : MonoBehaviour {

    private List<Transform> fleePoints = new List<Transform>();

	// Use this for initialization
	void Start () {
	    foreach(Transform t in transform)
        {
            if(t.name.Contains("Flee Point"))
            {
                fleePoints.Add(t);
            }
        }
	}
	
    public Vector3 FindClosest(Vector3 pos)
    {
        Vector3 closest = Vector3.zero;
        float dist = float.MaxValue;

        foreach(Transform t in fleePoints)
        {
            float temp = (pos - t.position).magnitude;
            if(temp < dist)
            {
                dist = temp;
                closest = t.position;
            }
        }

        return closest;
    }
}
