using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{    
    private NavMeshAgent agent;

    public void Move(string mCell)
    {
        GameObject dest = GameObject.Find(mCell);
        if (dest != null)
        {            
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = dest.transform.position;
        }        
    }

}
