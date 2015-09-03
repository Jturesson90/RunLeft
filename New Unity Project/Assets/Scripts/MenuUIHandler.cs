using UnityEngine;
using System.Collections;

public class MenuUIHandler : MonoBehaviour {

    public LevelSwitcher levelSwitcher;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlayPressed() {
        levelSwitcher.SwitchLevel("GameScene",0.5f);
        //Application.LoadLevel("GameScene");
    }

    public void OnLeaderboardPressed()
    {

    }
}
