using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    void Start()
    {
        agent.SetDestination(target.position); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
