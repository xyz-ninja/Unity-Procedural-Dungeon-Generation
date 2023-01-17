using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer visualizer) {

        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirections);

        foreach (var position in basicWallPositions) {
            visualizer.DrawWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions) {
        
        var wallPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions) {
            foreach (var direction in directions) {
                
                var neighbourPosition = position + direction;

                if (floorPositions.Contains(neighbourPosition) == false) {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }

        return wallPositions;
    }
}
