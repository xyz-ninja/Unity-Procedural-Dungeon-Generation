using System.Collections.Generic;
using UnityEngine;

public class BinarySpaceDungeonGenerator : SimpleWalkDungeonGenerator {
    
    [SerializeField] private int _minRoomWidth = 4;
    [SerializeField] private int _minRoomHeight = 4;

    [SerializeField] private int _dungeonWidth = 20;
    [SerializeField] private int _dungeonHeight = 20;

    [SerializeField, Range(0, 10)] private int _offset = 1; // addit offset for walls
    [SerializeField] private bool _randomWalkRooms = false;

    protected override void RunGeneration() {
        CreateRooms();
    }

    private void CreateRooms() {
        
        var rooms = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt((Vector3Int) _startPosition, new Vector3Int(_dungeonWidth, _dungeonHeight, 0)),
            _minRoomWidth, 
            _minRoomHeight);
        
        var floor = new HashSet<Vector2Int>();

        if (_randomWalkRooms) {

            floor = CreateRandomWalkRooms(rooms);

        } else {
            
            floor = CreateSimpleRooms(rooms);
        }

        var roomCenters = new List<Vector2Int>();
        foreach (var room in rooms) {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        var corridors = GenerateCorridors(roomCenters);
        floor.UnionWith(corridors);
        
        _tilemapVisualizer.DrawFloorTiles(floor);
        WallGenerator.CreateWalls(floor, _tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRandomWalkRooms(List<BoundsInt> rooms) {
        
        var floor = new HashSet<Vector2Int>();
        
        for (int i = 0; i < rooms.Count; i++) {
            
            var roomBounds = rooms[i];
            var roomCenter = new Vector2Int(
                Mathf.RoundToInt(roomBounds.center.x),
                Mathf.RoundToInt(roomBounds.center.y));

            var roomFloor = RunRandomWalk(_data, roomCenter);
            foreach (var position in roomFloor) {
                // check offset
                if (position.x >= (roomBounds.xMin + _offset) && position.x <= (roomBounds.xMax - _offset) &&
                    position.y >= (roomBounds.yMin - _offset) && position.y <= (roomBounds.yMax + _offset)) {

                    floor.Add(position);
                }
            }
        }

        return floor;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> rooms) {
        
        var floor = new HashSet<Vector2Int>();

        foreach (var room in rooms) {
            for (int col = _offset; col < room.size.x - _offset; col++) {
                for (int row = _offset; row < room.size.y - _offset; row++) {
                    
                    var position = (Vector2Int) room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }

        return floor;
    }
    
    private HashSet<Vector2Int> GenerateCorridors(List<Vector2Int> roomCenters) {
        
        var corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];

        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0) {
            
            var closestCenterPoint = GetClosestPoint(currentRoomCenter, roomCenters);
            roomCenters.Remove(closestCenterPoint);

            var newCorridor = CreateCorridor(currentRoomCenter, closestCenterPoint);
            currentRoomCenter = closestCenterPoint;
            
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int startPosition, Vector2Int destinationPosition) {
        
        var corridor = new HashSet<Vector2Int>();

        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        while (currentPosition.y != destinationPosition.y) {
            
            if (destinationPosition.y > currentPosition.y) {
                currentPosition += Vector2Int.up;
            } else if (destinationPosition.y < currentPosition.y) {
                currentPosition += Vector2Int.down;
            } else {
                break;
            }

            corridor.Add(currentPosition);
        }
        
        while (currentPosition.x != destinationPosition.x) {
            
            if (destinationPosition.x > currentPosition.x) {
                currentPosition += Vector2Int.right;
            } else if (destinationPosition.x < currentPosition.x) {
                currentPosition += Vector2Int.left;
            } else {
                break;
            }

            corridor.Add(currentPosition);
        }

        return corridor;
    }

    private Vector2Int GetClosestPoint(Vector2Int point, List<Vector2Int> allPoints) {
        
        var closestPoint = Vector2Int.zero;
        float minDistance = float.MaxValue;

        foreach (var position in allPoints) {

            if (position == point) {
                continue;
            }
            
            var distance = Vector2.Distance(position, point);
            if (distance < minDistance) {

                closestPoint = position;
                
                minDistance = distance;
            }
        }

        return closestPoint;
    }
}
