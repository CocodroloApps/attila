using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    public Text langCode;

    public void LoadNewLanguage()
    {
        I2.Loc.LocalizationManager.CurrentLanguageCode = langCode.text;
        GlobalInfo.language = langCode.text;
        PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
        loadedData.language = GlobalInfo.language;
        DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");
    }
}
