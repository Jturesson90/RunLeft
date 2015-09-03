using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class Score : MonoBehaviour
{
   
    public static float ScoreInSeconds = 0f;
    public static long ScoreInLong = 0;
    public long PlayerScore = 0;
    Text scoreText;
    bool _isCounting = false;
    Stopwatch stopWatch;

    void Awake()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "0:00.00";
    }
    void Start()
    {
        stopWatch = new Stopwatch();

    }
    public static string ScoreText;
    // Update is called once per frame
    void Update()
    {

        switch (RunLeftManager.Instance.State) {
            case RunLeftManager.GameState.Waiting: break;
            case RunLeftManager.GameState.Playing:
                if (!stopWatch.IsRunning)
                {
                    StartCounting();
                }
                break;
            case RunLeftManager.GameState.Ended:
                if (stopWatch.IsRunning) {
                    StopCounting();
                } break;
        }
        
        
        SetText();
    }
    public void StartCounting()
    {
        stopWatch.Start();
        _isCounting = true;
    }
    public void StopCounting()
    {
        stopWatch.Stop();
        _isCounting = false;
    }

    void SetText()
    {
        ScoreInLong = stopWatch.ElapsedMilliseconds;
        ScoreInSeconds = stopWatch.ElapsedMilliseconds / 1000f;
        scoreText.text = FormatTime(stopWatch);
    }

    string FormatTime(Stopwatch sw) {
       
        TimeSpan ts = sw.Elapsed;
        string elapsedTime = String.Format("{0:0}:{1:00}.{2:00}", ts.Minutes, ts.Seconds,
           ts.Milliseconds / 10);
        ScoreText = elapsedTime;
        return elapsedTime;
    }
   
    public string GetFinalScore()
    {
        return FormatTime(stopWatch);
    }
   
}
