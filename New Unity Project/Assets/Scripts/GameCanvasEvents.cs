using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class GameCanvasEvents : MonoBehaviour
{

    public GameObject Hint;
    public GameObject HighscoreText;
    public GameObject ScoreText;
    public GameObject Trophy;

    public Color32 BronzeColor;
    public Color32 SilverColor;
    public Color32 GoldColor;

    public long goldTime = 60000;
    public long silverTime = 30000;
    public long bronzeTime = 15000;

    private float _tempFinalScore = 0f;

    Animator animator;
    long highscore;

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
        print("Higscore is " + highscore);
    }

    // Update is called once per frame
    void Update()
    {
        bool changed = _gameState == RunLeftManager.Instance.State ? false : true;
        switch (RunLeftManager.Instance.State)
        {

            case RunLeftManager.GameState.Waiting:
                Hint.SetActive(true);
                break;
            case RunLeftManager.GameState.Playing:
                if (changed)
                {
                    Hint.SetActive(false);
                    print("IS PLAYING NOW");
                }
                break;
            case RunLeftManager.GameState.Ended:
                if (changed)
                {
                    Hint.SetActive(false);
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
        SetHighScoreText(bestScoreString);
        print("GAME OVER and score is " + Score.ScoreInLong);
        if (highscore < Score.ScoreInLong)
        {
            HighscoreHandler.Instance.SaveHighscore(Score.ScoreInLong);
        }
        else
        {

        }
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
                finalScore += Time.smoothDeltaTime * 10f;

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
