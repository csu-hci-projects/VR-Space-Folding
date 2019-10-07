using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceActivator : MonoBehaviour {
	
	public bool activeInA;
	public bool activeInB;
	public bool activeInC;
	public bool activeInD;
	public bool activeInE;

	static int curSpaceNum = 0;
	
	static List<SpaceActivator> instances = new List<SpaceActivator>();
	
	protected void Awake() {
		instances.Add(this);
	}
	
	protected void Start() {
		Apply();
	}
	
	public static void NowInSpace(int newNum) {
		curSpaceNum = newNum;
		foreach (var v in instances) v.Apply();
	}
	
	void Apply() {
		gameObject.SetActive(
			(curSpaceNum == 0 && activeInA) ||
			(curSpaceNum == 1 && activeInB) ||
			(curSpaceNum == 2 && activeInC) ||
			(curSpaceNum == 3 && activeInD) ||
			(curSpaceNum == 4 && activeInE));			
	}
}
