using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public GameObject selectImage;
    public GameObject finalImage;
    public GameObject objectiveImage;
    public GameObject startImage;
    public StageCell info;

    private GameObject cellIcon;
    private Text type;
    private Text x;
    private Text y;
    private Text troops;
    private Text weapons;
    private Text water;
    private Text food;
    private Text gold;

    public void Start()
    {
        cellIcon = GameObject.Find("Tile");
        type = GameObject.Find("Type").GetComponent<Text>();
        x = GameObject.Find("XText").GetComponent<Text>();
        y = GameObject.Find("YText").GetComponent<Text>();
        troops = GameObject.Find("TroopsText").GetComponent<Text>();
        weapons = GameObject.Find("WeaponsText").GetComponent<Text>();
        water = GameObject.Find("WaterText").GetComponent<Text>();
        food = GameObject.Find("FoodText").GetComponent<Text>();
        gold = GameObject.Find("GoldText").GetComponent<Text>();
    }

    public void ImClicked()
    {
        if (GlobalInfo.isEditing)
        {            
            SetInfo(GlobalInfo.paintCellType);
        }
        if (GlobalInfo.isPainting)
        {            
            PaintAll(GlobalInfo.paintCellType);
        }
        GlobalInfo.editingCell = info.listPos;
        ShowInfo();
    }

    public void ShowCell()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("EditorManager").GetComponent<EditorClickManager>().TypeSprite(info.type);
        finalImage.SetActive(info.isFinal);
        objectiveImage.SetActive(info.isObjective);
        startImage.SetActive(info.isStart);
    }

    public void ShowInfo()
    {
        cellIcon.GetComponent<Image>().sprite = GameObject.Find("EditorManager").GetComponent<EditorClickManager>().TypeSprite(info.type);        
        type.text = TypeName(info.type);
        x.text = info.x.ToString();
        y.text = info.y.ToString();
        troops.text = info.troops.ToString("#,#");
        weapons.text = info.weapons.ToString("#,#");
        water.text = info.water.ToString("#,#");
        food.text = info.food.ToString("#,#");
        gold.text = info.gold.ToString("#,#");

        finalImage.SetActive(info.isFinal);              
        objectiveImage.SetActive(info.isObjective);        
        startImage.SetActive(info.isStart);

        ShowBorder();
    }

    private void PaintAll(int num)
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("EditorCell");
        foreach (GameObject cell in cells)
        {
            cell.GetComponent<Cell>().SetInfo(GlobalInfo.paintCellType);            
        }
    }

    public void SetInfo(int num)
    {
        DefaultCells cond = new DefaultCells();
        cond.SetValues(num);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("EditorManager").GetComponent<EditorClickManager>().TypeSprite(num);
        info.type = num;
        info.water = cond.water;
        info.food = cond.food;
        info.troops = cond.troops;
        info.weapons = cond.weapons;
        info.gold = cond.gold;
        info.isFinal = cond.isFinal;
        info.isObjective = cond.isObjective;
        info.isStart = cond.isStart;

        finalImage.SetActive(info.isFinal);
        objectiveImage.SetActive(info.isObjective);
        startImage.SetActive(info.isStart);
    }

    private string TypeName(int num)
    {
        if (num == 1) { return "Tundra"; }
        if (num == 2) { return "Desert"; }
        if (num == 3) { return "Wood"; }
        if (num == 4) { return "Town"; }
        if (num == 5) { return "City"; }
        if (num == 6) { return "Army"; }
        if (num == 7) { return "Crop"; }
        if (num == 8) { return "Lake"; }
        if (num == 9) { return "River"; }
        if (num == 10) { return "Mine"; }
        if (num == 11) { return "Objective"; }
        if (num == 12) { return "Mountain"; }
        return "Empty";
    }

    public void ShowBorder()
    {
        selectImage.SetActive(true);
        Invoke("HideBorder", 1f);
    }

    public void ShowStaticBorder()
    {
        selectImage.SetActive(true);
    }

    public void HideBorder()
    {
        selectImage.SetActive(false);
    }
}
