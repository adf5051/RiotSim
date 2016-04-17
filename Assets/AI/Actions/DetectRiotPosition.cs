using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINDecision]
public class DetectRiotPosition : RAINDecision
{
    private int _lastRunning = 0;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

        _lastRunning = 0;
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ActionResult tResult = ActionResult.SUCCESS;

        FallbackPoint fp = FallbackPointManager.Instance.FindNextUncompromised();
        FallbackPoint current = ai.WorkingMemory.GetItem<FallbackPoint>("CurrentFB");

        if (fp.FormationPoint || fp == null)
        {
            return ActionResult.FAILURE;
        }

        if (current == null || current != fp)
        {
            ai.WorkingMemory.SetItem<FallbackPoint>("SeekFB", fp);
            tResult = _children[_children.Count - 1].Run(ai);
        }

        return tResult;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}