using UnityEngine;
using System.Collections;

public class RunLeftPlayerPrefs : MonoBehaviour
{
    const string LOG_IN_KEY = "login";
    // Use this for initialization
    public static bool ShouldLogIn()
    {
        return PlayerPrefs.GetInt(LOG_IN_KEY, 0) == 1 ? true : false;
    }
    public static void SetShouldLogIn(bool login)
    {
        
        if (login)
        {
            PlayerPrefs.SetInt(LOG_IN_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(LOG_IN_KEY, 0);
        }
    }
}
