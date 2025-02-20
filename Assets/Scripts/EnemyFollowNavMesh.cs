using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowNavMesh : MonoBehaviour
{
    public Transform player; // Assign the player
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get NavMeshAgent
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // Enemy follows player
        }
    }
}
