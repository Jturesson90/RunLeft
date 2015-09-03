using UnityEngine;
using System.Collections;

public class GameOverZone : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other)
    {
        print("Other: " + other);
    }
  
}
