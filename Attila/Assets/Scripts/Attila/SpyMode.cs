using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpyMode : MonoBehaviour
{

    public Text spyMoves;
    public GameObject infoBox;

    public void ActivateSpyMode()
    {
        if (GlobalInfo.spyMoves > 0 && GlobalInfo.spyMode == false)
        {
            GlobalInfo.spyMoves--;
            GlobalInfo.spyMode = true;
            ShowSpyMoves();
            GameObject.Find("GameManager").GetComponent<AudioAttila>().SpyEffect1();
        }
    }

    public void DisableSpyMode()
    {                
        GlobalInfo.spyMode = false;        
    }

    public void SpyCell(string dest)
    {
        GameObject cell = GameObject.Find(dest);
        if (cell != null)
        {
            if ("Cell" + GlobalInfo.playerPos.ToString() != dest)
            {
                GameObject.Find("GameManager").GetComponent<AudioAttila>().SpyEffect2();
                GlobalInfo.isShowingInfo = true;
                infoBox.SetActive(true);                
                GameObject.Find("InfoBox").GetComponent<InfoBox>().ChangeSprite(GameObject.Find("GameManager").GetComponent<GameManager>().TypeSprite(GlobalInfo.gridStage[cell.GetComponent<GameCell>().num - 1].type));
                GameObject.Find("InfoBox").GetComponent<InfoBox>().ShowMoveResult(cell.GetComponent<GameCell>().num -1);
            } else
            {
                GlobalInfo.spyMoves++;
                GlobalInfo.spyMode = false;
                GameObject.Find("GameManager").GetComponent<AudioAttila>().SpyEffect1();
                ShowSpyMoves();
            }
        }
    }

    public void ShowSpyMoves()
    {
        if (GlobalInfo.spyMoves == 0)
        {
            spyMoves.text = "-";
        }
        else
        {
            spyMoves.text = GlobalInfo.spyMoves.ToString("#,#");
        }        
    }

}
