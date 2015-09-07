using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DroleGamesSplash : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        RunLeftPlayerPrefs.SetShouldLogIn(true);
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AnimationDone()
    {
        Application.LoadLevel(1);
    }
}
