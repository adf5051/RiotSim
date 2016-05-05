using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINDecision]
public class FindEmptyBarrierSpot : RAINDecision
{

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ActionResult tResult = ActionResult.SUCCESS;

        FallbackPoint fb = ai.WorkingMemory.GetItem<FallbackPoint>("CurrentFB");
        Vector3 target = ai.WorkingMemory.GetItem<Vector3>("Target");
        //Vector3 target = fb.RandomUnclaimedSpot().transform.position;
        
        if (fb != null && !fb.FormationPoint && fb.BarrierSpots.Count >= 0)
        {
            Vector3 closest = ai.Body.transform.position;
            BarrierPlacementSpot closestSpot = null;
            float dist;
            float mindist = float.MaxValue;

            foreach (BarrierPlacementSpot bps in fb.BarrierSpots)
            {
                if (bps.Barrier)
                {
                    continue;
                }

                if (bps.Claimed && bps.transform.position == target)
                {
                    closest = target;
                    break;
                }
                

                dist = (bps.transform.position - ai.Body.transform.position).magnitude;

                if (dist < mindist)
                {
                    mindist = dist;
                    closest = bps.transform.position;
                    closestSpot = bps;
                }
            }

            ai.WorkingMemory.SetItem<Vector3>("Target", closest);

            if (closestSpot)
                closestSpot.Claimed = true;

            tResult = _children[0].Run(ai);
            return tResult;
        }
        else
        {
            return ActionResult.FAILURE;
        }


    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}