using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

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
    public GameObject tutorial1Box;
    public GameObject tutorial2Box;
    public GameObject tutorial3Box;
    public GameObject tutorial4Box;
    public GameObject tutorial5Box;
    public GameObject tutorial6Box;
    public GameObject blockedBox;
    public GameObject winBox;
    public GameObject waitBox;
    public GameObject resourcesBox;

    public Text hunsTroops;
    public Text romanTroops;
    public Text hunsDice;
    public Text romanDice;
    public Text hunsLose;
    public Text romanLose;
    public Text battleResut1;
    public Text battleResut2;
    public Sprite hunsWinSprite;
    public Sprite romanWinSprite;
    public Sprite hunsLoseSprite;
    public Sprite romanLoseSprite;
    public Image hunsSprite;
    public Image romanSprite;
    public Text battleButton1;
    public Text battleButton2;

    private int troopsO;
    private int weaponsO;
    private int waterO;
    private int foodO;
    private int goldO;
    private int scoreO;
    private bool needToRestart;
    private string victory;
    private string defeat;
    private string exit;
    private string restart;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<AudioAttila>().SceneEffect();
        GameObject.Find("GameManager").GetComponent<AdManager>().ShowBanner();
        SaveOriginals();
        GlobalInfo.isPlaying = false;
        GlobalInfo.isShowingInfo = false;
        GlobalInfo.levelCompleted = false;
        GlobalInfo.stagesCount++;
        victory = I2.Loc.LocalizationManager.GetTermTranslation("Victory!");
        defeat = I2.Loc.LocalizationManager.GetTermTranslation("Defeat");
        exit = I2.Loc.LocalizationManager.GetTermTranslation("Exit");
        restart = I2.Loc.LocalizationManager.GetTermTranslation("Restart");
        needToRestart = false;
        SetEnviroment();
        if (GlobalInfo.playFirstTime == true)
        {
            // Save PLAYDATEFIRSTTIME info to CONFIG
            PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
            loadedData.playDateFirstTime = DateTime.Now.ToBinary().ToString();
            DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");
            GlobalInfo.playFirstTime = false;
        }

        //Show tutorials if necesary
        if (GlobalInfo.actualStage==1 && GlobalInfo.showTutorial2 == true)
        {
            ShowTutorial1();
        }
        if (GlobalInfo.actualStage == 2 && GlobalInfo.showTutorial6 == true)
        {
            ShowTutorial5();
        }
        if (GlobalInfo.actualStage == 3 && GlobalInfo.showTutorial7 == true)
        {
            ShowTutorial6();
        }
        if (GlobalInfo.stagesCount % 2 == 0 && GlobalInfo.actualStage > 1)
        {
            GameObject.Find("GameManager").GetComponent<AdManager>().ShowAdInterticial();
        }

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
                spriteCell.GetComponent<Transform>().localScale = spriteCell.GetComponent<Transform>().localScale / 2;
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
        GameObject.Find("GameManager").GetComponent<UIAnimAttila>().ShowArmyEffect();
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
        RestoreOriginals();
        GameObject.Find("GameManager").GetComponent<UIAnimAttila>().HideAllGUIs();
        GameObject.Find("GameManager").GetComponent<AdManager>().StopAdBanner();
        StartCoroutine(ToStageSelector());
    }

    IEnumerator ToStageSelector()
    {
        yield return new WaitForSeconds(1f);
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
        int nType = GlobalInfo.gridStage[GlobalInfo.playerPos - 1].type;
        // Is Town OR City OR Army
        if (nType == 4 || nType == 5 || nType == 6 || nType == 11 || GlobalInfo.gridStage[GlobalInfo.playerPos - 1].isFinal == true || GlobalInfo.gridStage[GlobalInfo.playerPos - 1].isObjective == true)
        {
            if (GlobalInfo.gridStage[GlobalInfo.playerPos - 1].isFinal && GlobalInfo.objectivesNum > 0)
            {                
            } else
            {
                ShowBattleResult();
            }                
        }            
    }

    public void ShowBattleResult()
    {
        if (GlobalInfo.showTutorial4 == true)
        {
            ShowTutorial3();
        } else
        {
            GlobalInfo.isShowingInfo = true;
            //Battle result
            int nRomanTroops;
            int nHunTroops;
            int nRomanDice;
            int nHunDice;
            int nHunLoses;
            int nRomanLoses;
            float nRomanBattle;
            float nHunBattle;
            float nHunFactor;
            float nRomanFactor;

            nRomanTroops = 0;
            nHunLoses = 0;
            nRomanLoses = 0;
            nHunTroops = GlobalInfo.troops;
            hunsTroops.text = nHunTroops.ToString("#,#");
            // Objective
            if (GlobalInfo.gridStage[GlobalInfo.playerPos - 1].type == 11) 
            {
                nRomanTroops = 400 + GlobalInfo.gridStage[GlobalInfo.playerPos - 1].troops;
                romanTroops.text = nRomanTroops.ToString("#,#");
            }
            // Army
            if (GlobalInfo.gridStage[GlobalInfo.playerPos - 1].type == 6)
            {
                nRomanTroops = 2000 + GlobalInfo.gridStage[GlobalInfo.playerPos - 1].troops;
                romanTroops.text = nRomanTroops.ToString("#,#");
            }
            // Town
            if (GlobalInfo.gridStage[GlobalInfo.playerPos - 1].type == 4)
            {
                nRomanTroops = 600 + GlobalInfo.gridStage[GlobalInfo.playerPos - 1].troops;
                romanTroops.text = nRomanTroops.ToString("#,#");
            }
            // City
            if (GlobalInfo.gridStage[GlobalInfo.playerPos - 1].type == 5)
            {
                nRomanTroops = 3000 + GlobalInfo.gridStage[GlobalInfo.playerPos - 1].troops;
                romanTroops.text = nRomanTroops.ToString("#,#");
            }
            //Dice
            nRomanDice = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 6f));
            nHunDice = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 6f));
            hunsDice.text = nHunDice.ToString();
            romanDice.text = nRomanDice.ToString();
            nRomanBattle = nRomanTroops * nRomanDice;
            nHunBattle = nHunTroops * nHunDice;
            Debug.Log(nHunBattle);
            Debug.Log(nRomanBattle);

            //Huns wins
            if (nHunBattle > nRomanBattle)
            {
                battleResut1.text = victory;
                battleResut2.text = victory;
                battleButton1.text = exit;
                battleButton2.text = exit;

                //Huns loses some troops                
                nHunFactor = nHunBattle / (nHunBattle + nRomanBattle);
                nRomanFactor = nRomanBattle / (nHunBattle + nRomanBattle);
                Debug.Log(nHunFactor);
                Debug.Log(nRomanFactor);
                nHunBattle = nRomanFactor;
                nHunLoses = Mathf.RoundToInt(nHunTroops * nHunBattle);
                nHunTroops = nHunTroops - nHunLoses;
                
                //Roman loses all troops
                nRomanLoses = nRomanTroops;
                nRomanTroops = 0;
                GlobalInfo.troops = nHunTroops;                
                hunsSprite.sprite = hunsWinSprite;
                romanSprite.sprite = romanLoseSprite;
                    
            } else //Romans wins
            {
                battleResut1.text = defeat;
                battleResut2.text = defeat;
                battleButton1.text = restart;
                battleButton2.text = restart;

                nHunFactor = nHunBattle / (nHunBattle + nRomanBattle);
                nRomanFactor = nRomanBattle / (nHunBattle + nRomanBattle);
                nRomanBattle = nHunFactor;
                nRomanLoses = Mathf.RoundToInt(nRomanTroops * nRomanBattle);
                nRomanTroops = nRomanTroops - nRomanLoses;

                // Huns loses all troops
                needToRestart = true;
                nHunLoses = nHunTroops;
                nHunTroops = 0;
                GlobalInfo.troops = 0;
                hunsSprite.sprite = hunsLoseSprite;
                romanSprite.sprite = romanWinSprite;                
            }

            //Show
            hunsLose.text = "-" + nHunLoses.ToString("#,#");
            romanLose.text = "-" + nRomanLoses.ToString("#,#");
            GameObject.Find("GameManager").GetComponent<AudioAttila>().BattleEffect();
            StartCoroutine(ShowBattlePoints(nHunTroops, nRomanTroops));
        }        
    }

    IEnumerator ShowBattlePoints(int huns, int romans)
    {        
        waitBox.SetActive(true);
        GameObject.Find("Timer").GetComponent<Animator>().StopPlayback();
        yield return new WaitForSeconds(2f);
        GameObject.Find("Timer").GetComponent<Animator>().SetTrigger("Fisnish");
        waitBox.SetActive(false);
        battleBox.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject.Find("GameManager").GetComponent<UIAnimAttila>().ShowBattleLost();
        yield return new WaitForSeconds(3f);
        hunsTroops.text = huns.ToString("#,#");
        romanTroops.text = romans.ToString("#,#");
        if (huns == 0)
        {
            hunsTroops.text = "0";
        } else
        {
            romanTroops.text = "0";
        }
        PaintInfo();
    }

    public void HideBattleResult()
    {
        if (needToRestart)
        {
            RestartLevel();
        } else
        {
            battleBox.SetActive(false);
            GlobalInfo.isShowingInfo = false;
            if (GlobalInfo.levelCompleted == true)
            {
                ShowWinBox();
            }
        }        
    }

    public void ShowBlockedBox()
    {        
        GlobalInfo.isShowingInfo = true;
        blockedBox.SetActive(true);
    }

    public void HideBlockedBox()
    {
        blockedBox.SetActive(false);
        GlobalInfo.isShowingInfo = false;
        RestartLevel();
    }

    public void ShowResourcesBox()
    {
        GlobalInfo.isShowingInfo = true;
        resourcesBox.SetActive(true);
    }

    public void HideResourcesBox()
    {
        resourcesBox.SetActive(false);
        GlobalInfo.isShowingInfo = false;
        RestartLevel();
    }

    public void ShowWinBox()
    {
        GlobalInfo.isShowingInfo = true;
        GameObject.Find("GameManager").GetComponent<AudioAttila>().VictoryEffect();
        winBox.SetActive(true);
    }

    public void HideWinBox()
    {
        winBox.SetActive(false);
        GlobalInfo.isShowingInfo = false;
        GameObject.Find("Player").GetComponent<MovePlayer>().LoadNextLevel();
    }

    public void MoveHorse(string origen, string final)
    {
        GameObject.Find("GameManager").GetComponent<AudioAttila>().MoveEffect();
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

    public void ShowTutorial1()
    {
        GlobalInfo.isShowingInfo = true;
        tutorial1Box.SetActive(true);
    }

    public void HideTutorial1()
    {
        tutorial1Box.SetActive(false);
        GlobalInfo.showTutorial2 = false;
        GlobalInfo.isShowingInfo = false;
        ShowTutorial2();
    }

    public void ShowTutorial2()
    {
        GlobalInfo.isShowingInfo = true;
        tutorial2Box.SetActive(true);
    }

    public void HideTutorial2()
    {
        tutorial2Box.SetActive(false);
        GlobalInfo.showTutorial3 = false;
        GlobalInfo.isShowingInfo = false;
    }

    public void ShowTutorial3()
    {
        GlobalInfo.isShowingInfo = true;
        tutorial3Box.SetActive(true);
    }

    public void HideTutorial3()
    {
        tutorial3Box.SetActive(false);
        GlobalInfo.showTutorial4 = false;
        GlobalInfo.isShowingInfo = false;
        ShowTutorial4();
    }

    public void ShowTutorial4()
    {
        GlobalInfo.isShowingInfo = true;
        tutorial4Box.SetActive(true);
    }

    public void HideTutorial4()
    {
        tutorial4Box.SetActive(false);
        GlobalInfo.showTutorial5 = false;
        GlobalInfo.isShowingInfo = true;
        GlobalInfo.isShowingInfo = false;
        ShowBattleResult();
    }

    public void ShowTutorial5()
    {
        GlobalInfo.isShowingInfo = true;
        tutorial5Box.SetActive(true);
    }

    public void HideTutorial5()
    {
        tutorial5Box.SetActive(false);
        GlobalInfo.showTutorial6 = false;
        GlobalInfo.isShowingInfo = false;        
    }

    public void ShowTutorial6()
    {
        GlobalInfo.isShowingInfo = true;
        tutorial6Box.SetActive(true);
    }

    public void HideTutorial6()
    {
        tutorial6Box.SetActive(false);
        GlobalInfo.showTutorial7 = false;        
        GlobalInfo.isShowingInfo = false;
        FinishTutorials();
    }

    public void FinishTutorials()
    {        
        PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
        loadedData.showTutorial = false;
        DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");
        GlobalInfo.showTutorial = false;
    }
}
