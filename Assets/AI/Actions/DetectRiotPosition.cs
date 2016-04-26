using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINDecision]
public class DetectRiotPosition : RAINDecision
{

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ActionResult tResult = ActionResult.SUCCESS;

        FallbackPoint fp = FallbackPointManager.Instance.FindNextUncompromised();
        ai.WorkingMemory.SetItem<FallbackPoint>("SeekFB", fp);
        //FallbackPoint current = ai.WorkingMemory.GetItem<FallbackPoint>("CurrentFB");

        if (fp == null || fp.FormationPoint)
        {
            return ActionResult.FAILURE;
        }
            
        tResult = _children[_children.Count - 1].Run(ai);    

        return tResult;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}