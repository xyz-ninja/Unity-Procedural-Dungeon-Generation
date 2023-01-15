using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkGeneratorData", menuName = "Generators/Walk Data")]
public class WalkGeneratorData : ScriptableObject {
    
    public int iterations = 10;
    public int walkLength = 10;

    public bool randomOriginPosition = true;
}
