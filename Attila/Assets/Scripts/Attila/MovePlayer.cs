using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    public Text objectivesText;
    public Text finalText;

    private NavMeshAgent agent;
    private string destination;

    public void Start()
    {
        destination = "";
        GlobalInfo.isOldCellDestroyed = true;
    }

    public void Move(string mCell)
    {
        GameObject dest = GameObject.Find(mCell);
        if (dest != null)
        {
            //Don't move at the same position
            if ("Cell" + GlobalInfo.playerPos.ToString() != mCell)
            {
                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                agent.destination = dest.transform.position;
                GlobalInfo.isPlayerMoving = true;
                GlobalInfo.isOldCellDestroyed = false;
                destination = mCell;
                dest.GetComponent<GameCell>().SetMoveables();
                GameObject origin = GameObject.Find("Cell" + GlobalInfo.playerPos.ToString());
                origin.GetComponent<GameCell>().HideMoveables();
                StartCoroutine(DestroyCell(origin));
            }            
        }        
    }

    IEnumerator DestroyCell(GameObject cellToDestroy)
    {
        yield return new WaitForSeconds(1f);        
        Destroy(cellToDestroy);
        yield return new WaitForSeconds(0.5f);
        CalculateNewMovements();
        GlobalInfo.isOldCellDestroyed = true;
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

    public void ProcessEvents()
    {
        //Update values
        if (GameObject.Find(destination).GetComponent<GameCell>().objective == true)
        {
            GlobalInfo.objectivesNum--;
        }
        if (GameObject.Find(destination).GetComponent<GameCell>().final == true)
        {
            GlobalInfo.finalNum--;
        }

        if (GlobalInfo.objectivesNum == 0)
        {
            if (GlobalInfo.finalNum == 0)
            {
                //Good job!
            }
        }

        if (GlobalInfo.movementsNum == 0)
        {
            //Game over;
        }

        ShowInfo();
        GlobalInfo.isPlayerMoving = false;
    }

    public void ShowInfo()
    {
        objectivesText.text = GlobalInfo.objectivesNum.ToString();        
    }

    private void Update()
    {
        if (GlobalInfo.isPlayerMoving == true)
        {
            if (PathComplete() && GlobalInfo.isOldCellDestroyed)
            {                
                GameObject.Find(destination).GetComponent<GameCell>().ShowMoveables();
                GlobalInfo.playerPos = GameObject.Find(destination).GetComponent<GameCell>().num;

                //Set destionation cell
                GameObject cellStart = GameObject.Find("Cell" + GlobalInfo.playerPos.ToString());
                GameObject regularStart = GetChildWithName(cellStart, "Regualr_Collider_Union");
                GameObject spriteCellStart = GetChildWithName(regularStart, "Iso2DObject_Union");
                spriteCellStart.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<GameManager>().horse;
                ProcessEvents();                
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
