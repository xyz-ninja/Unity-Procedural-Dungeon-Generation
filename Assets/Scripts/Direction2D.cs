using System.Collections.Generic;
using UnityEngine;

public static class Direction2D {
	
	public static List<Vector2Int> cardinalDirections = new List<Vector2Int>() {
		new Vector2Int(0, 1),  // up
		new Vector2Int(1, 0),  // right
		new Vector2Int(0, -1), // down
		new Vector2Int(-1, 0)  // left
	};

	public static Vector2Int GetRandomCardinalDirection() {
		return cardinalDirections[Random.Range(0, cardinalDirections.Count)];
	}
}