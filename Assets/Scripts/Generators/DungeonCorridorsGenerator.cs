using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonCorridorsGenerator : SimpleWalkDungeonGenerator {
    
    [Header("Parametres")]
    [SerializeField] private int _corridorLength = 14;
    [SerializeField] private int _corridorsCount = 5;

    [SerializeField, Range(0.1f, 1f)] private float _roomPercent = 0.7f;

    protected override void RunGeneration() {
        GenerateFirstCorridor();
    }

    private void GenerateFirstCorridor() {
        
        var floorPositions = new HashSet<Vector2Int>();
        var potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        var roomPositions = CreateRooms(potentialRoomPositions);

        floorPositions.UnionWith(roomPositions);
        
        _tilemapVisualizer.DrawFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
    }

    private List<Vector2Int> _roomsToCreate;
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions) {
        
        _roomsToCreate.Clear();
        
        var roomPositions = new HashSet<Vector2Int>();
        int roomsCount = Mathf.RoundToInt(potentialRoomPositions.Count * _roomPercent);

        _roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid())
            .Take(roomsCount)
            .ToList();

        foreach (var roomPosition in _roomsToCreate) {
            
            var roomFloor = RunRandomWalk(_data, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions) {

        var currentPosition = _startPosition;
        potentialRoomPositions.Add(currentPosition);
        
        for (int i = 0; i < _corridorsCount; i++) {
            
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(
                currentPosition, _corridorLength);

            currentPosition = corridor[corridor.Count - 1]; // connect corridors
            potentialRoomPositions.Add(currentPosition);
            
            floorPositions.UnionWith(corridor);
        }
    }
}
