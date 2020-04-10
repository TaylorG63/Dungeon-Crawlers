using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        controller._navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        controller._navMeshAgent.isStopped = false;

        if (controller._navMeshAgent.remainingDistance <= controller._navMeshAgent.stoppingDistance && !controller._navMeshAgent.pathPending )
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }
}
