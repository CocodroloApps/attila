using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    public Text troops;
    public Text weapons;
    public Text water;
    public Text food;
    public Text gold;
    public Text gems;

    public GameObject boxSprite;

    public void ChangeSprite(Sprite newS)
    {
        boxSprite.GetComponent<Image>().sprite = newS;
    }

    public void ShowMoveResult(int cellNum)
    {
        string sign = "";
        if (GlobalInfo.gridStage[cellNum].troops > 0) { sign = "+ "; } else { sign = "- "; }
        if (GlobalInfo.gridStage[cellNum].troops == 0) { sign = "0"; }
        troops.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].troops).ToString("#,#");
        if (GlobalInfo.gridStage[cellNum].weapons > 0) { sign = "+ "; } else { sign = "- "; }
        if (GlobalInfo.gridStage[cellNum].weapons == 0) { sign = "0"; }
        weapons.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].weapons).ToString("#,#");
        if (GlobalInfo.gridStage[cellNum].water > 0) { sign = "+ "; } else { sign = "- "; }
        if (GlobalInfo.gridStage[cellNum].water == 0) { sign = "0"; }
        water.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].water).ToString("#,#");
        if (GlobalInfo.gridStage[cellNum].food > 0) { sign = "+ "; } else { sign = "- "; }
        if (GlobalInfo.gridStage[cellNum].food == 0) { sign = "0"; }
        food.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].food).ToString("#,#");
        if (GlobalInfo.gridStage[cellNum].gold > 0) { sign = "+ "; } else { sign = "- "; }
        if (GlobalInfo.gridStage[cellNum].gold == 0) { sign = "0"; }
        gold.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].gold).ToString("#,#");
        gems.text = "+ 10";
        if (GlobalInfo.gridStage[cellNum].isObjective == true) 
        {
            gems.text = "+ 100";
        }
        if (GlobalInfo.gridStage[cellNum].isFinal == true)
        {
            int iAux = 1000;
            gems.text = "+" + iAux.ToString("#,#");
        }
    }
}
