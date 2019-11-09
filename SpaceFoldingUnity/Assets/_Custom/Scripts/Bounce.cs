using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

	public Vector3 bounceOffset = Vector3.up;
	public float bounceTime = 1;

	Vector3 startPos;
	
	protected void Start() {
		startPos = transform.position;
	}
	
	protected void Update() {
		transform.position = startPos + bounceOffset 
			* Mathf.Abs(Mathf.Cos(Time.time/bounceTime * Mathf.PI));
	}
	
}
