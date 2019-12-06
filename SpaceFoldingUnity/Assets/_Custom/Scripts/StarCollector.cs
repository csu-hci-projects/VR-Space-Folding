/*
Attach this script to a collectible star.  It detects when either
of the player's hands is close enough, then does the "collected"
effect, and notifies the main manager.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]

public class StarCollector : MonoBehaviour {

	// Transforms which must be close enough to collect this item.
	public Transform[] hands;
	
	public UnityEvent collected;
	public UnityEvent shown;
	
	SphereCollider collider;
	bool visible;
	float transitionVal;	// 0: normal visible state; 1: totally gone

	Vector3 startPos;
	Vector3 startScale;
	
	protected void Start() {
		startPos = transform.position;
		startScale = transform.localScale;
		collider = GetComponent<SphereCollider>();
		Show();
	}
	
	protected void Update() {
		// if not visible (or transitioning away), do transition and bail out
		if (!visible) {
			if (transitionVal < 1) ContinueTransition();
			if (Input.GetKeyDown(KeyCode.Space)) Show();
			return;
		}

		// otherwise, do our inviting bounce
		transform.position = startPos + 0.1f * Vector3.up * Mathf.Abs(Mathf.Cos(Time.time/1f * Mathf.PI));

		// and then check for a touch from the user's hands
		Vector3 center = collider.bounds.center;
		float radius = collider.bounds.extents.x;
		
		foreach (Transform h in hands) {
			if (Vector3.Distance(h.position, center) < radius) {
				HandleTouch(h);
				break;
			}
		}
	}

	void HandleTouch(Transform hand) {
		Debug.Log(gameObject.name + " touched by " + hand.name);
		collected.Invoke();
		StartTransition();
	}
	
	void StartTransition() {
		visible = false;
		transitionVal = 0;
		var audio = GetComponent<AudioSource>();
		if (audio != null) audio.Play();
	}
	
	void ContinueTransition() {
		transitionVal = Mathf.MoveTowards(transitionVal, 1, Time.deltaTime / 0.5f);
		transform.position = Vector3.Lerp(startPos, startPos + Vector3.up * 1f, transitionVal);
		transform.localScale = startScale * (1 - transitionVal);
	}
	
	void Show() {
		visible = true;
		transform.position = startPos;
		transform.localScale = startScale;		
	}
}
