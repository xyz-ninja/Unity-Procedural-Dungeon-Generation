using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleWalkDungeonGenerator : AbstractDungeonGenerator {

    [Header("Data")]
    [SerializeField] protected WalkGeneratorData _data;
    
    protected override void RunGeneration() {

        var floorPositions = RunRandomWalk(_data, _startPosition);
        
        _tilemapVisualizer.Clear();
        _tilemapVisualizer.DrawFloorTiles(floorPositions);
        
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
    }

    private HashSet<Vector2Int> _floorPositions = new HashSet<Vector2Int>();
    protected HashSet<Vector2Int> RunRandomWalk(WalkGeneratorData data, Vector2Int position) {
       
        _floorPositions.Clear();

        var currentPosition = position;
        
        //HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        
        for (int i = 0; i < _data.iterations; i++) {
            
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, _data.walkLength);
            _floorPositions.UnionWith(path);

            if (_data.randomOriginPosition) {
                currentPosition = _floorPositions.ElementAt(Random.Range(0, _floorPositions.Count));
            }
        }

        return _floorPositions;
    }
}
