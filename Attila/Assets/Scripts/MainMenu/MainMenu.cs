using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalInfo.sessionsCount++;
        StartCoroutine(SaveSessionsConfig());
    }

    IEnumerator SaveSessionsConfig()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");        
        loadedData.sessionsCount = GlobalInfo.sessionsCount;
        DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");        
    }

    public void PlayGame()
    {
        if (GlobalInfo.playFirstTime == true)
        {
            // Save PLAYDATEFIRSTTIME info to CONFIG
            PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
            loadedData.playDateFirstTime = DateTime.Now.ToBinary().ToString();
            DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");
            GlobalInfo.playFirstTime = false;
        }
        SceneManager.LoadScene("StageSelector");
    }
}
