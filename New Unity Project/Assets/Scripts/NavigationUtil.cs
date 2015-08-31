using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public static class NavigationUtil
{
	private static PanelManager theMgr;

	public static PanelManager PanelMgr {
		get {
			if (theMgr == null) {
				theMgr = EventSystem.current.GetComponent<PanelManager> ();
			}
			return theMgr;
		}
	}

	public static void ShowGameOverMenu ()
	{

		PanelManager mgr = NavigationUtil.PanelMgr;
		if (mgr != null) {
			Debug.Log ("Showing GameOver Menu!");
			mgr.OpenGameOverPanel ();
			
		} else {
			Debug.LogWarning ("PanelMgr script missing!");
		}
	}
	
}
