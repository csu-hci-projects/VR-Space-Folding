using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StepTrigger : MonoBehaviour {

	
	public UnityEvent onSteppedIn;
	

	bool wasIn = false;
	
	BoxCollider bounds;
	
	protected void Awake() {
		bounds = GetComponent<BoxCollider>();
	}
	
	void Update () {
		Ray ray = new Ray(Camera.main.transform.position, Vector3.down);
		RaycastHit hit;
		bool isIn = bounds.Raycast(ray, out hit, 10f);
		if (isIn != wasIn) {
			if (isIn) {
				Debug.Log("Stepped in " + gameObject.name, gameObject);
				onSteppedIn.Invoke();
			}
			wasIn = isIn;
		}
	}
}
