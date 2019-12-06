using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class StateRecorder : MonoBehaviour {

	System.IO.StreamWriter recording;
	string recordingPath;
	int recordingLines;
	
	float startTime;
	string eventToLog;
	float nextLogTime;
	
	protected void Awake() {
		string dateStr = DateTime.Now.ToString("yyyy-MM-dd'T'HH-mm-ssK", CultureInfo.InvariantCulture);
		string name = string.Format("data-{0}.csv", dateStr);
		recordingPath = System.IO.Path.Combine(Application.persistentDataPath, name);
		recording = new System.IO.StreamWriter(recordingPath);
		if (recording != null) Debug.Log("Created output file at " + recordingPath);
		recording.WriteLine("Time,Elapsed,X,Y,Z,Heading,Event");
	}
	
	protected void OnDisable() {
		if (recording != null) {
			recording.Close();
			recording = null;
			Debug.Log("Closed output file with " + recordingLines + " lines at " + recordingPath);
		}
	}
	
	protected void Update() {
		if (Time.time < nextLogTime && string.IsNullOrEmpty(eventToLog)) return;
		nextLogTime = Time.time + 0.1f;
		
		var now = DateTime.Now;
		string timeStr = now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);
		float elapsed = Time.time - startTime;
		Vector3 pos = Camera.main.transform.position;
		Vector2 fwd2 = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
		float heading = fwd2.SignedAngle();
		recording.WriteLine("{0},{1},{2},{3},{4},{5},{6}",
			timeStr,
			elapsed,
			pos.x.ToString("0.000"),
			pos.y.ToString("0.000"),
			pos.z.ToString("0.000"),
			heading.ToString("0.0"),
			eventToLog
		);
		recordingLines++;
		eventToLog = null;
	}
	
	public void LogEvent(string s) {
		eventToLog = s;
	}
}
