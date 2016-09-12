using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CivFlee : RAINAction
{
    bool init = false;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (!init)
        {
            init = true;
            NavMeshAgent agent = ai.Body.GetComponent<NavMeshAgent>();
            Vector3 closest = FleePointManager.Instance.NearestFleePoint(ai.Body.transform.position);
            agent.areaMask = (1 << 4) | (1 << 3) | (1 << 5);
            agent.SetDestination(closest);
            
        }

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}