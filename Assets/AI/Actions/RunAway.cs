using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RunAway : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        NavMeshAgent agent = ai.Body.GetComponent<NavMeshAgent>();
        IAIGuy otherGuy = ai.WorkingMemory.GetItem<IAIGuy>("Enemy");

        if (otherGuy == null)
        {
            return ActionResult.SUCCESS;
        }
        else if (otherGuy.Health <= 0 || otherGuy.Dead)
        {
            ai.WorkingMemory.SetItem<IAIGuy>("Enemy", null);
            ai.Body.GetComponent<IAIGuy>().RemoveDeadEnemy(otherGuy);
            return ActionResult.SUCCESS;
        }

        agent.SetDestination(otherGuy.gameObject.transform.position + (5 * otherGuy.gameObject.transform.forward));

        if(agent.remainingDistance < 0.5)
        {
            return ActionResult.SUCCESS;
        }
        else
        {
            ai.WorkingMemory.SetItem<IAIGuy>("Enemy", null);
            agent.Resume();
            return ActionResult.RUNNING;
        }
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}