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

namespace ScriptInspector.Themes
{
	using UnityEngine;
	using UnityEditor;
	
	[InitializeOnLoad]
	public class SolarizedDark
	{
		private static string _themeName = "Solarized Dark";
		
		static SolarizedDark()
		{
			FGTextEditor.AddTheme(_colourTheme, _themeName);
		}
		
		public static Theme _colourTheme = new Theme
		{
			background = new Color32(0x00, 0x2B, 0x36, 0xFF),
			text = new Color32(0x83, 0x94, 0x96, 0XFF),
			hyperlinks = new Color32(0x26, 0x8B, 0xD2, 0xFF),
			
			keywords = new Color32(0x71, 0x9A, 0x07, 0xFF),
			constants = new Color32(0x2A, 0xA1, 0x98, 0xFF),
			strings = new Color32(0x2A, 0xA1, 0x98, 0xFF),
			builtInLiterals = new Color32(0x71, 0x9A, 0x07, 0xFF),
			operators = new Color32(0x71, 0x9A, 0x07, 0xFF),
			
			referenceTypes = new Color32(0xB5, 0x89, 0x00, 0xFF),
			valueTypes = new Color32(0xB5, 0x89, 0x00, 0xFF),
			interfaceTypes = new Color32(0xB5, 0x89, 0x00, 0xFF),
			enumTypes = new Color32(0xB5, 0x89, 0x00, 0xFF),
			delegateTypes = new Color32(0x6C, 0x71, 0xC4, 0xFF),
			builtInTypes = new Color32(0x71, 0x9A, 0x07, 0xFF),
			
			namespaces = new Color32(0x65, 0x7B, 0x83, 0xFF),
			methods = new Color32(0x93, 0xA1, 0xA1, 0xFF),
			fields = new Color32(0x83, 0x94, 0x96, 0xFF),
			properties = new Color32(0x83, 0x94, 0x96, 0xFF),
			events = new Color32(0x6C, 0x71, 0xC4, 0xFF),
			
			parameters = new Color32(0x83, 0x94, 0x96, 0xFF),
			variables = new Color32(0x83, 0x94, 0x96, 0xFF),
			typeParameters = new Color32(0xD3, 0x36, 0x82, 0xFF),
			enumMembers = new Color32(0xD3, 0x36, 0x82, 0xFF),
			
			preprocessor = new Color32(0xCB, 0x4B, 0x16, 0xFF),
			defineSymbols = new Color32(0xD3, 0x36, 0x82, 0xFF),
			inactiveCode = new Color32(0x58, 0x6E, 0x75, 0xFF),
			comments = new Color32(0x58, 0x6E, 0x75, 0xFF),
			xmlDocs = new Color32(0x58, 0x6E, 0x75, 0xFF),
			xmlDocsTags = new Color32(0x58, 0x6E, 0x75, 0xFF),
			
			lineNumbers = new Color32(0x65, 0x7B, 0x83, 0xFF),
			lineNumbersHighlight = new Color32(0x83, 0x94, 0x96, 0xFF),
			lineNumbersBackground = new Color32(0x07, 0x36, 0x42, 0xFF),
			fold = new Color32(0x65, 0x7B, 0x83, 0xFF),
			
			activeSelection = new Color32(0x58, 0x6E, 0x75, 102),
			passiveSelection = new Color32(0x58, 0x6E, 0x75, 102),
			searchResults = new Color32(0x07, 0x36, 0x42, 0xFF),
			
			trackSaved = new Color32(0x71, 0x9A, 0x07, 0xFF),
			trackChanged = new Color32(0xB5, 0x89, 0x00, 0xFF),
			trackReverted = new Color32(95, 149, 250, 255),
			
			currentLine = new Color32(0x07, 0x36, 0x42, 0xFF),
			currentLineInactive = new Color32(0x07, 0x36, 0x42, 0xFF),
			
			referenceHighlight = new Color32(0x00, 0x49, 0x3E, 204),
			referenceModifyHighlight = new Color32(0x79, 0x22, 0x05, 144),
			
			tooltipBackground = new Color32(0x07, 0x36, 0x42, 0xFF),
			tooltipText = new Color32(0x83, 0x94, 0x96, 0XFF),
			tooltipFrame = new Color32(0x65, 0x7B, 0x83, 0xFF),
			
			listPopupBackground = new Color32(0x07, 0x36, 0x42, 0xFF),
		};
	}
}
