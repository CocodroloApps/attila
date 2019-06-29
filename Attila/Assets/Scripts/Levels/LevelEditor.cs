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
    public bool isFinal;
    public bool isObjective;
    public bool isStart;

    public DefaultCells()
    {
        IsEmtpy();
    }

    public void SetValues(int num)
    {
        version = 1;
        if (num == 0) { IsEmtpy(); }
        if (num == 1) { IsTundra(); }
        if (num == 2) { IsDesert(); }
        if (num == 3) { IsWood(); }
        if (num == 4) { IsTown(); }
        if (num == 5) { IsCity(); }
        if (num == 6) { IsArmy(); }
        if (num == 7) { IsCrop(); }
        if (num == 8) { IsLake(); }
        if (num == 9) { IsRiver(); }
        if (num == 10) { IsMine(); }
        if (num == 11) { IsObjective(); }
        if (num == 12) { IsMountain(); }
    }

    public void IsTundra()
    {
        version = 1;
        type = 1;
        water = 0;
        food = 500;
        troops = 100;
        weapons = 100;
        gold = 0;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsDesert()
    {
        version = 1;
        type = 2;
        water = 0;
        food = 0;
        troops = -100;
        weapons = 0;
        gold = 0;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsWood()
    {
        version = 1;
        type = 3;
        water = 500;
        food = 500;
        troops = 0;
        weapons = 500;
        gold = 50;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsTown()
    {
        version = 1;
        type = 4;
        water = 1000;
        food = 5000;
        troops = 50;
        weapons = 200;
        gold = 10000;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsCity()
    {
        version = 1;
        type = 5;
        water = 50000;
        food = 50000;
        troops = 800;
        weapons = 1000;
        gold = 70000;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsArmy()
    {
        version = 1;
        type = 6;
        water = 0;
        food = 100;
        troops = 1000;
        weapons = 2000;
        gold = 10000;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsCrop()
    {
        version = 1;
        type = 7;
        water = 25000;
        food = 50000;
        troops = 50;
        weapons = 25;
        gold = 500;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsLake()
    {
        version = 1;
        type = 8;
        water = 50000;
        food = 10000;
        troops = 0;
        weapons = 0;
        gold = 500;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsRiver()
    {
        version = 1;
        type = 9;
        water = 50000;
        food = 10000;
        troops = 0;
        weapons = 0;
        gold = 5000;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsMine()
    {
        version = 1;
        type = 10;
        water = 50;
        food = 500;
        troops = 20;
        weapons = 200;
        gold = 50000;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsObjective()
    {
        version = 1;
        type = 11;
        water = 5000;
        food = 5000;
        troops = 500;
        weapons = 5000;
        gold = 9000;
        isFinal = false;
        isObjective = true;
        isStart = false;
    }

    public void IsMountain()
    {
        version = 1;
        type = 12;
        water = 500;
        food = 500;
        troops = -50;
        weapons = 5;
        gold = 100;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }

    public void IsEmtpy()
    {
        version = 1;
        type = 0;
        water = 0;
        food = 0;
        troops = 0;
        weapons = 0;
        gold = 0;
        isFinal = false;
        isObjective = false;
        isStart = false;
    }
}


public class LevelEditor : MonoBehaviour
{
    public GameObject saveBox;
    public GameObject newBox;
    public GameObject modifyBox;
    public GameObject fileInfoItem;

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

    public void OpenFile(string file, string name, string num)
    {               
        editorStage = DataSaver.loadData<Stage>(file, "");
        editorStage.nameStage = name;
        editorStage.numStage = int.Parse(num);
        GlobalInfo.editingCell = -1;
        GameObject.Find("Grid").GetComponent<Grid>().Clean();
        Invoke("UpdateGrid", 1.2f);        
        GameObject.Find("StageNumText").GetComponent<Text>().text = editorStage.numStage.ToString();
    }

    private void UpdateGrid()
    {
        GameObject.Find("Grid").GetComponent<Grid>().PaintCells();
        GameObject[] cells = GameObject.FindGameObjectsWithTag("EditorCell");
        int iAux = 0;
        foreach (GameObject cell in cells)
        {
            cell.GetComponent<Cell>().info.x = editorStage.gridStage[iAux].x;
            cell.GetComponent<Cell>().info.y = editorStage.gridStage[iAux].y;
            cell.GetComponent<Cell>().info.listPos = editorStage.gridStage[iAux].listPos;
            cell.GetComponent<Cell>().info.type = editorStage.gridStage[iAux].type;
            cell.GetComponent<Cell>().info.isFinal = editorStage.gridStage[iAux].isFinal;
            cell.GetComponent<Cell>().info.isObjective = editorStage.gridStage[iAux].isObjective;
            cell.GetComponent<Cell>().info.isStart = editorStage.gridStage[iAux].isStart;
            cell.GetComponent<Cell>().info.water = editorStage.gridStage[iAux].water;
            cell.GetComponent<Cell>().info.food = editorStage.gridStage[iAux].food;
            cell.GetComponent<Cell>().info.troops = editorStage.gridStage[iAux].troops;
            cell.GetComponent<Cell>().info.weapons = editorStage.gridStage[iAux].weapons;
            cell.GetComponent<Cell>().info.gold = editorStage.gridStage[iAux].gold;
            
            cell.GetComponent<Cell>().ShowCell();
            iAux++;
        }
    }

    public void OpenNewPanel()
    {
        GlobalInfo.isModifying = true;
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
            GameObject.Find("Grid").GetComponent<Grid>().Clean();
            Invoke("UpdateStage", 1.0f);
            GameObject.Find("StageNumText").GetComponent<Text>().text = editorStage.numStage.ToString();
            GlobalInfo.editingCell = -1;
            CloseNewPanel();
        }
    }

    private void UpdateStage()
    {
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
            editorStage.gridStage[iAux].isStart = cell.GetComponent<Cell>().info.isStart;
            editorStage.gridStage[iAux].water = cell.GetComponent<Cell>().info.water;
            editorStage.gridStage[iAux].food = cell.GetComponent<Cell>().info.food;
            editorStage.gridStage[iAux].troops = cell.GetComponent<Cell>().info.troops;
            editorStage.gridStage[iAux].weapons = cell.GetComponent<Cell>().info.weapons;
            editorStage.gridStage[iAux].gold = cell.GetComponent<Cell>().info.gold;
            iAux++;
        }
    }

    public void CloseNewPanel()
    {
        newBox.SetActive(false);
        GlobalInfo.isModifying = false;
    }

    public void OpenSavePanel()
    {
        if (editorStage.numStage > 0)
        {
            GlobalInfo.isModifying = true;
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
            UpdateEditorStage();
            DataSaver.saveData(editorStage, fileName, "");
            ShowFiles(Path.Combine(Application.persistentDataPath, "levels"), "ObjectGrid");
            CloseSavePanel();
        }        
    }

    public void CloseSavePanel()
    {
        saveBox.SetActive(false);
        GlobalInfo.isModifying = false;
    }

    public void OpenModifyPanel()
    {        
        if (GlobalInfo.editingCell >= 0)
        {
            GameObject clickedCell = GameObject.Find("Cell" + (GlobalInfo.editingCell + 1).ToString() + "(Clone)");            
            if (clickedCell.GetComponent<Cell>().info.type > 0)
            {
                GlobalInfo.isModifying = true;
                modifyBox.SetActive(true);                
                clickedCell.GetComponent<Cell>().ShowStaticBorder();

                GameObject inputTroopsField = GameObject.Find("InputTroops");
                GameObject inputWeaponsField = GameObject.Find("InputWeapons");
                GameObject inputWaterField = GameObject.Find("InputWater");
                GameObject inputFoodField = GameObject.Find("InputFood");
                GameObject inputGoldField = GameObject.Find("InputGold");

                inputTroopsField.GetComponent<InputField>().text = clickedCell.GetComponent<Cell>().info.troops.ToString();
                inputWeaponsField.GetComponent<InputField>().text = clickedCell.GetComponent<Cell>().info.weapons.ToString();
                inputWaterField.GetComponent<InputField>().text = clickedCell.GetComponent<Cell>().info.water.ToString();
                inputFoodField.GetComponent<InputField>().text = clickedCell.GetComponent<Cell>().info.food.ToString();
                inputGoldField.GetComponent<InputField>().text = clickedCell.GetComponent<Cell>().info.gold.ToString();

                GameObject inputFinalField = GameObject.Find("isFinal");
                GameObject inputObjectiveField = GameObject.Find("isObjective");
                GameObject inputStartField = GameObject.Find("isStart");

                inputFinalField.GetComponent<Toggle>().isOn = clickedCell.GetComponent<Cell>().info.isFinal;
                inputObjectiveField.GetComponent<Toggle>().isOn = clickedCell.GetComponent<Cell>().info.isObjective;
                inputStartField.GetComponent<Toggle>().isOn = clickedCell.GetComponent<Cell>().info.isStart;
            }            
        }        
    }

    public void Modify()
    {
        GameObject clickedCell = GameObject.Find("Cell" + (GlobalInfo.editingCell + 1).ToString() + "(Clone)");
        GameObject inputTroopsField = GameObject.Find("InputTroops");
        GameObject inputWeaponsField = GameObject.Find("InputWeapons");
        GameObject inputWaterField = GameObject.Find("InputWater");
        GameObject inputFoodField = GameObject.Find("InputFood");
        GameObject inputGoldField = GameObject.Find("InputGold");
        GameObject inputFinalField = GameObject.Find("isFinal");
        GameObject inputObjectiveField = GameObject.Find("isObjective");
        GameObject inputStartField = GameObject.Find("isStart");

        clickedCell.GetComponent<Cell>().info.troops = int.Parse(inputTroopsField.GetComponent<InputField>().text);
        clickedCell.GetComponent<Cell>().info.weapons = int.Parse(inputWeaponsField.GetComponent<InputField>().text);
        clickedCell.GetComponent<Cell>().info.water = int.Parse(inputWaterField.GetComponent<InputField>().text);
        clickedCell.GetComponent<Cell>().info.food = int.Parse(inputFoodField.GetComponent<InputField>().text);
        clickedCell.GetComponent<Cell>().info.gold = int.Parse(inputGoldField.GetComponent<InputField>().text);
        clickedCell.GetComponent<Cell>().info.isFinal = inputFinalField.GetComponent<Toggle>().isOn;
        clickedCell.GetComponent<Cell>().info.isObjective = inputObjectiveField.GetComponent<Toggle>().isOn;
        clickedCell.GetComponent<Cell>().info.isStart = inputStartField.GetComponent<Toggle>().isOn;

        UpdateEditorStage();
        clickedCell.GetComponent<Cell>().ShowInfo();
        CloseModifyPanel();
    }

    public void CloseModifyPanel()
    {
        GameObject clickedCell = GameObject.Find("Cell" + (GlobalInfo.editingCell + 1).ToString() + "(Clone)");
        clickedCell.GetComponent<Cell>().HideBorder();
        modifyBox.SetActive(false);
        GlobalInfo.isModifying = false;
    }

    private void UpdateEditorStage()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("EditorCell");
        int iAux = 0;
        foreach (GameObject cell in cells)
        {            
            editorStage.gridStage[iAux].type = cell.GetComponent<Cell>().info.type;
            editorStage.gridStage[iAux].isFinal = cell.GetComponent<Cell>().info.isFinal;
            editorStage.gridStage[iAux].isObjective = cell.GetComponent<Cell>().info.isObjective;
            editorStage.gridStage[iAux].isStart = cell.GetComponent<Cell>().info.isStart;
            editorStage.gridStage[iAux].water = cell.GetComponent<Cell>().info.water;
            editorStage.gridStage[iAux].food = cell.GetComponent<Cell>().info.food;
            editorStage.gridStage[iAux].troops = cell.GetComponent<Cell>().info.troops;
            editorStage.gridStage[iAux].weapons = cell.GetComponent<Cell>().info.weapons;
            editorStage.gridStage[iAux].gold = cell.GetComponent<Cell>().info.gold;
            iAux++;
        }
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
        GlobalInfo.editingCell = -1;
        DefaultCells cond = new DefaultCells();
        editorStage.version = cond.version;
    }
}
