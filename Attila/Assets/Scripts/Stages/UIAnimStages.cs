using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUIAnimator;

public class UIAnimStages : MonoBehaviour
{
    public Canvas m_Canvas;
    public GAui hun1;
    public GAui hun2;
    public GAui hun3;
    public GAui hun4;
    public GAui hun5;
    public GAui hun6;
    public GAui hun7;
    public GAui hun8;
    public GAui hun9;
    public GAui hun10;
    public GAui ToMenu;

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
        yield return new WaitForSeconds(0.3f);

        // Play In-Animations of m_Title1
        hun1.PlayInAnims(eGUIMove.Self);
        hun2.PlayInAnims(eGUIMove.Self);
        hun3.PlayInAnims(eGUIMove.Self);
        hun4.PlayInAnims(eGUIMove.Self);
        hun5.PlayInAnims(eGUIMove.Self);
        hun6.PlayInAnims(eGUIMove.Self);
        hun7.PlayInAnims(eGUIMove.Self);
        hun8.PlayInAnims(eGUIMove.Self);
        hun9.PlayInAnims(eGUIMove.Self);
        hun10.PlayInAnims(eGUIMove.Self);
        ToMenu.PlayInAnims(eGUIMove.Self);

        // Play In-Animations of all primary buttons
        StartCoroutine(EnableAllDemoButtons());
    }

    IEnumerator EnableAllDemoButtons()
    {
        yield return new WaitForSeconds(0.2f);
        GSui.Instance.EnableGraphicRaycaster(m_Canvas, true);
    }

    public void HideAllGUIs()
    {
        hun1.PlayOutAnims();
        hun2.PlayOutAnims();
        hun3.PlayOutAnims();
        hun4.PlayOutAnims();
        hun5.PlayOutAnims();
        hun6.PlayOutAnims();
        hun7.PlayOutAnims();
        hun8.PlayOutAnims();
        hun9.PlayOutAnims();
        hun10.PlayOutAnims();
    }
}

