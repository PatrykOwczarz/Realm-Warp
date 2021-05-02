using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A scriptable object to define the parameters of powerups.
// a more detailed explanation of what scriptable objects are, is provided in the report.
[CreateAssetMenu(fileName = "New Powerup", menuName = "Powerup")]
public class Powerup : ScriptableObject
{
    new public string name = "New Item";
    public float amount = 0.0f;
    public Material material = null;

}
