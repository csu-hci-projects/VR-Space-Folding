using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProtocolManager : MonoBehaviour {

	[System.Serializable]
	public struct Target {
		public StarCollector star;
		public Sprite promptLabel;
	}
	public List<Target> targets;
	
	public Image prompt;
	public Image checkmark;
	
	int nextTargetIdx;
	
	protected void Start() {
		// Hide all stars, and hook up the events.
		foreach (var target in targets) {
			target.star.Hide();
			target.star.onCollected.AddListener(StarCollected);
		}
		
		// Then, pick one at random to show.
		targets.Shuffle();
		SetupNextTarget();
	}
	
	
	public void StarCollected(GameObject star) {
		GetComponent<StateRecorder>().LogEvent("collected " + star.gameObject.name);
		checkmark.gameObject.SetActive(true);
		prompt.GetComponent<CanvasGroupFader>().FadeOut(1.5f);
		Invoke("SetupNextTarget", 2);
	}
	
	void SetupNextTarget() {
		if (nextTargetIdx >= targets.Count) {
			targets.Shuffle();
			nextTargetIdx = 0;
		}
		var target = targets[nextTargetIdx];
		nextTargetIdx++;
		
		target.star.Show();
		prompt.sprite = target.promptLabel;
		prompt.GetComponent<CanvasGroupFader>().FadeIn(0.5f);
		prompt.GetComponent<AudioSource>().Play();
		checkmark.gameObject.SetActive(false);
		GetComponent<StateRecorder>().LogEvent("prompted " + target.star.gameObject.name);
	}
}
