using UnityEngine;
using System.Collections;
using System;

public class PanelManager : MonoBehaviour
{

	public GameObject gameOverPanel;
	
	private GameObject currentPanel;
	private GameObject prevSelected;
	// Use this for initialization


	public void OpenGameOverPanel ()
	{
        OpenPanel(gameOverPanel);

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
        currentPanel.SetActive(true);
		

		
	}
	private void ClosePanel (GameObject panel)
	{
        panel.SetActive(false);
		prevSelected = panel;
	}

    internal void OpenMenu()
    {
        throw new NotImplementedException();
    }
}
