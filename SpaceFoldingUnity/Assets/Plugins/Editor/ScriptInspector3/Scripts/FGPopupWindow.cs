﻿/* SCRIPT INSPECTOR 3
 * version 3.0.25, March 2019
 * Copyright © 2012-2019, Flipbook Games
 * 
 * Unity's legendary editor for C#, UnityScript, Boo, Shaders, and text,
 * now transformed into an advanced C# IDE!!!
 * 
 * Follow me on http://twitter.com/FlipbookGames
 * Like Flipbook Games on Facebook http://facebook.com/FlipbookGames
 * Join discussion in Unity forums http://forum.unity3d.com/threads/138329
 * Contact info@flipbookgames.com for feedback, bug reports, or suggestions.
 * Visit http://flipbookgames.com/ for more info.
 */


namespace ScriptInspector
{

using UnityEngine;
using UnityEditor;

public class FGPopupWindow : EditorWindow
{
	private static int allowNextPopups;
	
	[System.NonSerialized]
	protected EditorWindow owner;
	
	protected Rect dropDownRect;
	protected bool horizontal;
	private bool flipped;
	private bool startsFlipped;
	protected bool resizing;
	
	private static System.Type containerWindowType;
	private static System.Reflection.MethodInfo fitToScreenMethod;
	private static System.Reflection.FieldInfo dontSaveToLayoutField;
	private static System.Reflection.FieldInfo parentField;
	private static System.Reflection.PropertyInfo windowProperty;
	private static System.Reflection.MethodInfo moveInFrontOfMethod;
	private static System.Reflection.MethodInfo showTooltip;
	
	static FGPopupWindow()
	{
		const System.Reflection.BindingFlags instanceFlags =
			System.Reflection.BindingFlags.Public |
			System.Reflection.BindingFlags.NonPublic |
			System.Reflection.BindingFlags.Instance;

		showTooltip = typeof(EditorWindow).GetMethod("ShowTooltip", instanceFlags);

		containerWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ContainerWindow");
		if (containerWindowType != null)
		{
			fitToScreenMethod = containerWindowType.GetMethod("FitWindowRectToScreen", instanceFlags);
			dontSaveToLayoutField = containerWindowType.GetField("m_DontSaveToLayout", instanceFlags);
			moveInFrontOfMethod = containerWindowType.GetMethod("MoveInFrontOf", new System.Type[] { containerWindowType });
		}
		
		parentField = typeof(EditorWindow).GetField("m_Parent", instanceFlags);
		if (parentField != null)
		{
			var viewType = typeof(EditorWindow).Assembly.GetType("UnityEditor.View");
			windowProperty = viewType.GetProperty("window", instanceFlags);
		}
	}
	
	protected void ShowTooltip()
	{
		if (showTooltip != null)
			showTooltip.Invoke(this, null);
		else
			ShowPopup();
	}
	
	protected void MoveInFrontOf(EditorWindow window)
	{
		if (moveInFrontOfMethod == null)
			return;
		var myContainer = GetContainerWindow(this);
		if (!(UnityEngine.Object)myContainer)
			return;
		var otherContainer = GetContainerWindow(window);
		if (!(UnityEngine.Object)otherContainer)
			return;
		moveInFrontOfMethod.Invoke(myContainer, new object[]{ otherContainer });
	}
	
	protected virtual void OnEnable()
	{
		if (allowNextPopups == 0)
		{
			EditorApplication.delayCall += () => { Close(); DestroyImmediate(this); };
		}
		else
		{
			--allowNextPopups;
		}
	}
	
	protected static T CreatePopup<T>() where T : FGPopupWindow
	{
		++allowNextPopups;
		T popupWindow = CreateInstance<T>();
		return popupWindow;
	}

	private static readonly object boxedValueTrue = true;
	private static readonly object boxedValueFalse = false;
	
	private static object GetContainerWindow(EditorWindow window)
	{
		if (parentField == null || windowProperty == null)
			return null;
		
		var hostView = parentField.GetValue(window);
		if (hostView == null)
			return null;
		
		return windowProperty.GetValue(hostView, null);
	}
	
	protected static Rect FitRectToScreen(Rect rc, EditorWindow window)
	{
		var container = GetContainerWindow(window);
		if (container == null)
			return rc;
		
		if (dontSaveToLayoutField != null)
			dontSaveToLayoutField.SetValue(container, boxedValueTrue);
		
		if (fitToScreenMethod == null)
			return rc;
		
		rc.height += 20f;
		rc = (Rect) fitToScreenMethod.Invoke(container, new object[] {rc, boxedValueTrue, boxedValueFalse});
		rc.height -= 20f;
		
		return rc;
	}
	
	public bool Flipped
	{
		get { return flipped; }
		set
		{
			if (value != flipped)
			{
				flipped = value;
				if (owner)
					SetSize(position.width, position.height);
				else
					startsFlipped = true;
			}
		}
	}
	
	protected void SetSize(float width, float height)
	{
		var x = horizontal ? (flipped ? dropDownRect.x - width : dropDownRect.xMax) : dropDownRect.x;
		var y = horizontal ? dropDownRect.y : (flipped ? dropDownRect.y - height : dropDownRect.yMax);
		var rc = new Rect(x, y, width, height);
		var fit = FitRectToScreen(rc, this);
		
		if (startsFlipped == flipped)
		{
			if (horizontal ? rc.x != fit.x : rc.y != fit.y)
			{	flipped = !flipped;
				if (flipped)
				{
					x = horizontal ? dropDownRect.x - width : fit.x;
					y = horizontal ? fit.y : dropDownRect.y - height;
					rc = new Rect(x, y, width, height);
					fit = FitRectToScreen(rc, this);
				}
				else
				{
					x = horizontal ? dropDownRect.x - width : fit.x;
					y = horizontal ? fit.y : dropDownRect.yMax;
					rc = new Rect(x, y, width, height);
					fit = FitRectToScreen(rc, this);
				}
			}
		}
		
		resizing = true;
		minSize = Vector2.one;
		maxSize = new Vector2(4000f, 4000f);
		position = fit;
		maxSize = minSize = new Vector2(width, height);
		resizing = false;
	}
}

}
