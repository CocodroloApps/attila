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
    public List<StageCell> gridStage;
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
