using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] public List<Transform> patrolPath;
    public Transform target;
    public NavMeshAgent guardAgent;
    public int currentPatrol;
    public bool idle;

    // Start is called before the first frame update
    void Start()
    {
        idle = false;
        guardAgent = this.gameObject.GetComponent<NavMeshAgent>();
        if (patrolPath[0] == null && idle == false)
        {
            patrolPath[0] = this.gameObject.transform;
            idle = true;
        }
        guardAgent.SetDestination(patrolPath[0].position);
    }

    // Update is called once per frame
    void Update()
    {

        if (guardAgent.remainingDistance < 1f && idle == false)
        {
            currentPatrol++;

            if (currentPatrol < patrolPath.Count)
            {
                guardAgent.SetDestination(patrolPath[currentPatrol].position);
            }
            else
            {
                currentPatrol = 0;
                guardAgent.SetDestination(patrolPath[currentPatrol].position);
            }
        }
    }
}
