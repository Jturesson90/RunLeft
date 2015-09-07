using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class GameCanvasEvents : MonoBehaviour
{
    [Header("Public Gameobjects")]
    public GameObject Hint;
    public GameObject HighscoreText;
    public GameObject DuringGameScore;

    public GameObject Trophy;
    public GameObject NewHighscore;

    [Header("Trophy Colors")]
    public Color32 BronzeColor;
    public Color32 SilverColor;
    public Color32 GoldColor;

    [Header("Trophy timers in Milliseconds")]
    public long goldTime = 60000;
    public long silverTime = 30000;
    public long bronzeTime = 15000;

    [Header("Final score")]
    public float finalScoreSpeedCount = 20f;
    public GameObject ScoreText;

    private float _tempFinalScore = 0f;

    Animator animator;
    long highscore;

    private Vector3 scorePanelStartPos;
    public Vector3 scorePanelOutSidePos;


    private RunLeftManager.GameState _gameState = RunLeftManager.Instance.State;
    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {


        Highscores highscores = HighscoreHandler.Instance.GetHighscores();
        highscore = highscores.Highscore;
        SetupHint();
        SetupScorePanel();
    }
    void SetupScorePanel()
    {

        RectTransform rt = DuringGameScore.GetComponent<RectTransform>();
        scorePanelStartPos = rt.anchoredPosition;
        rt.anchoredPosition = new Vector3(0f, 900f, 0);
        LeanTween.move(rt, scorePanelStartPos, 0.5f).setEase(LeanTweenType.easeOutBack);

    }

    void SetupHint()
    {
        RectTransform rt = Hint.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(-900f, rt.anchoredPosition.y, 0);
        LeanTween.move(rt, new Vector3(0, rt.anchoredPosition.y, 0f), 0.5f).setEase(LeanTweenType.easeOutBack);
    }

    void TweenOut(GameObject go, Vector3 outSidePos)
    {
        RectTransform rt = go.GetComponent<RectTransform>();
        LeanTween.move(rt, outSidePos, 0.5f).setEase(LeanTweenType.easeInBack);
    }

    // Update is called once per frame
    void Update()
    {
        bool changed = _gameState == RunLeftManager.Instance.State ? false : true;
        switch (RunLeftManager.Instance.State)
        {

            case RunLeftManager.GameState.Waiting:

                break;
            case RunLeftManager.GameState.Playing:
                if (changed)
                {
                    TweenOut(Hint, new Vector3(900f, Hint.GetComponent<RectTransform>().anchoredPosition.y, 0f));
                    //Hint.SetActive(false);
                    print("IS PLAYING NOW");
                }
                break;
            case RunLeftManager.GameState.Ended:
                if (changed)
                {
                    TweenOut(DuringGameScore, scorePanelOutSidePos);
                    OnGameOver();
                }
                break;
        }

        _gameState = RunLeftManager.Instance.State;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapePressed();
        }

        HandleFinalScore();
    }
    void OnEscapePressed()
    {
        switch (RunLeftManager.Instance.State)
        {

            case RunLeftManager.GameState.Waiting:
                GameObject.Find("LevelSwitcher").GetComponent<LevelSwitcher>().SwitchLevel("MenuScene");
                break;
            case RunLeftManager.GameState.Playing:
                break;
            case RunLeftManager.GameState.Ended:
                GameObject.Find("LevelSwitcher").GetComponent<LevelSwitcher>().SwitchLevel("MenuScene");
                break;
        }
    }
    public void OnPlayButtonPressed()
    {
        //RunLeftManager.Instance.CleanUp();
        GameObject.Find("LevelSwitcher").GetComponent<LevelSwitcher>().SwitchLevel(Application.loadedLevelName);
        //   Application.LoadLevel(Application.loadedLevel);
    }
    public void OnLeaderBoardButtonPressed()
    {
        GoogleGameHandler.Instance.ShowLeaderboard();
    }
    public void OnBackButtonPressed()
    {
        NavigationUtil.ShowMenu();
    }

    public void OnGameOver()
    {
        Flash();
        _tempFinalScore = Score.ScoreInSeconds;
        GoogleGameHandler.Instance.SendToHighscore(Score.ScoreInLong);
        long bestScore = Math.Max(highscore, Score.ScoreInLong);
        string bestScoreString = bestScore.ToFormattedTimeString();

        bool newHighscore = false;
        SetHighScoreText(bestScoreString);
        if (highscore < Score.ScoreInLong)
        {
            newHighscore = true;
            HighscoreHandler.Instance.SaveHighscore(Score.ScoreInLong);
        }
        NewHighscore.SetActive(newHighscore);
        SetTrophyColor();

    }

    private void Flash()
    {
        animator.SetTrigger("flash");
    }


    private float finalScore = 0f;
    private void HandleFinalScore()
    {
        if (_tempFinalScore > 0f)
        {
            if (finalScore < _tempFinalScore)
            {
                finalScore += Time.smoothDeltaTime * _tempFinalScore / finalScoreSpeedCount;

                finalScore = Mathf.Min(finalScore, _tempFinalScore);
                SetScoreText(finalScore.ToFormattedTimeString());
            }
        }
    }

    void SetScoreText(string str)
    {
        ScoreText.GetComponent<Text>().text = str;
    }
    private void SetHighScoreText(string str)
    {
        HighscoreText.GetComponent<Text>().text = str;
    }

    void SetTrophyColor()
    {

        GoogleGameHandler.Instance.IncreaseCompletedRuns();
        if (Score.ScoreInLong > goldTime)
        {
            GoogleGameHandler.Instance.AchievementGold();
            Trophy.GetComponent<Image>().color = GoldColor;
        }
        else if (Score.ScoreInLong > silverTime)
        {
            GoogleGameHandler.Instance.AchievementSilver();
            Trophy.GetComponent<Image>().color = SilverColor;
        }
        else if (Score.ScoreInLong > bronzeTime)
        {
            GoogleGameHandler.Instance.AchievementBronze();
            Trophy.GetComponent<Image>().color = BronzeColor;
        }
        else
        {

        }
    }


}
