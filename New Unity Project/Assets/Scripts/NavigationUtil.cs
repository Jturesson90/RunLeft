using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public static class NavigationUtil
{
    private static PanelManager theMgr;

    public static PanelManager PanelMgr
    {
        get
        {
            if (theMgr == null)
            {
                theMgr = EventSystem.current.GetComponent<PanelManager>();
            }
            return theMgr;
        }
    }

    public static void ShowGameOverMenu()
    {

        PanelManager mgr = NavigationUtil.PanelMgr;
        if (mgr != null)
        {
            Debug.Log("Showing GameOver Menu!");
            mgr.OpenGameOverPanel();

        }
        else
        {
            Debug.LogWarning("PanelMgr script missing!");
        }
    }

    public static void ShowMenu()
    {
        if (Application.loadedLevelName.Equals("GameScene"))
        {
           
            GameObject.Find("LevelSwitcher").GetComponent<LevelSwitcher>().SwitchLevel("MenuScene",0.5f);
        }
        else
        {
            PanelManager mgr = NavigationUtil.PanelMgr;
            if (mgr != null)
            {
                Debug.Log("Showing GameOver Menu!");
                mgr.OpenMenu();

            }
            else
            {
                Debug.LogWarning("PanelMgr script missing!");
            }
        }

    }
}
