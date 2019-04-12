using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorClickManager : MonoBehaviour {

    public Sprite edit;
    public Sprite noEdit;
    public Sprite paint;
    public Sprite noPaint;
    public GameObject editButton;
    public GameObject editTile;
    public GameObject paintButton;

    public Sprite tundra;
    public Sprite desert;
    public Sprite wood;
    public Sprite town;
    public Sprite city;
    public Sprite army;
    public Sprite crop;
    public Sprite lake;
    public Sprite river;
    public Sprite mine;
    public Sprite objective;
    public Sprite mountain;
    public Sprite empty;

    private bool paintFistTime;

    public void Start()
    {
        GlobalInfo.isEditing = false;
        GlobalInfo.isPainting = false;
        paintFistTime = true;
    }

    public void onClickEditor()
    {
        GlobalInfo.isEditing = !(bool)GlobalInfo.isEditing;
        if (GlobalInfo.isEditing == true)
        {
            GlobalInfo.isPainting = false;
            paintButton.GetComponent<Image>().sprite = noPaint;

            editButton.GetComponent<Image>().sprite = edit;
            if (paintFistTime)
            {
                GlobalInfo.paintCellType = 0;                
            }
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
            editTile.SetActive(true);
        } else
        {
            editButton.GetComponent<Image>().sprite = noEdit;
            editTile.SetActive(false);
        }
    }

    public void onClickPainter()
    {
        GlobalInfo.isPainting = !(bool)GlobalInfo.isPainting;
        if (GlobalInfo.isPainting == true)
        {
            GlobalInfo.isEditing = false;
            editButton.GetComponent<Image>().sprite = noEdit;

            paintButton.GetComponent<Image>().sprite = paint;
            if (paintFistTime)
            {
                GlobalInfo.paintCellType = 0;
            }
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
            editTile.SetActive(true);
        }
        else
        {
            paintButton.GetComponent<Image>().sprite = noPaint;
            editTile.SetActive(false);
        }
    }


    public void onClickTundra()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 1;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }        
    }

    public void onClickDesert()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 2;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickWood()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 3;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickTown()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 4;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickCity()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 5;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickArmy()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 6;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickCrop()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 7;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickLake()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 8;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickRiver()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 9;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickMine()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 10;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickObjective()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 11;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickMountain()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 12;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public void onClickEmpty()
    {
        if (GlobalInfo.isEditing || GlobalInfo.isPainting)
        {
            GlobalInfo.paintCellType = 0;
            editTile.GetComponent<Image>().sprite = TypeSprite(GlobalInfo.paintCellType);
        }
    }

    public Sprite TypeSprite(int num)
    {
        if (num == 1) { return tundra; }
        if (num == 2) { return desert; }
        if (num == 3) { return wood; }
        if (num == 4) { return town; }
        if (num == 5) { return city; }
        if (num == 6) { return army; }
        if (num == 7) { return crop; }
        if (num == 8) { return lake; }
        if (num == 9) { return river; }
        if (num == 10) { return mine; }
        if (num == 11) { return objective; }
        if (num == 12) { return mountain; }
        return empty;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "EditorCell" && GlobalInfo.isModifying == false)
                {
                    GameObject.Find(hit.collider.gameObject.name).GetComponent<Cell>().ImClicked();                    
                }               
            }
        }
    }
}
