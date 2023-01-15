using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour {
	
	[SerializeField] private Tilemap _floorTilemap;

	[Header("Tiles")]
	[SerializeField] private TileBase _floorTile;

	public void DrawFloorTiles(IEnumerable<Vector2Int> positions) {
		DrawTiles(positions, _floorTilemap, _floorTile);
	}

	private void DrawTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile) {
		
		foreach (var position in positions) {
			DrawTile(tilemap, tile, position);
		}
	}

	private void DrawTile(Tilemap tilemap, TileBase tile, Vector2Int position) 
	{
		var tilePosition = tilemap.WorldToCell((Vector3Int) position);
		tilemap.SetTile(tilePosition, tile);
	}

	public void Clear() {
		_floorTilemap.ClearAllTiles();
	}
}
