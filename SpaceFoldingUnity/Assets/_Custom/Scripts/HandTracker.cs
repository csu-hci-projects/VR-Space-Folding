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
		#if !UNITY_EDITOR
			transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
			transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
		#endif
	}
}
