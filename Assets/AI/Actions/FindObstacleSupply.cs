using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINDecision]
public class FindObstacleSupply : RAINDecision
{

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ActionResult tResult = ActionResult.SUCCESS;
        FallbackPoint fb = ai.WorkingMemory.GetItem<FallbackPoint>("CurrentFB");
        if(fb == null || fb.FormationPoint || fb.BarrierStorage.Length <= 0)
        {
            return ActionResult.FAILURE;
        }

        Police p = ai.Body.GetComponent<Police>();
        if(p.Barrier != null)
        {
            return ActionResult.SUCCESS;
        }

        Vector3 supplyPos = ai.Body.transform.position;
        float dist;
        float minDist = float.MaxValue;

        foreach(GameObject sup in fb.BarrierStorage)
        {
            dist = (sup.transform.position - ai.Body.transform.position).magnitude;
            if(dist < minDist)
            {
                supplyPos = sup.transform.position;
                minDist = dist;
            }
        }

        ai.WorkingMemory.SetItem<Vector3>("Target", supplyPos);

        tResult = _children[0].Run(ai);
        return tResult;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}