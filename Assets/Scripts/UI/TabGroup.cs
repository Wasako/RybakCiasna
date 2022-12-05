using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{

    public List<HubTabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public HubTabButton selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(HubTabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<HubTabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(HubTabButton button)
    {
        ResetTab();
        if (selectedTab == null || button != selectedTab)
        {
            button.backgroud.sprite = tabHover;
        }
    }

    public void OnTabExit(HubTabButton button)
    {
        ResetTab();
    }

    public void OnTabSelected(HubTabButton button)
    {
        selectedTab = button;
        ResetTab();
        button.backgroud.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i=0; i<objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTab()
    {
        foreach(HubTabButton button in tabButtons)
        {
            if(selectedTab!=null && button == selectedTab) { continue; }
            button.backgroud.sprite = tabIdle;
        }
    }
}
