using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{    
    private NavMeshAgent agent;
    private string destination;

    public void Start()
    {
        destination = "";
    }

    public void Move(string mCell)
    {
        GameObject dest = GameObject.Find(mCell);
        if (dest != null)
        {            
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = dest.transform.position;
            GlobalInfo.isPlayerMoving = true;
            destination = mCell;
            dest.GetComponent<GameCell>().SetMoveables();
            GameObject origin = GameObject.Find("Cell" + GlobalInfo.playerPos.ToString());
            origin.GetComponent<GameCell>().HideMoveables();
            StartCoroutine(DestroyCell(origin));            
        }        
    }

    IEnumerator DestroyCell(GameObject cellToDestroy)
    {
        yield return new WaitForSeconds(1f);
        Destroy(cellToDestroy);
        yield return new WaitForSeconds(0.5f);
        CalculateNewMovements();
    }

    private void CalculateNewMovements()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("GameCell");
        foreach (GameObject cell in cells)
        {
            cell.GetComponent<GameCell>().CalculateMovements();
        }
    }

    private bool PathComplete()
    {
        NavMeshAgent mNavMeshAgent = GetComponent<NavMeshAgent>();
        if (!mNavMeshAgent.pathPending)
        {
            if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
            {
                if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }

    private void Update()
    {
        if (GlobalInfo.isPlayerMoving == true)
        {
            if (PathComplete())
            {
                GlobalInfo.isPlayerMoving = false;
                GameObject.Find(destination).GetComponent<GameCell>().ShowMoveables();
                GlobalInfo.playerPos = GameObject.Find(destination).GetComponent<GameCell>().num;

                //Set destionation cell
                GameObject cellStart = GameObject.Find("Cell" + GlobalInfo.playerPos.ToString());
                GameObject regularStart = GetChildWithName(cellStart, "Regualr_Collider_Union");
                GameObject spriteCellStart = GetChildWithName(regularStart, "Iso2DObject_Union");
                spriteCellStart.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<GameManager>().horse;
            }            
        }
    }

    private GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
}
