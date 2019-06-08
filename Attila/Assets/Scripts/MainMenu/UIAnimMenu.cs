using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUIAnimator;

public class UIAnimMenu : MonoBehaviour
{
    public Canvas m_Canvas;
    public GAui m_Title1;
    public GAui m_TopLeft_A;
    public GAui m_Right;
    public GAui m_Right_A;
    public GAui m_Right_B;
    public GAui m_Right_C;
    public GAui m_Bottom;

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
        // Disable all scene switch buttons
        GSui.Instance.EnableGraphicRaycaster(m_Canvas, false);
        StartCoroutine(MoveInTitleGameObjects());
    }

    IEnumerator MoveInTitleGameObjects()
    {
        yield return new WaitForSeconds(0.3f);

        // Play In-Animations of m_Title1
        m_Title1.PlayInAnims(eGUIMove.Self);

        // Play In-Animations of all primary buttons
        StartCoroutine(MoveInPrimaryButtons());
    }

    IEnumerator MoveInPrimaryButtons()
    {
        yield return new WaitForSeconds(0.5f);

        // Play In-Animations of all primary buttons
        m_TopLeft_A.PlayInAnims(eGUIMove.Self);
        m_Right.PlayInAnims(eGUIMove.Self);
        m_Bottom.PlayInAnims(eGUIMove.Self);
        
        // Enable all scene switch buttons
        StartCoroutine(EnableInPrimaryButtonsAnim());
    }

    IEnumerator EnableInPrimaryButtonsAnim()
    {
        yield return new WaitForSeconds(0.5f);

        // Play In-Animations of all primary buttons
        m_Right_A.PlayInAnims(eGUIMove.Self);
        m_Right_B.PlayInAnims(eGUIMove.Self);
        m_Right_C.PlayInAnims(eGUIMove.Self);

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
        GlobalInfo.isShowingInfo = true;
        m_TopLeft_A.PlayOutAnims();
        m_Right.PlayOutAnims();

        // Play Out-Animations of m_Title1 and m_Title2
        StartCoroutine(HideTitleTextMeshes());
    }

    IEnumerator HideTitleTextMeshes()
    {
        yield return new WaitForSeconds(0.6f);

        yield return new WaitForSeconds(0.7f);
        GlobalInfo.isShowingInfo = false;
    }
}
