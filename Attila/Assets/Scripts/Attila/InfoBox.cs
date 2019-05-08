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

    public void ShowMoveResult(int cellNum)
    {
        string sign = "";
        if (GlobalInfo.gridStage[cellNum].troops >= 0) { sign = "+ "; } else { sign = "- "; }
        troops.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].troops).ToString();
        if (GlobalInfo.gridStage[cellNum].weapons >= 0) { sign = "+ "; } else { sign = "- "; }
        weapons.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].weapons).ToString();
        if (GlobalInfo.gridStage[cellNum].water >= 0) { sign = "+ "; } else { sign = "- "; }
        water.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].water).ToString();
        if (GlobalInfo.gridStage[cellNum].food >= 0) { sign = "+ "; } else { sign = "- "; }
        food.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].food).ToString();
        if (GlobalInfo.gridStage[cellNum].gold >= 0) { sign = "+ "; } else { sign = "- "; }
        gold.text = sign + Mathf.Abs(GlobalInfo.gridStage[cellNum].gold).ToString();
        gems.text = "+ 10";
        if (GlobalInfo.gridStage[cellNum].isObjective == true) 
        {
            gems.text = "+ 100";
        }
        if (GlobalInfo.gridStage[cellNum].isFinal == true)
        {
            gems.text = "+ 1000";
        }
    }
}
