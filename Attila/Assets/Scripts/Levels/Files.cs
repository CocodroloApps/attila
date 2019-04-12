using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Files : MonoBehaviour
{
    public Text stageName;
    public Text stageFile;
    public Text stageNum;
    public GameObject line;

    public void LoadStageFile()
    {
        string fileName = Path.Combine(Application.persistentDataPath, "levels");
        fileName = Path.Combine(fileName, stageFile.text);
        GameObject.Find("EditorManager").GetComponent<Editor>().OpenFile(fileName, stageName.text, stageNum.text);

        GameObject[] cells = GameObject.FindGameObjectsWithTag("FileCell");
        foreach (GameObject cell in cells)
        {
            cell.GetComponent<Files>().HideLine();
        }
        ShowLine();
    }

    public void ShowLine()
    {
        line.SetActive(true);
    }

    public void HideLine()
    {
        line.SetActive(false);
    }

}
