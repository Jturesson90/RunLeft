using UnityEngine;
using System.Collections;

public class DroleGamesSplash : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void AnimationDone()
    {
        Application.LoadLevel(1);
    }
}
