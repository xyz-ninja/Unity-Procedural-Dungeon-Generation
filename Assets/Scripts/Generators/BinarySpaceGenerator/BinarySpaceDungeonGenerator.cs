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
        floor = CreateSimpleRooms(rooms);
        
        _tilemapVisualizer.DrawFloorTiles(floor);
        WallGenerator.CreateWalls(floor, _tilemapVisualizer);
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
}
