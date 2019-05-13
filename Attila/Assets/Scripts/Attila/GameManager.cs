using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text troops;
    public Text weapons;
    public Text water;
    public Text food;
    public Text gold;
    public Text stageName;
    public Text score;

    public Sprite tundra;
    public Sprite desert;
    public Sprite woods;
    public Sprite town;
    public Sprite city;
    public Sprite army;
    public Sprite crops;
    public Sprite lake;
    public Sprite river;
    public Sprite mine;
    public Sprite objective;
    public Sprite mountains;
    public Sprite empty;
    public Sprite horse;

    public GameObject infoBox;
    public GameObject battleBox;

    private int troopsO;
    private int weaponsO;
    private int waterO;
    private int foodO;
    private int goldO;
    private int scoreO;

    // Start is called before the first frame update
    void Start()
    {
        SaveOriginals();
        GlobalInfo.isPlaying = false;
        GlobalInfo.isShowingInfo = false;
        GlobalInfo.stagesCount++;
        SetEnviroment();
        StartPlay();
    }

    public void SetEnviroment()
    {
        ShowInfo();
        Levels.LoadLevel(GlobalInfo.actualStage);
        GlobalInfo.objectivesNum = 0;
        GlobalInfo.finalNum = 0;
        stageName.text = GlobalInfo.stageName;
        PaintStage();
        PaintInfo();
    }

    public void ShowInfo()
    {
        troops.text = GlobalInfo.troops.ToString("#,#");
        weapons.text = GlobalInfo.weapons.ToString("#,#");
        water.text = GlobalInfo.water.ToString("#,#");
        food.text = GlobalInfo.food.ToString("#,#");
        gold.text = GlobalInfo.gold.ToString("#,#");
        score.text = GlobalInfo.score.ToString("#,#");
    }

    public void SaveOriginals()
    {
        //Save original values
        troopsO = GlobalInfo.troops;
        weaponsO = GlobalInfo.weapons;
        waterO = GlobalInfo.water;
        foodO = GlobalInfo.food;
        goldO = GlobalInfo.gold;
        scoreO = GlobalInfo.score;
    }

    public void RestoreOriginals()
    {
        //Restore original values
        GlobalInfo.troops = troopsO;
        GlobalInfo.weapons = weaponsO;
        GlobalInfo.water = waterO;
        GlobalInfo.food = foodO;
        GlobalInfo.gold = goldO;
        GlobalInfo.score = scoreO;
    } 

    private void StartPlay()
    {
        GlobalInfo.isPlaying = true;
    }

    private void PaintStage()
    {
        int xx = 0;
        int yy = 0;
        for (int i = 1; i <= 64; i++)
        {
            GameObject cell = GameObject.Find("Cell" + i.ToString());
            GameObject regular = GetChildWithName(cell, "Regualr_Collider_Union");
            GameObject spriteCell = GetChildWithName(regular, "Iso2DObject_Union");

            // GameCell Class info
            cell.GetComponent<GameCell>().num = i;
            cell.GetComponent<GameCell>().x = xx;
            cell.GetComponent<GameCell>().y = yy;
            xx++;
            if (xx == 8)
            {
                xx = 0;
                yy++;
            }

            //Sprite
            if (GlobalInfo.gridStage[i - 1].type > 0)
            {
                spriteCell.GetComponent<SpriteRenderer>().sprite = TypeSprite(GlobalInfo.gridStage[i - 1].type);
                if (GlobalInfo.gridStage[i - 1].isObjective)
                {                    
                    GameObject ObjFlag = GeneralUtils.FindObject(cell, "Flag");
                    ObjFlag.SetActive(true);
                    cell.GetComponent<GameCell>().objective = true;
                    GlobalInfo.objectivesNum++;                    
                }
                if (GlobalInfo.gridStage[i - 1].isFinal)
                {                 
                    GameObject FinFlag = GeneralUtils.FindObject(cell, "FlagF");
                    FinFlag.SetActive(true);
                    cell.GetComponent<GameCell>().final = true;
                    GlobalInfo.finalNum++;
                }
            } else
            {
                Destroy(cell);
            }

            //Player info
            if (GlobalInfo.gridStage[i - 1].isStart == true)
            {
                GlobalInfo.playerPos = i;
            }            
        }
        //Set player
        GameObject player = GameObject.Find("Player");
        player.transform.position = GameObject.Find("Cell" + GlobalInfo.playerPos.ToString()).GetComponent<Transform>().position;

        //Set player cell
        if (GlobalInfo.gridStage[GlobalInfo.playerPos - 1].type !=12 && GlobalInfo.gridStage[GlobalInfo.playerPos - 1].isFinal == false)
        {
            GameObject cellStart = GameObject.Find("Cell" + GlobalInfo.playerPos.ToString());
            GameObject regularStart = GetChildWithName(cellStart, "Regualr_Collider_Union");
            GameObject spriteCellStart = GetChildWithName(regularStart, "Iso2DObject_Union");
            spriteCellStart.GetComponent<SpriteRenderer>().sprite = horse;
        }        
        Invoke("CalculateMovementsAvaliable", 0.5f);
    }

    private void CalculateMovementsAvaliable()
    {
        //Calculate movements avaliable
        GameObject[] cells = GameObject.FindGameObjectsWithTag("GameCell");
        foreach (GameObject cell in cells)
        {
            cell.GetComponent<GameCell>().CalculateMovements();
        }
        GameObject cellStart = GameObject.Find("Cell" + GlobalInfo.playerPos.ToString());
        cellStart.GetComponent<GameCell>().SetMoveables();
        cellStart.GetComponent<GameCell>().ShowMoveables();
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

    public void PaintInfo()
    {
        GameObject.Find("Player").GetComponent<MovePlayer>().ShowInfo();
    }

    public Sprite TypeSprite(int num)
    {
        if (num == 1) { return tundra; }
        if (num == 2) { return desert; }
        if (num == 3) { return woods; }
        if (num == 4) { return town; }
        if (num == 5) { return city; }
        if (num == 6) { return army; }
        if (num == 7) { return crops; }
        if (num == 8) { return lake; }
        if (num == 9) { return river; }
        if (num == 10) { return mine; }
        if (num == 11) { return objective; }
        if (num == 12) { return mountains; }
        return empty;
    }

    public void ToStageMenu()
    {
        SceneManager.LoadScene("StageSelector");
    }

    public void RestartLevel()
    {
        RestoreOriginals();
        SceneManager.LoadScene("Attila");
    }

    public void ShowMoveResult()
    {
        if (GlobalInfo.isPlayerMoving == false && GlobalInfo.isEventAvaliable == true)
        {
            GlobalInfo.isShowingInfo = true;
            infoBox.SetActive(true);
            GameObject.Find("InfoBox").GetComponent<InfoBox>().ShowMoveResult(GlobalInfo.playerPos - 1);
        }        
    }

    public void HideMoveResult()
    {
        GlobalInfo.isShowingInfo = false;
        infoBox.SetActive(false);
    }

    public void ShowBattleResult()
    {
        GlobalInfo.isShowingInfo = true;
        battleBox.SetActive(true);
    }

    public void HideBattleResult()
    {        
        battleBox.SetActive(false);
        ShowMoveResult();
    }

    public void MoveHorse(string origen, string final)
    {
        GameObject.Find("Player").GetComponent<MovePlayer>().Move(final);
    }

    private bool DestionationAvaliable(GameObject dest)
    {        
        return dest.GetComponent<GameCell>().moveable;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.name == "Regualr_Collider_Union" 
                        && GlobalInfo.isPlaying == true 
                        && GlobalInfo.isPlayerMoving == false
                        && GlobalInfo.isShowingInfo == false)
                    {
                        if (DestionationAvaliable(GameObject.Find(hit.collider.gameObject.transform.parent.name)))
                        {
                            MoveHorse("Cell" + GlobalInfo.playerPos.ToString(), hit.collider.gameObject.transform.parent.name);
                        }                        
                    }                    
                }                
            }
        }
    }
}
