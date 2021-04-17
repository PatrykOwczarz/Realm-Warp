using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup", menuName = "Powerup")]
public class Powerup : ScriptableObject
{
    new public string name = "New Item";
    public float amount = 0.0f;
    public Material material = null;

}
