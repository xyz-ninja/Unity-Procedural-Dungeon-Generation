using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour {
	
	[SerializeField] protected TilemapVisualizer _tilemapVisualizer;
	[SerializeField] protected Vector2Int _startPosition = Vector2Int.zero;

	public TilemapVisualizer TilemapVisualizer => _tilemapVisualizer;

	public void GenerateDungeon() {
		
		_tilemapVisualizer.Clear();
		RunGeneration();
	}

	protected abstract void RunGeneration();
}
