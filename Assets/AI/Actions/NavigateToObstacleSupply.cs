using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class NavigateToObstacleSupply : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        NavMeshAgent agent = ai.Body.GetComponent<NavMeshAgent>();
        Police p = ai.Body.GetComponent<Police>();

        if(ai.WorkingMemory.GetItem<Vector3>("Target") == ai.Body.transform.position || p.Barrier != null)
        {
            return ActionResult.SUCCESS;
        }

        if(agent.destination != ai.WorkingMemory.GetItem<Vector3>("Target"))
        {
            agent.SetDestination(ai.WorkingMemory.GetItem<Vector3>("Target"));
            agent.autoBraking = true;
            agent.speed = p.Speed;
            agent.Resume();
        }

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}