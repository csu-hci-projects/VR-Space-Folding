﻿/* FormatText

This component sets the Text property of a Text component
with a format string, and data it can receive from an event.

In case you don't have a Text component handy, you can also
hook up the resulting text with another event.
*/
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class FormatText : MonoBehaviour {
	#region Public Properties
	public string format = "{0}";

	public StringEvent textChanged;

	#endregion
	//--------------------------------------------------------------------------------
	#region Public Methods
	public void SetInt(int value) {
		ApplyText(string.Format(format, value));
	}

	public void SetFloat(float value) {
		ApplyText(string.Format(format, value));
	}
	
	public void SetDouble(double value) {
		ApplyText(string.Format(format, value));
	}
	
	public void SetString(string value) {
		ApplyText(string.Format(format, value));
	}

	#endregion
	//--------------------------------------------------------------------------------
	#region Private Methods
	void ApplyText(string text) {
		text = text.Replace("\\n", "\n");
		
		Text textComp = GetComponent<Text>();
		if (textComp != null) textComp.text = text;

		TextMeshProUGUI tmComp = GetComponent<TextMeshProUGUI>();
		if (tmComp != null) tmComp.text = (string.IsNullOrEmpty(text) ? " " : text);	// stupid TMPro bug
		
		TextMeshPro tmPro = GetComponent<TextMeshPro>();
		if (tmPro != null) tmPro.text = text;

		if (textChanged != null) textChanged.Invoke(text);
	}
	#endregion
}
