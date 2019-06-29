using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUIAnimator;

public class UIAnimAttila : MonoBehaviour
{
    public Canvas m_Canvas;
    public GAui hun;
    public GAui roman;
    public GAui ribbon;
    public GAui ToStage;
    public GAui warPoints1;
    public GAui warPoints2;
    public GAui war2Points1;
    public GAui war2Points2;
    public GAui troops;
    public GAui weapons;
    public GAui water;
    public GAui food;
    public GAui gold;

    void Awake()
    {
        if (enabled)
        {
            // Disable auto-animation and let this script controls all GAui elements in the scene.
            GSui.Instance.m_AutoAnimation = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GSui.Instance.EnableGraphicRaycaster(m_Canvas, false);
        StartCoroutine(MoveInGameObjects());
    }

    IEnumerator MoveInGameObjects()
    {
        yield return new WaitForSeconds(0.8f);

        hun.PlayInAnims(eGUIMove.Self);
        roman.PlayInAnims(eGUIMove.Self);
        ribbon.PlayInAnims(eGUIMove.Self);
        StartCoroutine(EnableInPrimaryButtonsAnim());
    }

    IEnumerator EnableInPrimaryButtonsAnim()
    {
        yield return new WaitForSeconds(0.5f);

        // Play In-Animations of all primary buttons
        ToStage.PlayInAnims(eGUIMove.Self);
       
        // Enable all scene switch buttons
        StartCoroutine(EnableAllDemoButtons());
    }

    IEnumerator EnableAllDemoButtons()
    {
        yield return new WaitForSeconds(0.2f);
        GSui.Instance.EnableGraphicRaycaster(m_Canvas, true);
    }

    public void HideAllGUIs()
    {
        ribbon.PlayOutAnims();
        hun.PlayOutAnims();
        roman.PlayOutAnims();
    }

    public void ShowBattleLost()
    {
        warPoints1.PlayInAnims(eGUIMove.Self);
        warPoints2.PlayInAnims(eGUIMove.Self);
        StartCoroutine(ShowPoints());
    }

    public void ShowBattleLost2()
    {
        war2Points1.PlayInAnims(eGUIMove.Self);
        war2Points2.PlayInAnims(eGUIMove.Self);
        StartCoroutine(ShowPoints2());
    }

    IEnumerator ShowPoints()
    {
        yield return new WaitForSeconds(0.4f);
        warPoints1.PlayOutAnims();
        warPoints2.PlayOutAnims();
    }

    IEnumerator ShowPoints2()
    {
        yield return new WaitForSeconds(0.4f);
        war2Points1.PlayOutAnims();
        war2Points2.PlayOutAnims();
    }

    public void ShowArmyEffect()
    {
        troops.PlayInAnims(eGUIMove.Self);
        weapons.PlayInAnims(eGUIMove.Self);
        water.PlayInAnims(eGUIMove.Self);
        food.PlayInAnims(eGUIMove.Self);
        gold.PlayInAnims(eGUIMove.Self);        
    }

}
