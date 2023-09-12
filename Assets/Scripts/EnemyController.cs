using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private int currentPatrolPoint;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyHealthManager enemyHealthManager;
    [SerializeField] private eState currentState;
    [SerializeField] private float waitAtPoint = 2f;
    [SerializeField] private float waitCounter;
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float timeBetweenAttack = 2f;
    [SerializeField] private float attackCounter = 1f;
    
    [SerializeField] private const string IsMoving = "IsMoving"; 
    [SerializeField] private const string Attack = "Attack"; 
    void Start()
    {
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetIsGameOver())
        {
            agent.Stop();
            enemyHealthManager.TakeDamage();
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.PlayerCurrentTranform.position);
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
                    Debug.Log("move to patrolling");
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = eState.isChasing;
                    anim.SetBool(IsMoving, true);
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
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = eState.isChasing;
                }
                
                anim.SetBool(IsMoving, true);
                break;
            case eState.isChasing:
                agent.SetDestination(GameManager.Instance.PlayerCurrentTranform.position);
                if (distanceToPlayer <= attackRange)
                {
                    currentState = eState.isAttacking;
                    anim.SetTrigger(Attack);
                    Debug.Log("Attack");
                    anim.SetBool(IsMoving, false);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    attackCounter = timeBetweenAttack;
                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = eState.isIdle;
                    waitCounter = waitAtPoint;
                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }
                break;
            case eState.isAttacking:
                transform.LookAt(GameManager.Instance.PlayerCurrentTranform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                
                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    if (distanceToPlayer <= attackRange)
                    {
                        anim.SetTrigger(Attack);
                        attackCounter = timeBetweenAttack;
                    }
                    else
                    {
                        currentState = eState.isIdle;
                        waitCounter = waitAtPoint;
                        agent.isStopped = false;
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
      
    }
}

