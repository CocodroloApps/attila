using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameCell : MonoBehaviour
{
    public int x;
    public int y;
    public int num;
    public bool moveable;
    public int[] moves = new int[8] ;

    // Start is called before the first frame update
    void Start()
    {
        ResetMovements();        
    }

    private void ResetMovements()
    {
        moves[0] = 0;
        moves[1] = 0;
        moves[2] = 0;
        moves[3] = 0;
        moves[4] = 0;
        moves[5] = 0;
        moves[6] = 0;
        moves[7] = 0;
    }

    public void CalculateMovements()
    {
        ResetMovements();
        if ((x + 1 < 8) && (y - 2 >= 0))
        {
            moves[0] = FindGameCell(x+1,y-2);
        }
        if ((x + 2 < 8) && (y + 1 < 8))
        {
            moves[1] = FindGameCell(x + 2, y + 1);
        }
        if ((x + 2 < 8) && (y - 1 >= 0))
        {
            moves[2] = FindGameCell(x + 2, y - 1);
        }
        if ((x + 1 < 8) && (y + 2 < 8))
        {
            moves[3] = FindGameCell(x + 1, y + 2);
        }
        if ((x - 1 >= 0) && (y + 2 < 8))
        {
            moves[4] = FindGameCell(x - 1, y + 2);
        }
        if ((x - 2 >= 0) && (y + 1 < 8))
        {
            moves[5] = FindGameCell(x - 2, y + 1);
        }
        if ((x - 2 >= 0) && (y - 1 >= 0))
        {
            moves[6] = FindGameCell(x - 2, y - 1);
        }
        if ((x - 1 >= 0) && (y - 2 >= 0))
        {
            moves[7] = FindGameCell(x - 1, y - 2);
        }
    }    

    private int FindGameCell(int xx, int yy)
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("GameCell");
        foreach (GameObject cell in cells)
        {
            if (cell.GetComponent<GameCell>().x == xx && cell.GetComponent<GameCell>().y == yy)
            {
                return cell.GetComponent<GameCell>().num;
            }
        }
        return 0;
    }

    public void SetMoveables()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("GameCell");
        foreach (GameObject cell in cells)
        {
            cell.GetComponent<GameCell>().moveable = false;
            int pos = Array.IndexOf(moves, cell.GetComponent<GameCell>().num);
            if (pos > -1)
            {
                cell.GetComponent<GameCell>().moveable = true;
            }
        }
    }

    public void ShowMoveables()
    {
        for (int i = 0; i < 8; i++)
        {
            if (moves[i]>0)
            {
                GameObject cell = GameObject.Find("Cell" + moves[i].ToString());
                GameObject selector = GeneralUtils.FindObject(cell, "Selector");
                selector.SetActive(true);
            }
        }
    }

    public void HideMoveables()
    {
        for (int i = 0; i < 8; i++)
        {
            if (moves[i] > 0)
            {
                GameObject cell = GameObject.Find("Cell" + moves[i].ToString());
                GameObject selector = GeneralUtils.FindObject(cell, "Selector");
                selector.SetActive(false);
            }
        }
    }
}
