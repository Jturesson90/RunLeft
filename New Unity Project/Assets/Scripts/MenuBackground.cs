using UnityEngine;
using System.Collections;

public class MenuBackground : MonoBehaviour {

    [Tooltip("Minus for left")]
    public float rotation = -1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, Time.deltaTime * rotation);
    }
}
