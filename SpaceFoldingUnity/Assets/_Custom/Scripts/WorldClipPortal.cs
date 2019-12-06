using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldClipPortal : MonoBehaviour {

	public Material localSpaceMaterial;
	public Material remoteSpaceMaterial;

	public int leadsToSpaceNum;

	public bool inRemote;
	
	public UnityEvent onEnteredLocal;
	public UnityEvent onEnteredRemote;

	Renderer rend;
	Vector2 lastCamPos;
	
	void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = false;

		Transform camT = Camera.main.transform;
		lastCamPos = new Vector2(camT.position.x, camT.position.z);
	}
	
	[ContextMenu("Show All")]
	protected void OnApplicationQuit() {
		ShowEntirely(remoteSpaceMaterial);
		ShowEntirely(localSpaceMaterial);
	}
	
	void Update() {
		if (gameObject.activeInHierarchy) DoMagic(false);
	}
	
	void LateUpdate() {
		if (gameObject.activeInHierarchy) DoMagic(true);
	}
	
	void DoMagic(bool isLate) {
		if (Input.GetKeyDown("f")) inRemote = !inRemote;
		
		// Wow, it's inefficient to do all this stuff.  But if it works,
		// we can refactor.  (ToDo)
		
		// Get the position of this portal in the frustum.  Is it even in view?
		// And do this in 2D, which will be a little more reliable than checking
		// the 3D position of the center or bottom or whatever.
		Transform camT = Camera.main.transform;
		Vector2 cam = new Vector2(camT.position.x, camT.position.z);
		Vector2 camFwd = new Vector2(camT.forward.x, camT.forward.z);
		float fov = Camera.main.fieldOfView * Camera.main.aspect;
		float minAng = -45;//-0.5f * fov;
		float maxAng = 45;//0.5f * fov;
		
		// Also figure out which side of the portal we're on.
		// If on the backside, bail out.
		Vector2 portal = new Vector2(transform.position.x, transform.position.z);
		// Our portals are quads.  Quads render from the BACK, not the front.
		// I have no idea why.  But that's why we define portalFwd as:
		Vector2 portalFwd = new Vector2(-transform.forward.x, -transform.forward.z);
		bool closeEnough = (Vector2.Distance(cam, portal) < 0.5f);
		bool nowOnFront = Vector2.Angle(cam - portal, portalFwd) < 90;
		bool wasOnFront = Vector2.Angle(lastCamPos - portal, portalFwd) < 90;
		if (closeEnough && wasOnFront && !nowOnFront) {
			// Transition to another dimension!
			inRemote = !nowOnFront;
			Debug.Log(gameObject.name + " transitioned to " + (inRemote ? "remote " : "local ")
				+ (inRemote ? remoteSpaceMaterial.name : localSpaceMaterial.name)
				+ " on frame " + Time.frameCount
				 + "; active="+gameObject.activeInHierarchy, gameObject);
			SpaceActivator.NowInSpace(leadsToSpaceNum);
			if (inRemote) onEnteredRemote.Invoke();
			else onEnteredLocal.Invoke();
		}
		lastCamPos = cam;
		if (!nowOnFront) {
			if (!isLate) {
				ShowEntirely(localSpaceMaterial);
				HideEntirely(remoteSpaceMaterial);
			}
			return;
		}
		
		Material back, front;
		Vector2 right;
		//if (inRemote) {
		//	// We are on the back side of this portal.
		//	front = remoteSpaceMaterial;
		//	back = localSpaceMaterial;
		//	right = Vector2.right;
		//	if (Input.GetKeyDown(KeyCode.Space)) Debug.Log("on back of portal; front="+front + ", back="+back);
		//} else {
			// We're on the front side.
			front = localSpaceMaterial;
			back = remoteSpaceMaterial;
			right = new Vector2(transform.right.x, transform.right.z);
		if (Input.GetKeyDown(KeyCode.Space)) Debug.Log("on front of portal; front="+front + ", back="+back + ", right=" + right);
		//}
		//	if (transform.rotation.y > 179) right = -right;
		
		Vector2 portalLeft = portal - right * transform.localScale.x * 0.5f;
		Vector2 portalRight = portal + right * transform.localScale.x * 0.5f;
		float angle = Vector2.Angle(portal - cam, camFwd);
		float angleLeft = Vector2.SignedAngle(portalLeft - cam, camFwd);
		float angleRight = Vector2.SignedAngle(portalRight - cam, camFwd);
		if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(gameObject.name + "angle: " + angle + ", angleLeft: " + angleLeft + "  angleRight: " + angleRight + "  fov: " +  fov);
		if (/*angle < 90 &&*/ angleLeft < maxAng && angleRight > minAng) {
			if (!isLate) return;
			
			// Portal is visible.  Do our magic.
			if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(gameObject.name + ": Clipping " + localSpaceMaterial + " and " + remoteSpaceMaterial);
			rend.enabled = false;

			// Clip at the portal position.
			Vector4 cutSign = new Vector4(Mathf.Round(-transform.forward.x), 0, Mathf.Round(-transform.forward.z), 1);
			//Vector4 cutSign = new Vector4(-1, 0, 0, 1);
			Vector3 cutPoint = new Vector4(transform.position.x, transform.position.y, transform.position.z, 1);
			back.SetVector("_CutPoint", cutPoint);
			front.SetVector("_CutPoint", cutPoint);
			Debug.DrawRay(transform.position, Vector3.up, Color.red, 0);
			
			// Then, also clip by the XZ line of each edge.
			back.SetVector("_LineBound1", new Vector4(portalLeft.x, portalLeft.y, cam.x, cam.y));
			front.SetVector("_LineBound1", new Vector4(portalLeft.x, portalLeft.y, cam.x, cam.y));
			back.SetVector("_LineBound2", new Vector4(portalRight.x, portalRight.y, cam.x, cam.y));
			front.SetVector("_LineBound2", new Vector4(portalRight.x, portalRight.y, cam.x, cam.y));
			
			Debug.DrawLine(new Vector3(portalLeft.x, 0.1f, portalLeft.y), new Vector3(portalRight.x, 0.1f, portalRight.y), Color.blue, 0);
			Debug.DrawRay(new Vector3(portalLeft.x, 0, portalLeft.y), Vector3.up * 3, Color.blue, 0);
			Debug.DrawRay(new Vector3(portalRight.x, 0, portalRight.y), Vector3.up * 3, Color.green, 0);

			
			// And do the same for the local space, but with opposite cut sign.
			back.SetVector("_CutSign", -cutSign);
			front.SetVector("_CutSign", cutSign);
		} else {
			// Nope, the portal is outside the viewing frustum.
			// Hide the remote space and show the entire local space.
			if (isLate) return;
			if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(gameObject.name + " is out of view.  Hiding " + back + " and showing " + front);
			rend.enabled = true;
			HideEntirely(back);
			ShowEntirely(front);
		}

	}
	
	public void Hide() {
		HideEntirely(remoteSpaceMaterial);
		gameObject.SetActive(false);
		Debug.Log("Hid " + gameObject + "; active="+gameObject.activeInHierarchy, gameObject);
	}
	
	public void Show() {
		gameObject.SetActive(true);
		Debug.Log("Showed " + gameObject, gameObject);
	}
	
	public void HideLocal() {
		HideEntirely(localSpaceMaterial);
	}
	
	public void HideRemote() {
		HideEntirely(remoteSpaceMaterial);
	}
	
	public static void HideEntirely(Material mat) {
		mat.SetVector("_LineBound1", new Vector4(0,0,1,0));
		mat.SetVector("_LineBound2", new Vector4(0,0,1,0));
		mat.SetVector("_CutSign", new Vector4(1,1,1,-1));		
	}
	
	public static void ShowEntirely(Material mat) {
		mat.SetVector("_LineBound1", new Vector4(0,0,1,0));
		mat.SetVector("_LineBound2", new Vector4(0,0,1,0));
		mat.SetVector("_CutSign", new Vector4(1,1,1,1));		
	}
}
