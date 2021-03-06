﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Grid
    public int rows;
    public int cols;    
    public Vector2 gridOffset;
    public GameObject cell;

    private BoxCollider2D gridSize;
    private Vector2 originalCellSize;

    private Vector2 cellScale;
    private Vector2 cellSize;
    private Vector2 offset;

    public void Clean()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void PaintCells()
    {
        gridSize = GetComponent<BoxCollider2D>();
        originalCellSize = cell.GetComponent<SpriteRenderer>().sprite.bounds.size;

        cellSize = new Vector2(gridSize.size.x / (float)cols, gridSize.size.y / (float)rows);

        cellScale.x = cellSize.x / (originalCellSize.x + gridOffset.x);
        cellScale.y = cellSize.y / (originalCellSize.y + gridOffset.y);
        
        cell.transform.localScale = new Vector2(cellScale.x, cellScale.y);

        offset.x = gridSize.offset.x - ((gridSize.size.x) / 2 ) + cellSize.x / 2;
        offset.y = gridSize.offset.y - ((gridSize.size.y) / 2 ) + cellSize.y / 2;

        DefaultCells cond = new DefaultCells();
        int sufixName = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                sufixName++;
                //add the cell size so that no two cells will have the same x and y position
                Vector2 pos = new Vector2(col * cellSize.x + transform.position.x + offset.x, row * cellSize.y + transform.position.y + offset.y);

                //Add Cell information
                cell.GetComponent<Cell>().info.x = col;
                cell.GetComponent<Cell>().info.y = row;
                cell.GetComponent<Cell>().info.listPos = sufixName-1;
                cell.GetComponent<Cell>().info.type = cond.type;
                cell.GetComponent<Cell>().info.isFinal = false;
                cell.GetComponent<Cell>().info.isObjective = false;
                cell.GetComponent<Cell>().info.water = cond.water;
                cell.GetComponent<Cell>().info.food = cond.food;
                cell.GetComponent<Cell>().info.troops = cond.troops;
                cell.GetComponent<Cell>().info.weapons = cond.weapons;
                cell.GetComponent<Cell>().info.gold = cond.gold;
                cell.name = "Cell" + sufixName.ToString();

                //instantiate the game object, at position pos, with rotation set to identity
                GameObject cO = Instantiate(cell, pos, Quaternion.identity) as GameObject;

                //set the parent of the cell to GRID so you can move the cells together with the grid;
                cO.transform.parent = transform;
            }
        }        
    }
}
