using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour {

	public float MovementSpeed = 2;
	public float MouseSensitivity = 90;
	public Rigidbody rb;
	public GameObject cam;

	float yaw = 0;
	float roll = 0;

	void Start () {
		if (rb == null) rb = GetComponent<Rigidbody>();
		if (cam == null) cam = gameObject;
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
		else
			Cursor.lockState = CursorLockMode.Locked;

		if (Cursor.lockState == CursorLockMode.Locked) {
			Vector3 euler = cam.transform.eulerAngles;
			if (Input.GetKey(KeyCode.RightShift)) {
				roll = roll + MouseSensitivity * Input.GetAxis("Mouse X") * Time.deltaTime;
			} else {
				euler += new Vector3(-MouseSensitivity  * Input.GetAxis("Mouse Y"), 
						MouseSensitivity * Input.GetAxis("Mouse X"), 0.0f)  * Time.deltaTime;
				roll = Mathf.MoveTowardsAngle(roll, 0, 360 * Time.deltaTime);
			}
			euler.z = roll;
			cam.transform.eulerAngles = euler;
		}
		
		Vector3 forward = cam.transform.forward;
		forward.y = 0;
		forward = forward.normalized;

		Vector3 right = cam.transform.right;
		right.y = 0;
		right = right.normalized;

		Vector3 shift = Vector3.zero;
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			shift += forward * MovementSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			shift -= forward * MovementSpeed * Time.deltaTime;
		}
		
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			shift -= right * MovementSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			shift += right * MovementSpeed * Time.deltaTime;
		}
		if (rb != null) rb.MovePosition(rb.position + shift);
		else transform.position += shift;
	}
	
	public void PortalCameraCorrect(Transform target)
	{
		transform.position = target.position;
		cam.transform.rotation = target.rotation;
	}
}
