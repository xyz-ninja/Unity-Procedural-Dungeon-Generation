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

	public static List<BoundsInt> BinarySpacePartitioning(BoundsInt splitSpace, int minWidth, int minHeight) {
		
		var roomsQueue = new Queue<BoundsInt>();
		var rooms = new List<BoundsInt>();
		
		roomsQueue.Enqueue(splitSpace);

		while (roomsQueue.Count > 0) {
			
			var room = roomsQueue.Dequeue();
			
			// room can be split
			if (room.size.y >= minHeight && room.size.x >= minWidth) {
				if (Random.value < 0.5f) {
					if (room.size.y >= minHeight * 2) {
						
						// if contains space for 2 rooms
						SplitHorizontally(minHeight, roomsQueue, room);
						
					} else if (room.size.x >= minWidth * 2) {
					
						// if contains space for 2 rooms
						SplitVertically(minWidth, roomsQueue, room);
						
					} else if (room.size.x >= minWidth && room.size.y >= minHeight) {
						
						// contain space for single room
						rooms.Add(room);
					}

				} else {
					
					if (room.size.x >= minWidth * 2) {

						// if contains space for 2 rooms
						SplitVertically(minWidth, roomsQueue, room);
						
					} else if (room.size.y >= minHeight * 2) {
						
						// if contains space for 2 rooms
						SplitHorizontally(minHeight, roomsQueue, room);
						
					} else if (room.size.x >= minWidth && room.size.y >= minHeight) {
						
						// contain space for single room
						rooms.Add(room);
					}
				}
			}
		}

		return rooms;
	}

	private static void SplitVertically(in int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room) {

		var xSplit = Random.Range(1, room.size.x);
		
		BoundsInt room1 = new BoundsInt(
			room.min, 
			new Vector3Int(xSplit, room.size.y, room.size.z));
		
		BoundsInt room2 = new BoundsInt(
			new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
			new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
		
		roomsQueue.Enqueue(room1);
		roomsQueue.Enqueue(room2);
	}

	private static void SplitHorizontally(in int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room) {

		// for grid like generation, we can use
		// (minHeight, room.size.y - minHeight)
		
		var ySplit = Random.Range(1, room.size.y);
		
		BoundsInt room1 = new BoundsInt(
			room.min,
			new Vector3Int(room.size.x, ySplit, room.size.z));
		
		BoundsInt room2 = new BoundsInt(
			new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
			new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
		
		roomsQueue.Enqueue(room1);
		roomsQueue.Enqueue(room2);
	}
}