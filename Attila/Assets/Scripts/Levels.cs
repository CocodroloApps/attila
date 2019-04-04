using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

[Serializable]
public class StageCell
{
    public int x;
    public int y;
    public int listPos;
    public int type;
    public bool isFinal;
    public bool isObjective;
    public int water;
    public int food;
    public int troops;
    public int weapons;
    public int gold;
}

[Serializable]
public class Stage
{
    public int version;
    public int numStage;
    public string nameStage;
    public string fileName;
    public List<StageCell> gridStage = new List<StageCell>();

    public Stage()
    {        
        DefaultCells cond = new DefaultCells();
        version = cond.version;
        numStage = 0;
        nameStage = "";
        fileName = "";        
        for (int i = 0; i < 64; i++)
        {
            StageCell defaultCell = new StageCell();
            defaultCell.x = 0;
            defaultCell.y = 0;
            defaultCell.listPos = 0;
            defaultCell.type = cond.type;
            defaultCell.isFinal = false;
            defaultCell.isObjective = false;
            defaultCell.water = cond.water;
            defaultCell.food = cond.food;
            defaultCell.troops = cond.troops;
            defaultCell.weapons = cond.weapons;
            defaultCell.gold = cond.gold;
            gridStage.Add(defaultCell);
        }
    }
}

public class Levels : MonoBehaviour
{

    public static void LoadLevel(int levelNum)
    {
        //load info from levels folder       
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Levels/Attila" + levelNum.ToString());
        object resultValue = JsonUtility.FromJson<Stage>(Encoding.ASCII.GetString(jsonTextFile.bytes));
        Stage loadlevel = (Stage)Convert.ChangeType(resultValue, typeof(Stage));

        GlobalInfo.stageVersion = loadlevel.version; 
        GlobalInfo.actualStage = loadlevel.numStage;
        GlobalInfo.gridStage = loadlevel.gridStage;
    }
}
