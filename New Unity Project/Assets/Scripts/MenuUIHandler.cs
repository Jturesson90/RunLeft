using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject MuteButton;
    public Sprite MuteOffImage;
    public Sprite MuteOnImage;
    public LevelSwitcher levelSwitcher;
    // Use this for initialization
    void Start()
    {

        if (RunLeftPlayerPrefs.ShouldLogIn())
        {
            GoogleGameHandler.Instance.Instantiate();
            RunLeftPlayerPrefs.SetShouldLogIn(false);
        }
        if (RunLeftPlayerPrefs.IsMuted())
        {
            print("MUTE!");
            MuteSound();
        }

        if (!MusicManager.Instance.IsPlaying())
        {
            print("SPELAR!");
         //   MusicManager.Instance.Play();
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
    public void OnMutePressed()
    {
        print("OnMutePressed!");
        AudioListener.volume = AudioListener.volume == 1 ? 0 : 1;
        HandleMuteButtonImages(AudioListener.volume == 1 ? true : false);
    }

    private void HandleMuteButtonImages(bool soundIsOn)
    {
        if (soundIsOn)
        {
            MuteButton.GetComponent<Image>().sprite = MuteOffImage;
            RunLeftPlayerPrefs.SetMute(false);
        }
        else
        {
            RunLeftPlayerPrefs.SetMute(true);
            MuteButton.GetComponent<Image>().sprite = MuteOnImage;
        }
    }
    public void MuteSound()
    {
        print("MuteSound!");
        AudioListener.volume = 0;
        MuteButton.GetComponent<Image>().sprite = MuteOnImage;
    }
}
