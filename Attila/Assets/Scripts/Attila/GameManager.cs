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

    // Start is called before the first frame update
    void Start()
    {
        GlobalInfo.isPlaying = false;
        GlobalInfo.stagesCount++;
        SetEnviroment();
        StartPlay();
    }

    public void SetEnviroment()
    {
        troops.text = GlobalInfo.troops.ToString("#,#");
        weapons.text = GlobalInfo.weapons.ToString("#,#");
        water.text = GlobalInfo.water.ToString("#,#");
        food.text = GlobalInfo.food.ToString("#,#");
        gold.text = GlobalInfo.gold.ToString("#,#");

        Levels.LoadLevel(GlobalInfo.actualStage);
        GlobalInfo.objectivesCompleted = 0;
        stageName.text = GlobalInfo.stageName;
        PaintStage();
    }

    private void StartPlay()
    {
        GlobalInfo.isPlaying = true;
    }

    private void PaintStage()
    {        
        for (int i = 1; i <= 64; i++)
        {
            GameObject cell = GameObject.Find("Cell" + i.ToString());
            GameObject regular = GetChildWithName(cell, "Regualr_Collider_Union");
            GameObject spriteCell = GetChildWithName(regular, "Iso2DObject_Union");
            if (GlobalInfo.gridStage[i - 1].type > 0)
            {
                spriteCell.GetComponent<SpriteRenderer>().sprite = TypeSprite(GlobalInfo.gridStage[i - 1].type);
            } else
            {
                GameObject.Destroy(cell);
            }
            if (GlobalInfo.gridStage[i - 1].isStart == true)
            {
                GlobalInfo.playerPos = i;
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
                    Debug.Log(GlobalInfo.isPlaying);
                    if (hit.collider.gameObject.name == "Regualr_Collider_Union" && GlobalInfo.isPlaying == true)
                    {
                        GameObject.Find("Player").GetComponent<MovePlayer>().Move(hit.collider.gameObject.transform.parent.name);
                    }                    
                }                
            }
        }
    }
}
