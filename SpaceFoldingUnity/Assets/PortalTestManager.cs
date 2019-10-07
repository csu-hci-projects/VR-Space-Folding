/*	This script activates certain objects when we are in a certain
dimension, and deactivates objects for dimensions we're not in.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTestManager : MonoBehaviour, DimensionShiftNotificationReceiver {

	public Dimension[] dimensions;
	public Portal portalA;
	public Portal portalB;
	public Portal portalC;
	public Portal portalD;
	public GameObject wallD2B;
	public GameObject wallD2C;
	public GameObject wallD3A;
	public GameObject wallD3D;
		
	void Start () {
		// initial set-up
		foreach (var d in dimensions) d.gameObject.SetActive(true);
		wallD2B.SetActive(true);
		wallD2C.SetActive(false);
		wallD3D.SetActive(false);
		wallD3A.SetActive(false);
		portalA.gameObject.SetActive(false);
		portalB.gameObject.SetActive(false);
		portalC.gameObject.SetActive(false);
		portalD.gameObject.SetActive(true);
	}
	
	public void DimensionChanged(Dimension fromDimension, Dimension toDimension) {
		if (toDimension == dimensions[0]) {		// entering D1...
			wallD2B.SetActive(true);
			wallD2C.SetActive(false);
			wallD3D.SetActive(false);
			portalA.gameObject.SetActive(false);
			portalB.gameObject.SetActive(false);
			portalC.gameObject.SetActive(fromDimension == dimensions[1]);
			portalD.gameObject.SetActive(fromDimension == dimensions[2]);
		} else if (toDimension == dimensions[1]) {		// entering D2...
			wallD2C.SetActive(true);
			wallD2B.SetActive(true);
			portalA.gameObject.SetActive(false);
			portalD.gameObject.SetActive(false);
			portalB.gameObject.SetActive(true);
			portalC.gameObject.SetActive(true);
		} else if (toDimension == dimensions[2]) {		// entering D3...
			wallD3A.SetActive(true);
			wallD3D.SetActive(true);
			wallD2B.SetActive(false);
			wallD2C.SetActive(false);
			portalB.gameObject.SetActive(false);
			portalC.gameObject.SetActive(false);
			portalA.gameObject.SetActive(true);
			portalD.gameObject.SetActive(true);
		} else {										// entering D4...
			wallD2B.SetActive(false);
			wallD3A.SetActive(false);
			portalC.gameObject.SetActive(false);
			portalD.gameObject.SetActive(false);
			portalA.gameObject.SetActive(fromDimension == dimensions[2]);
			portalB.gameObject.SetActive(fromDimension == dimensions[1]);			
		}

	}
}
