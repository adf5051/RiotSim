using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RiotFindGoal : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        GameObject goal = ai.WorkingMemory.GetItem<GameObject>("RiotGoal");
        if (goal)
        {
            return ActionResult.SUCCESS;
        }
        else
        {
            GameObject go = GameObject.Find("RiotGoal");
            if (go)
            {
                ai.WorkingMemory.SetItem<GameObject>("RiotGoal", go);
                return ActionResult.SUCCESS;
            }

            return ActionResult.FAILURE;
        }
        
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}