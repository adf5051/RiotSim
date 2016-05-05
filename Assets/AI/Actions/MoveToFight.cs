using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class MoveToFight : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        NavMeshAgent agent = ai.Body.GetComponent<NavMeshAgent>();
        IAIGuy otherGuy = ai.WorkingMemory.GetItem<IAIGuy>("Enemy");
        //agent.ResetPath();
        
        if (otherGuy == null)
        {
            return ActionResult.SUCCESS;
        }else if(otherGuy.Health <= 0 || otherGuy.Dead)
        {
            ai.WorkingMemory.SetItem<IAIGuy>("Enemy", null);
            ai.Body.GetComponent<IAIGuy>().RemoveDeadEnemy(otherGuy);
            return ActionResult.SUCCESS;
        }

        Vector3 mag = ai.Body.transform.position - otherGuy.gameObject.transform.position;

        agent.SetDestination(otherGuy.gameObject.transform.position);

        if(mag.magnitude < 2)
        {
            agent.Stop();
        }
        else
        {
            agent.Resume();
            return ActionResult.RUNNING;
        }

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}