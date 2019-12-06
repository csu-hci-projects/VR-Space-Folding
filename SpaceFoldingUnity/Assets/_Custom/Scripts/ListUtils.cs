using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ListUtils {
	// Extension method to shuffle a list.
	// Reference: http://stackoverflow.com/questions/273313/
	public static void Shuffle<T>(this IList<T> list) {  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = UnityEngine.Random.Range(0, n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
	
	// Extension method to pick a random element of a list.
	public static T RandomElement<T>(this List<T> list) {
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Extension method to pop the last element off a list.
	public static T Pop<T>(this List<T> list) {
		int idx = list.Count - 1;
		T result = list[idx];
		list.RemoveAt(idx);
		return result;
	}

	/// <summary>
	/// Returns an index into the list, weighted by the values in the list.
	/// So if you give it a list containing {16, 8}, then this should be
	/// twice as likely to return index 0 as index 1.
	/// </summary>
	/// <returns>The random index.</returns>
	/// <param name="weights">Weights.</param>
	public static int WeightedRandomIndex(List<float> weights) {
		float total = weights.Sum();
		float draw = UnityEngine.Random.Range(0f, total);
		for (int i=0; i<weights.Count; i++) {
			if (draw < weights[i]) return i;
			draw -= weights[i];
		}
		return weights.Count - 1;
	}

}
