using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class SimpleWalkDungeonGenerator : AbstractDungeonGenerator {

    [Header("Data")]
    [SerializeField] private WalkGeneratorData _data;

    [Button()]
    protected override void RunGeneration() {

        var floorPositions = RunRandomWalk();
        
        _tilemapVisualizer.Clear();
        _tilemapVisualizer.DrawFloorTiles(floorPositions);
    }

    private HashSet<Vector2Int> _floorPositions = new HashSet<Vector2Int>();
    protected HashSet<Vector2Int> RunRandomWalk() {
       
        _floorPositions.Clear();

        var currentPosition = _startPosition;
        
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
