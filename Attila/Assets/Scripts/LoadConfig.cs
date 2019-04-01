using UnityEngine;
using System.IO;
using System;

public class LoadConfig : MonoBehaviour {

    private string configFileName;
    private string highScoreFileName;
    private string fileName;

	// Use this for initialization
	void Awake ()
    {
        //Configuration
        configFileName = GlobalInfo.configFile;
        fileName = Path.Combine(Application.persistentDataPath, "data");
        fileName = Path.Combine(fileName, configFileName + ".txt");
        if (!File.Exists(fileName))
        {
            PlayerInfo saveData = new PlayerInfo();
            saveData.gameDateFirstTime = DateTime.Now.ToBinary().ToString();
            saveData.playDateFirstTime = "";
            IntialConditions cond = new IntialConditions();
            saveData.maxStagesGame = cond.maxStagesGame;
            saveData.actualStage = cond.actualStage;
            saveData.maxStageCompleted = cond.maxStageCompleted;
            saveData.score = cond.score;
            saveData.water = cond.water;
            saveData.food = cond.food;
            saveData.troops = cond.troops;
            saveData.weapons = cond.weapons;
            saveData.gold = cond.gold;

            //Save data from PlayerInfo to a file named players
            DataSaver.saveData(saveData, configFileName);

            GlobalInfo.gameFirstTime = true;
            GlobalInfo.playFirstTime = true;
            GlobalInfo.language = saveData.language;
            GlobalInfo.soundPlay = true;
            GlobalInfo.sessionsCount = 0;
            GlobalInfo.stagesCount = 0;
            GlobalInfo.maxStageCompleted = 0;
                        
            //Initial conditions            
            GlobalInfo.maxStagesGame = cond.maxStagesGame;
            GlobalInfo.actualStage = cond.actualStage;
            GlobalInfo.score = cond.score;
            GlobalInfo.water = cond.water;
            GlobalInfo.food = cond.food;
            GlobalInfo.troops = cond.troops;
            GlobalInfo.weapons = cond.weapons;
            GlobalInfo.gold = cond.gold;
        } else
        {
            PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(configFileName);
            if (loadedData == null)
            {
                return;
            }
            
            GlobalInfo.gameFirstTime = false;
            if (loadedData.playDateFirstTime != "")
            {
                GlobalInfo.playFirstTime = false;
            } else
            {
                GlobalInfo.playFirstTime = true;
            }

            GlobalInfo.language = loadedData.language;
            GlobalInfo.soundPlay = loadedData.soundPlay;
            GlobalInfo.sessionsCount = loadedData.sessionsCount;
            GlobalInfo.stagesCount = 0;

            GlobalInfo.maxStagesGame = loadedData.maxStagesGame;
            GlobalInfo.maxStageCompleted = loadedData.maxStageCompleted;
            GlobalInfo.actualStage = loadedData.actualStage;
            GlobalInfo.score = loadedData.score;
            GlobalInfo.water = loadedData.water;
            GlobalInfo.food = loadedData.food;
            GlobalInfo.troops = loadedData.troops;
            GlobalInfo.weapons = loadedData.weapons;
            GlobalInfo.gold = loadedData.gold;
        }
    }
}
