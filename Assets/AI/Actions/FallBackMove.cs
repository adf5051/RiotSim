using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class FallBackMove : RAINAction
{

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        NavMeshAgent agent = ai.Body.GetComponent<NavMeshAgent>();
        FallbackPoint fb = ai.WorkingMemory.GetItem<FallbackPoint>("SeekFB");

        Vector3 seek = fb.GetComponent<MeshFilter>().mesh.bounds.ClosestPoint(ai.Body.transform.position);
        seek = fb.transform.TransformPoint(seek);

        if(agent.destination != seek)
        {
            agent.areaMask = (1 << 5) | (1 << 4) | (1 << 3);
            agent.speed = ai.Body.GetComponent<Police>().Speed;
            agent.SetDestination(seek);
            agent.autoBraking = true;
            agent.Resume();
        }

        if(agent.remainingDistance < 0.5 || fb == ai.WorkingMemory.GetItem<FallbackPoint>("CurrentFB"))
        {
            ai.WorkingMemory.SetItem<FallbackPoint>("CurrentFB", fb);
            agent.Stop();
            return ActionResult.SUCCESS;
        }

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}