using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandTracker : MonoBehaviour {

	public enum Hand {
		Left,
		Right
	}
	//	public Hand hand = Hand.Right;
	public OVRInput.Controller controller;

	protected void Update() {
		//		OVRInput.Controller c = hand == Hand.Left ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
		transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
		transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
		Debug.Log(gameObject.name + " at " + transform.localPosition + " (cam at " + Camera.main.transform.position + ")");
	}
}
