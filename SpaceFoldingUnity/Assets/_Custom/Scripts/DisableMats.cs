using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMats : MonoBehaviour {

	public Material[] matsToDisable;

	void Start() {
		foreach (Material mat in matsToDisable) {
			WorldClipPortal.HideEntirely(mat);
		}
	}
}
