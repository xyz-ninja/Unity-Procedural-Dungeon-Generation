using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class DungeonGeneratorEditor : Editor {
    
    private AbstractDungeonGenerator _generator;

    private void Awake() {
        _generator = (AbstractDungeonGenerator) target;
    }

    public override void OnInspectorGUI() {
        
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Dungeon")) {
            _generator.GenerateDungeon();
        }
        
        if (GUILayout.Button("Clear")) {
            _generator.TilemapVisualizer.Clear();
        }
    }
}
