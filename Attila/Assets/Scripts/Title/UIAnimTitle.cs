using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUIAnimator;

public class UIAnimTitle : MonoBehaviour
{
    public Canvas m_Canvas;
    public GAui title;

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

        title.PlayInAnims(eGUIMove.Self);        
        StartCoroutine(EnableAllDemoButtons());
    }

    IEnumerator EnableAllDemoButtons()
    {
        yield return new WaitForSeconds(0.2f);
        GSui.Instance.EnableGraphicRaycaster(m_Canvas, true);
    }
}
