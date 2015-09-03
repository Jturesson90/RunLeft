using UnityEngine;
using System.Collections;

public class AchievementsHandler {
    private static AchievementsHandler _instance;
    public static AchievementsHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AchievementsHandler();
            }
            return _instance;
        }
    }


}
