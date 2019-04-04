using System.Collections.Generic;
using System;

[Serializable]
public class PlayerInfo
{
    public int version = 1;
    public string gameDateFirstTime;
    public string playDateFirstTime;
    public int sessionsCount;
    public string language = "en";
    public bool soundPlay = true;

    //Game
    public int maxStagesGame;
    public int maxStageCompleted;
    public int actualStage;
    public int score;
    public int water;
    public int food;
    public int troops;
    public int weapons;
    public int gold;
}

public class IntialConditions
{
    public int version;
    //Game
    public int maxStagesGame;
    public int actualStage;
    public int maxStageCompleted;
    public int score;
    public int water;
    public int food;
    public int troops;
    public int weapons;
    public int gold;

    public IntialConditions()
    {
        version = 1;    
        maxStagesGame = 2;
        maxStageCompleted = 0;
        actualStage = 1;
        score = 0;
        water = 100;
        food = 100;
        troops = 1000;
        weapons = 2000;
        gold = 10000;
    }
}




