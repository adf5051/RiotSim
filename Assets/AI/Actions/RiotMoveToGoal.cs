using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RiotMoveToGoal : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        NavMeshAgent agent = ai.Body.GetComponent<NavMeshAgent>();

        Vector3 goalPos = ai.WorkingMemory.GetItem<GameObject>("RiotGoal").transform.position;
        Vector3 offset = Vector3.zero;
        offset.x = ai.Body.transform.position.x;

        Vector3 destination = goalPos + offset;
        //Vector3 f = ai.Body.transform.forward;
        //Vector3 vtog = goalPos - ai.Body.transform.position;
        //float dot = Vector3.Dot(vtog, f);
        //Vector3 destination = goalPos;
        //float angle = Vector3.Angle(f, vtog);

        //if (dot > 0 && angle < 22.5f) 
        //{
        //    destination = ai.Body.transform.position + (ai.Body.transform.forward * dot);
        //} 

        if(agent.destination != destination)
        {
            agent.destination = destination;
            agent.areaMask = (1 << 3) | (1 << 4);
            agent.Resume();
        }

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}