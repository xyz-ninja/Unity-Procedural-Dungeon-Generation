using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
	public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int length) {
		
		var path = new HashSet<Vector2Int>();

		path.Add(startPosition);

		var prevPosition = startPosition;

		for (int i = 0; i < length; i++) {
			
			var newPosition = prevPosition + Direction2D.GetRandomCardinalDirection();
			path.Add(newPosition);
			
			prevPosition = newPosition;
		}

		return path;
	}

	public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int length) {
		
		var corridor  = new List<Vector2Int>();
		var direction = Direction2D.GetRandomCardinalDirection();

		var currentPosition = startPosition;
		corridor.Add(currentPosition);
		
		for (int i = 0; i < length; i++) {
			
			currentPosition += direction;
			
			corridor.Add(currentPosition);
		}

		return corridor;
	}
}