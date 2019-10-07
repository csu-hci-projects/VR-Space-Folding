using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour {

	public UnityEvent onEnter;
	public UnityEvent onExit;

	public Collider[] validTriggers;

	protected void OnTriggerEnter(Collider other) {
		if (System.Array.IndexOf(validTriggers, other) >= 0) {
			onEnter.Invoke();
		}
	}
	
	protected void OnTriggerExit(Collider other) {
		if (System.Array.IndexOf(validTriggers, other) >= 0) {
			onExit.Invoke();
		}
	}
}
