using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class GameCanvasEvents : MonoBehaviour
{
 
    public GameObject HighscoreText;
    public GameObject ScoreText;
    public GameObject Trophy;

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

            case RunLeftManager.GameState.Waiting: break;
            case RunLeftManager.GameState.Playing:
                if (changed)
                {
                    print("IS PLAYING NOW");
                }
                break;
            case RunLeftManager.GameState.Ended:
                if (changed)
                {
                    OnGameOver();
                }
                break;
        }
        _gameState = RunLeftManager.Instance.State;
    }

    public void OnPlayButtonPressed()
    {
        RunLeftManager.Instance.CleanUp();
        Application.LoadLevel(Application.loadedLevel);
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
        ScoreText.GetComponent<Text>().text = Score.ScoreText;
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


    }

    private void Flash()
    {
        print("FLASH!");
        animator.SetTrigger("flash");
    }

    private void SetHighScoreText(string str) {
        HighscoreText.GetComponent<Text>().text = str;
    }

}
