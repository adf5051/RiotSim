using RAIN.Core;
using RAIN.Action;
using UnityEngine;
using System.Collections;

[RAINAction]
public class CustomPolicePatrol : RAINAction {
    NavMeshAgent agent;

    public override void Start(AI ai)
    {
        agent = ai.Body.GetComponent<NavMeshAgent>();
        agent.SetDestination(ai.WorkingMemory.GetItem<Vector3>("moveTarget"));
        agent.areaMask = (1 << 5) | (1 << 4) | (1<<3);
        agent.autoBraking = false;
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }

}
