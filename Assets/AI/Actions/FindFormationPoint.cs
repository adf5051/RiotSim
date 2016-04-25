using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINDecision]
public class FindFormationPoint : RAINDecision
{
    private GameObject formationPoint;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ActionResult tResult = ActionResult.SUCCESS;
        FallbackPoint fb = ai.WorkingMemory.GetItem<FallbackPoint>("CurrentFB");
        if(fb == null ||  !fb.FormationPoint)
        {
            fb = FallbackPointManager.Instance.FindFormationPoint();
            ai.WorkingMemory.SetItem<FallbackPoint>("CurrentFB", fb);
        }
        Vector3 target = ai.WorkingMemory.GetItem<Vector3>("Target");

        if(formationPoint == null)
        {
            formationPoint = fb.FindClosestFormationPoint(ai.Body.transform.position);       
        }

        ai.WorkingMemory.SetItem<Vector3>("Target", formationPoint.transform.position);

        tResult = _children[0].Run(ai);
        return tResult;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}