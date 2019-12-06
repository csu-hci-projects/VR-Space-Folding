/*	MouseCameraController

This controller moves the camera with the mouse, to serve the same function
in the desktop app as moving the head does in the VR app.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePitchYaw : MonoBehaviour {

	public KeyCode requiredModifier = KeyCode.None;

	public float yawSpeed = 3;
	public float pitchSpeed = 3;
	
	private float yaw = 0;
	private float pitch = 0;
			
	void Awake() {
		// This script only needs to happen in the editor.  Not on the device.
		#if !UNITY_EDITOR
		this.enabled = false;
		#endif
	}
	
	void Start() {
		
	}
	
	void Update() {
		if (requiredModifier != KeyCode.None && !Input.GetKey(requiredModifier)) return;
		
		yaw += Input.GetAxisRaw("Mouse X") * yawSpeed;
		pitch = Mathf.Clamp(pitch - Input.GetAxisRaw("Mouse Y") * pitchSpeed, -90, 90);
		transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
	}
}
