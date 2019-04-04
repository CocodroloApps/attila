using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DefaultCells
{
    public int version;
    //Game
    public int type;
    public int water;
    public int food;
    public int troops;
    public int weapons;
    public int gold;

    public DefaultCells()
    {        
        IsEmpy();
    }

    public void IsTundra()
    {
        version = 1;
        type = 1;
        water = 0;
        food = 5;
        troops = 100;
        weapons = 50;
        gold = 0;
    }

    public void IsDessert()
    {
        version = 1;
        type = 2;
        water = 0;
        food = 0;
        troops = -100;
        weapons = 0;
        gold = 0;
    }

    public void IsWood()
    {
        version = 1;
        type = 3;
        water = 5;
        food = 10;
        troops = 0;
        weapons = 5;
        gold = 0;
    }

    public void IsTown()
    {
        version = 1;
        type = 4;
        water = 10;
        food = 50;
        troops = 50;
        weapons = 50;
        gold = 50;
    }

    public void IsCity()
    {
        version = 1;
        type = 5;
        water = 100;
        food = 500;
        troops = 800;
        weapons = 100;
        gold = 500;
    }

    public void IsArmy()
    {
        version = 1;
        type = 6;
        water = 0;
        food = 50;
        troops = 1000;
        weapons = 500;
        gold = 800;
    }

    public void IsCrop()
    {
        version = 1;
        type = 7;
        water = 5;
        food = 500;
        troops = 10;
        weapons = 5;
        gold = 5;
    }

    public void IsLake()
    {
        version = 1;
        type = 8;
        water = 500;
        food = 10;
        troops = 0;
        weapons = 0;
        gold = 5;
    }

    public void IsRiver()
    {
        version = 1;
        type = 9;
        water = 500;
        food = 10;
        troops = 0;
        weapons = 0;
        gold = 5;
    }

    public void IsMine()
    {
        version = 1;
        type = 10;
        water = 5;
        food = 5;
        troops = 20;
        weapons = 20;
        gold = 500;
    }

    public void IsObjective()
    {
        version = 1;
        type = 11;
        water = 50;
        food = 50;
        troops = 50;
        weapons = 50;
        gold = 50;
    }

    public void IsMountain()
    {
        version = 1;
        type = 12;
        water = 50;
        food = 50;
        troops = -50;
        weapons = 5;
        gold = 10;
    }

    public void IsEmpy()
    {
        version = 1;
        type = 0;
        water = 0;
        food = 0;
        troops = 0;
        weapons = 0;
        gold = 0;
    }
}


public class Editor : MonoBehaviour
{
    public GameObject saveBox;
    public GameObject newBox;
    public GameObject fileInfoItem;
    public GameObject editTile;

    private Stage editorStage;

    private void ShowFiles(string filePath, string where)
    {
        DirectoryInfo dir = new DirectoryInfo(filePath);
        FileInfo[] info = dir.GetFiles("*.json");

        GameObject itemsParent = GameObject.Find(where);
        foreach (Transform child in itemsParent.transform)
        {
            Destroy(child.gameObject);
        }

        //Find files
        foreach (FileInfo f in info)
        {
            Debug.Log(f.ToString());

            //Read File info
            Stage stage = new Stage();
            stage = DataSaver.loadData<Stage>(f.ToString(),"");
            
            //Edit Prefab before Instantiate
            Transform levelTitle = fileInfoItem.GetComponentInChildren<Transform>().Find("Name");
            levelTitle.GetComponent<Text>().text = stage.nameStage;
            Transform levelNum = fileInfoItem.GetComponentInChildren<Transform>().Find("Num");
            levelNum.GetComponent<Text>().text = stage.numStage.ToString();
            Transform levelFilename = fileInfoItem.GetComponentInChildren<Transform>().Find("File");
            levelFilename.GetComponent<Text>().text = Path.GetFileName(f.ToString());

            Instantiate(fileInfoItem, new Vector3(0, 0, 0), Quaternion.identity, itemsParent.transform);            
        }
    }

    public void OpenNewPanel()
    {
        newBox.SetActive(true);
        GameObject inputNumField = GameObject.Find("InputStageNum");
        inputNumField.GetComponent<InputField>().text = "0";
    }

    public void NewStage()
    {
        GameObject inputNumField = GameObject.Find("InputStageNum");        
        if (inputNumField.GetComponent<InputField>().text != "0")
        {
            editorStage.numStage = int.Parse(inputNumField.GetComponent<InputField>().text);
            GameObject.Find("Grid").GetComponent<Grid>().PaintCells();
            GameObject[] cells = GameObject.FindGameObjectsWithTag("EditorCell");
            int iAux = 0;            
            foreach (GameObject cell in cells)
            {                
                editorStage.gridStage[iAux].x = cell.GetComponent<Cell>().info.x;
                editorStage.gridStage[iAux].y = cell.GetComponent<Cell>().info.y;
                editorStage.gridStage[iAux].listPos = cell.GetComponent<Cell>().info.listPos;
                editorStage.gridStage[iAux].type = cell.GetComponent<Cell>().info.type;
                editorStage.gridStage[iAux].isFinal = cell.GetComponent<Cell>().info.isFinal;
                editorStage.gridStage[iAux].isObjective = cell.GetComponent<Cell>().info.isObjective;
                editorStage.gridStage[iAux].water = cell.GetComponent<Cell>().info.water;
                editorStage.gridStage[iAux].food = cell.GetComponent<Cell>().info.food;
                editorStage.gridStage[iAux].troops = cell.GetComponent<Cell>().info.troops;
                editorStage.gridStage[iAux].weapons = cell.GetComponent<Cell>().info.weapons;
                editorStage.gridStage[iAux].gold = cell.GetComponent<Cell>().info.gold;
                iAux++;
            }
            GameObject.Find("StageNumText").GetComponent<Text>().text = editorStage.numStage.ToString();
            CloseNewPanel();
        }
    }

    public void CloseNewPanel()
    {
        newBox.SetActive(false);
    }

    public void OpenSavePanel()
    {
        if (editorStage.numStage > 0)
        {
            saveBox.SetActive(true);
            GameObject inputNameField = GameObject.Find("InputStageName");
            inputNameField.GetComponent<InputField>().text = editorStage.nameStage;
            GameObject inputFileNameField = GameObject.Find("InputFileName");
            inputFileNameField.GetComponent<InputField>().text = editorStage.fileName;
        }        
    }

    public void SaveFile()
    {
        GameObject inputNameField = GameObject.Find("InputStageName");        
        GameObject inputFileNameField = GameObject.Find("InputFileName");

        if (inputFileNameField.GetComponent<InputField>().text !="")
        {
            editorStage.nameStage = inputNameField.GetComponent<InputField>().text;
            editorStage.fileName = inputFileNameField.GetComponent<InputField>().text;            
            string fileName = Path.Combine(Application.persistentDataPath, "levels");
            fileName = Path.Combine(fileName, editorStage.fileName + "." + "json");            
            DataSaver.saveData(editorStage, fileName, "");
            ShowFiles(Path.Combine(Application.persistentDataPath, "levels"), "ObjectGrid");
            CloseSavePanel();
        }        
    }

    public void CloseSavePanel()
    {
        saveBox.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        editorStage = new Stage();
        string filePath = Path.Combine(Application.persistentDataPath, "levels");
        ShowFiles(filePath, "ObjectGrid");
        editorStage.nameStage = "";
        editorStage.fileName = "";
        editorStage.numStage = 0;
        DefaultCells cond = new DefaultCells();
        editorStage.version = cond.version;
    }
}
