using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnAwake : MonoBehaviour {

	public Vector3 moveToPosition;
	
	protected void Awake() {
		transform.position = moveToPosition;
	}
}
