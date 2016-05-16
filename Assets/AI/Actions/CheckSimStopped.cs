using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CheckSimStopped : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if(SimManager.Instance.State == State.running)
        {
            return ActionResult.SUCCESS;
        }

        NavMeshAgent agent = ai.Body.GetComponent<NavMeshAgent>();
        agent.Stop();

        return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}