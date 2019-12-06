/*	Extensions

	This module defines miscellaneous extensions for Unity
	or System classes.
*/

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class Extensions {
	#region Vector2

	/// <summary>
	/// Returns the signed angle between this vector and another.
	/// (This is unlike Vector2.Angle, which always returns the
	/// absolute value of the angle.)
	/// </summary>
	/// <returns>The signed angle, in degrees, from A to B.</returns>
	/// <param name="a">Vector this was called on.</param>
	/// <param name="b">Vector to measure the angle to.</param>
	public static float SignedAngleTo(this Vector2 a, Vector2 b) {
		return Mathf.Atan2( a.x*b.y - a.y*b.x, a.x*b.x + a.y*b.y ) * Mathf.Rad2Deg;
	}

	/// <summary>
	/// Returns the signed angle between this vector and the +X axis.
	/// </summary>
	/// <returns>The signed angle, reprenting the direction of this in degrees.</returns>
	/// <param name="a">Vector this was called on.</param>
	public static float SignedAngle(this Vector2 a) {
		return Mathf.Atan2( a.y, a.x ) * Mathf.Rad2Deg;
	}

	#endregion
	//--------------------------------------------------------------------------------
	
	#region List
	
	public static T Last<T>(this List<T> list) {
		return list[list.Count - 1];
	}
	
	public static string JoinToString<T>(this List<T> list, string delimiter=", ") {
		int count = list.Count;
		string[] strs = new string[count];
		for (int i=0; i<count; i++) strs[i] = list[i].ToString();
		return string.Join(delimiter, strs);
	}
	
	#endregion
	//--------------------------------------------------------------------------------
	
	#region Dictionary
	
	public static VT Lookup<KT,VT>(this Dictionary<KT,VT> dict, KT key, VT defaultValue) {
		if (!dict.ContainsKey(key)) return defaultValue;
		return dict[key];
	}
		
	#endregion
}
