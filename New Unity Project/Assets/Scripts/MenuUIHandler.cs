using UnityEngine;
using System.Collections;

public class MenuUIHandler : MonoBehaviour
{

    public LevelSwitcher levelSwitcher;
    // Use this for initialization
    void Start()
    {
        if (RunLeftPlayerPrefs.ShouldLogIn())
        {
            GoogleGameHandler.Instance.Instantiate();
            RunLeftPlayerPrefs.SetShouldLogIn(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnPlayPressed()
    {
        levelSwitcher.SwitchLevel("GameScene", 0.5f);
        //Application.LoadLevel("GameScene");
    }

    public void OnLeaderboardPressed()
    {
        GoogleGameHandler.Instance.ShowLeaderboard();
    }
    public void OnAchievementPressed()
    {
        GoogleGameHandler.Instance.ShowAchievements();
    }
}
