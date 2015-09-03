using UnityEngine;
using System.Collections;
using System;

public class RunLeftManager
{

    private static RunLeftManager _instance;
    public enum GameState { Waiting, Playing, Ended };
    private GameState _state = GameState.Waiting;
    public static RunLeftManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RunLeftManager();
            }
            return _instance;
        }
    }
    public GameState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }

    public void CleanUp()
    {
        _instance = null;
    }
}
