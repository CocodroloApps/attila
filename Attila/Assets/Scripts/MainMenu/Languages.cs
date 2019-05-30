using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Languages : MonoBehaviour
{
    public GameObject langPrefab;
    public GameObject itemsParent;
    
    // Start is called before the first frame update
    void Start()
    {
        I2.Loc.LocalizationManager.CurrentLanguageCode = GlobalInfo.language;
        List<string> langs = new List<string>();
        langs = I2.Loc.LocalizationManager.GetAllLanguagesCode();
        List<string> langsName = new List<string>();
        langsName = I2.Loc.LocalizationManager.GetAllLanguages();

        for (int i = 0; i < langs.Count ; i++)
        {
            //Edit Prefab before Instantiate
            Transform langTitle = langPrefab.GetComponentInChildren<Transform>().Find("Name");           
            langTitle.GetComponent<Text>().text = I2.Loc.LocalizationManager.GetTermTranslation("Language", true, 0, true, false, null, langsName[i]);
            Transform langCode = langPrefab.GetComponentInChildren<Transform>().Find("Code");
            langCode.GetComponent<Text>().text = langs[i].ToUpper();

            Instantiate(langPrefab, new Vector3(0, 0, 0), Quaternion.identity, itemsParent.transform);
        }        
    }
}
