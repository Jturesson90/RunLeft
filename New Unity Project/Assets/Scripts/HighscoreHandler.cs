﻿using UnityEngine;
using System.Collections;
using System;

public class HighscoreHandler
{
    private string SAVE_PATH;
    private static HighscoreHandler _instance;

    public static HighscoreHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new HighscoreHandler();
            }
            return _instance;
        }
    }

    private HighscoreHandler()
    {
        SAVE_PATH = Application.persistentDataPath + "/data_highscore.dat";
        Highscores highscores = EasySerializer.DeserializeObjectFromFile(SAVE_PATH) as Highscores;
        if (highscores != null)
        {
        }
        else
        {
            SaveNew();
        }
    }
    private void SaveNew()
    {
        Highscores highscores = new Highscores();
        highscores.BestNotUploadedScore = 0;
        highscores.Highscore = 0;
        EasySerializer.SerializeObjectToFile(highscores, SAVE_PATH);
    }

    public void SaveHighscore(long score)
    {
        Debug.Log("SAVING new score"+score);
        Highscores highscores = EasySerializer.DeserializeObjectFromFile(SAVE_PATH) as Highscores;
        if (highscores == null)
        {
            SaveNew();
        }
        if (highscores.Highscore < score)
        {
            highscores.Highscore = score;
            EasySerializer.SerializeObjectToFile(highscores, SAVE_PATH);
        }
    }

    internal long GetUnpublishedScore()
    {
        throw new NotImplementedException();
    }

    internal void SaveUnpublishedScore(long score)
    {
        throw new NotImplementedException();
    }

    internal void SaveUnpublishedScore()
    {
        throw new NotImplementedException();
    }

    public Highscores GetHighscores()
    {
        Highscores highscores = EasySerializer.DeserializeObjectFromFile(SAVE_PATH) as Highscores;
        if (highscores == null)
        {
            SaveNew();
        }
        return highscores;
    }

}
[System.Serializable]
public class Highscores
{
    public long BestNotUploadedScore { get; set; }
    public long Highscore { get; set; }

}