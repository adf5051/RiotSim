using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class MoveToFormationPoint : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        NavMeshAgent agent = ai.Body.GetComponent<NavMeshAgent>();
        Police p = ai.Body.GetComponent<Police>();

        if (agent.destination != ai.WorkingMemory.GetItem<Vector3>("Target"))
        {
            agent.areaMask = (1 << 5) | (1 << 4) | (1<<3);
            agent.SetDestination(ai.WorkingMemory.GetItem<Vector3>("Target"));
            agent.autoBraking = true;
            agent.speed = p.Speed;
            agent.Resume();
        }

        if(agent.remainingDistance < 0.1f)
        {
            return ActionResult.SUCCESS;
        }

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}