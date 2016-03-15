using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Navigation.Waypoints;
using RAIN.Serialization;

[RAINDecision]
public class PoliceFindPatrolPoint : RAINDecision
{

    public List<PatrolPoint> patrolPoints;
    public PatrolPoint currentPoint = null;
    private bool pointReady;

    public override void Start(AI ai)
    {
        base.Start(ai);
        Transform temp = ai.WorkingMemory.GetItem<Transform>("patrolRoute");

        if (temp != null) {
            patrolPoints = new List<PatrolPoint>();
            foreach(Transform t in temp)
            {
                if(t.tag == "PatrolPoint")
                {
                    patrolPoints.Add(t.GetComponent<PatrolPoint>());
                }
            }
            //patrolPoints = ai.WorkingMemory.GetItem<GameObject>("patrolRoute").GetComponent<WaypointRig>().WaypointSet.Waypoints;
        }
        else
        {
            patrolPoints = null;
            Debug.Log(ai.Body, ai.Body);
        }

    }

    public override ActionResult Execute(AI ai)
    {
        int q = 0;
        if (patrolPoints == null)
        {
            return ActionResult.FAILURE;
        }

        if (currentPoint == null)
        {
            currentPoint = FindNearestPatrolPoint(ai);
        }

        if(currentPoint == null)
        {
            return ActionResult.FAILURE;
        }
        else
        {
            //ai.WorkingMemory.SetItem<Vector3>("moveTarget", patrolPoints[(int)pointIndex].Position);
            ai.WorkingMemory.SetItem<Vector3>("moveTarget", currentPoint.transform.position);
        }

        ActionResult tResult = _children[0].Run(ai);
        if(_children[0].Run(ai) == ActionResult.SUCCESS && pointReady)
        {
            currentPoint = currentPoint.next.GetComponent<PatrolPoint>();
            pointReady = false;
        }
        else
        {
            pointReady = true;
        }
   
        return tResult;
    }

    PatrolPoint FindNearestPatrolPoint(AI ai)
    {
        float currentDistance;
        float shortestDistance = float.MaxValue;
        PatrolPoint closest = null;

        for(int i = 0; i < patrolPoints.Count; i++)
        {
            currentDistance = (ai.Body.transform.position - patrolPoints[i].transform.position).magnitude;
            if(currentDistance < shortestDistance)
            {
                shortestDistance = currentDistance;
                closest = patrolPoints[i];
            }
        }

        return closest;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}