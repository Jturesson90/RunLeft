using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour
{

	public GameObject gameOverPanel;
	
	private GameObject currentPanel;
	private GameObject prevSelected;
	// Use this for initialization


	public void OpenGameOverPanel ()
	{


	}

	private void OpenPanel (GameObject panel)
	{
		if (currentPanel == panel) {
			return;
		}

		if (currentPanel != null) {
			ClosePanel (currentPanel);
		}
		currentPanel = panel;

		
	}
	private void ClosePanel (GameObject panel)
	{
		prevSelected = panel;
	}

}
