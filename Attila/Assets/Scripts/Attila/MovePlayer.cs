using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        // Is mountain.. dont destroy
        if (GlobalInfo.gridStage[cellToDestroy.GetComponent<GameCell>().num - 1].type == 12)
        {
        } else
        {
            //Dont destroy final if ALL objectives are NOT destroyed first
            if (GlobalInfo.gridStage[cellToDestroy.GetComponent<GameCell>().num - 1].isFinal && GlobalInfo.objectivesNum > 0)
            {
            } else
            {
                Destroy(cellToDestroy);
            }            
        }        
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
            Objective();            
        }
        if (GameObject.Find(destination).GetComponent<GameCell>().final == true)
        {
            Final();
        }

        if ((GameObject.Find(destination).GetComponent<GameCell>().final == false) && (GameObject.Find(destination).GetComponent<GameCell>().objective == false))
        {
            int nType = GlobalInfo.gridStage[GameObject.Find(destination).GetComponent<GameCell>().num - 1].type;
            // Is Town OR City OR Army
            if (nType == 4 || nType == 5 || nType == 6 )
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().ShowBattleResult();
            }
            Normal();
        }

        //Events
        if (GlobalInfo.objectivesNum == 0)
        {
            if (GlobalInfo.finalNum == 0)
            {
                //Good job!
                GlobalInfo.maxStageCompleted = GlobalInfo.actualStage;
                PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
                loadedData.actualStage = GlobalInfo.actualStage;
                loadedData.maxStageCompleted = GlobalInfo.maxStageCompleted;
                loadedData.troops = GlobalInfo.troops;
                loadedData.weapons = GlobalInfo.weapons;
                loadedData.water = GlobalInfo.water;
                loadedData.food = GlobalInfo.food;
                loadedData.gold = GlobalInfo.gold;
                loadedData.score = GlobalInfo.gold;
                DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");

                if (GlobalInfo.maxStageCompleted == GlobalInfo.maxStagesGame)
                {
                    //Game Finish
                    SceneManager.LoadScene("Winner");
                }
                else
                {
                    //Next stage
                    GlobalInfo.levelCompleted = true;                    
                }                
            }
        }

        if (GlobalInfo.movementsNum == 0)
        {
            //Game over NO MOVES;
            GameObject.Find("GameManager").GetComponent<GameManager>().ShowBlockedBox();
        }

        ShowInfo();
        GlobalInfo.isPlayerMoving = false;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(NextLevel());
    }

    public void Objective()
    {
        GlobalInfo.score = GlobalInfo.score + 100;
        GameObject.Find(destination).GetComponent<GameCell>().UpdateValues();
        GlobalInfo.objectivesNum--;
        GameObject.Find("GameManager").GetComponent<GameManager>().ShowBattleResult();
    }

    public void Final()
    {
        GlobalInfo.score = GlobalInfo.score + 1000;
        if (GlobalInfo.objectivesNum == 0)
        {
            GameObject.Find(destination).GetComponent<GameCell>().UpdateValues();
            GlobalInfo.finalNum--;
            GameObject.Find("GameManager").GetComponent<GameManager>().ShowBattleResult();
        }        
    }

    public void Normal()
    {
        GameObject.Find(destination).GetComponent<GameCell>().UpdateValues();
        GlobalInfo.score = GlobalInfo.score + 10;
    }

    IEnumerator NextLevel()
    {
        yield return new WaitUntil(() => GlobalInfo.isShowingInfo == false);
        yield return new WaitForSeconds(1.0f);
        GlobalInfo.actualStage++;
        SceneManager.LoadScene("Attila");
    }

    public void ShowInfo()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().ShowInfo();
        finalText.text = GlobalInfo.finalNum.ToString();
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
                if (GlobalInfo.gridStage[GameObject.Find(destination).GetComponent<GameCell>().num - 1].type == 12)
                {
                    //Is mountain
                    GlobalInfo.isEventAvaliable = true;
                } else
                {
                    if (GlobalInfo.gridStage[GameObject.Find(destination).GetComponent<GameCell>().num - 1].isFinal && GlobalInfo.objectivesNum > 0)
                    {
                        //Is final but objectives are not completed
                        GlobalInfo.isEventAvaliable = false;
                    }
                    else
                    {
                        GlobalInfo.isEventAvaliable = true;
                        GameObject cellStart = GameObject.Find("Cell" + GlobalInfo.playerPos.ToString());
                        GameObject regularStart = GetChildWithName(cellStart, "Regualr_Collider_Union");
                        GameObject spriteCellStart = GetChildWithName(regularStart, "Iso2DObject_Union");
                        spriteCellStart.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<GameManager>().horse;
                    }                    
                }                
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
