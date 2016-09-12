using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINDecision("Invert Result")]
public class InverseActionDecorator : RAINDecision {

    public override void Start(AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        ActionResult result = _children[_children.Count - 1].Run(ai);
        if (result == ActionResult.SUCCESS)
        {
            result = ActionResult.FAILURE;
        }
        else if(result == ActionResult.FAILURE)
        {
            result = ActionResult.SUCCESS;
        }

        return result;
    }
}
