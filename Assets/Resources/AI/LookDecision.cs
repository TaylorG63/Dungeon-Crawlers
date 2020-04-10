using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit Hit;
        if (Physics.SphereCast(controller.look.position, controller.enemyStats.lookSphereCastRadius, controller.look.forward, out Hit, (int)controller.enemyStats.lookRange)
            && Hit.collider.CompareTag("Player"))
        {
            controller.chaseTarget = Hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}
