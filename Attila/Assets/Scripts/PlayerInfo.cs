using System.Collections.Generic;
using System;

[Serializable]
public class PlayerInfo
{
    public int version = 1;
    public int levelsVersion;
    public string gameDateFirstTime;
    public string playDateFirstTime;
    public int sessionsCount;
    public string language = "en";
    public bool soundPlay = true;
    public bool showTutorial = true;
    
    //Game
    public int maxStagesGame;
    public int maxStageCompleted;
    public int actualStage;
    public int spyMoves;
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
    public int levelsVersion;
    //Game
    public int maxStagesGame;
    public int actualStage;
    public int maxStageCompleted;
    public int spyMoves;
    public int score;
    public int water;
    public int food;
    public int troops;
    public int weapons;
    public int gold;

    public IntialConditions()
    {
        version = 1;
        levelsVersion = 0;
        maxStagesGame = 10;
        maxStageCompleted = 0;
        spyMoves = 5;
        actualStage = 1;
        score = 0;
        troops = 10000;
        weapons = 10000;
        water = 35000;
        food = 35000;                
        gold = 50000;
    }
}




