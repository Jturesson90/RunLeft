using UnityEngine;
using System.Collections;

public class PandaHandler : MonoBehaviour
{
	public bool turnLeft = false;
	public GameObject rotationObject;
	public float movementSpeed = 1f;
	public float angle = 90f;
	private float tempMovementSpeed = 0f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			turnLeft = true;
		} else {
			turnLeft = false;
		}

	}
	void FixedUpdate ()
	{
		tempMovementSpeed = turnLeft ? movementSpeed : 0f;
		
		transform.Translate (Vector3.up * movementSpeed * Time.deltaTime);
		transform.Rotate (Vector3.forward, Time.deltaTime * angle * tempMovementSpeed);
	}
}
