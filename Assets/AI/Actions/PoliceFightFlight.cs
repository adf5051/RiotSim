using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINDecision]
public class PoliceFightFlight : RAINDecision
{

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ActionResult tResult = ActionResult.SUCCESS;
        IAIGuy otherGuy = ai.WorkingMemory.GetItem<IAIGuy>("Enemy");
        Police p = ai.Body.GetComponent<Police>();

        if (p.Fighting && (otherGuy == null || otherGuy.gameObject == null || otherGuy.Dead))
        {
            p.RemoveDeadEnemy(otherGuy);
            p.Fighting = false;
            return ActionResult.SUCCESS;
        }

        if (p.SpottedRioters.Count > 0 && !p.Fighting)
        {
            float dist = float.MaxValue;
            IAIGuy closest = null;
            Vector3 mag;

            for (int i = 0; i < p.SpottedRioters.Count; i++)
            {
                IAIGuy guy = p.SpottedRioters[i].GetComponent<IAIGuy>();

                if (guy.Health <= 0 || guy.gameObject == null || guy.Dead)
                {
                    i--;
                    p.RemoveDeadEnemy(guy);
                    continue;
                }

                mag = guy.gameObject.transform.position - p.gameObject.transform.position;

                if (mag.magnitude < dist)
                {
                    dist = mag.magnitude;
                    closest = guy;
                }
            }

            ai.WorkingMemory.SetItem<IAIGuy>("Enemy", closest);

            //if (dist < 3)
            //{
            //    ai.WorkingMemory.SetItem<IAIGuy>("Enemy", closest);
            //    p.Fighting = true;
            //}
            //else
            //{
            //    p.Fighting = false;
            //    return ActionResult.SUCCESS;
            //}
        }
        else
        {
            p.Fighting = false;
            return ActionResult.SUCCESS;
        }

        bool fight = SimManager.Instance.policeBayes.Decide(p.Health, p.Strength, p.Barrier);

        // bayes will change as we are fighing. so ignore it once the fight starts. 
        if (fight || p.Fighting)
        {
            tResult = _children[0].Run(ai);
        }
        else
        {
            tResult = _children[1].Run(ai);
        }

        return tResult;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}