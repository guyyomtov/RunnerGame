using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private int currentPatrolPoint;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private eState currentState;
    [SerializeField] private float waitAtPoint = 2f;
    [SerializeField] private float waitCounter;
    [SerializeField] private const string IsMoving = "IsMoving"; 
    void Start()
    {
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case eState.isIdle:
                anim.SetBool(IsMoving, false);
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = eState.isPatroling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }
                
                break;
            case eState.isPatroling:
               
                if (agent.remainingDistance <= 0.2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }
                    //agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    currentState = eState.isIdle;
                    waitCounter = waitAtPoint;
                }
                anim.SetBool(IsMoving, true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
      
    }
}

public enum eState
{
    isIdle, isPatroling
}
