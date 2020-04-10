using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StateController : MonoBehaviour
{
    public State currentState;
    public EnemyStats enemyStats;
    public Transform look;
    public State remainState;

    [HideInInspector]public NavMeshAgent _navMeshAgent;
    [HideInInspector]public int nextWayPoint;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    private bool aiActive;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetupAi(bool aiActiveFromManager)
    {
        aiActive = aiActiveFromManager;
        if (aiActive)
        {
            _navMeshAgent.enabled = true;
        }
        else
        {
            _navMeshAgent.enabled = false;
        }
    }
    private void Update()
    {
        if (!aiActive)
        {
            return;
        }
        else
        {
            currentState.UpdateState(this);
        }
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}
