using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject MuteButton;
    public Sprite MuteOffImage;
    public Sprite MuteOnImage;
    public LevelSwitcher levelSwitcher;

    public GameObject AchievementButton;
    public GameObject LeaderboarButton;
    public GameObject PlayButton;

    [Header("Game Center Specific")]
    public GameObject GameCenterButton;
    public GameObject GameCenterPlayButton;

    // Use this for initialization
    void Start()
    {
		Application.targetFrameRate = 60;

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

#if UNITY_IOS
    AchievementButton.SetActive(false);
        LeaderboarButton.SetActive(false);
        GameCenterButton.SetActive(true);
        GameCenterPlayButton.SetActive(true);
        PlayButton.SetActive(false);
#elif UNITY_ANDROID 
        GameCenterButton.SetActive(false);
        AchievementButton.SetActive(true);
        LeaderboarButton.SetActive(true);
        GameCenterPlayButton.SetActive(false);
        PlayButton.SetActive(true);
#endif


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
