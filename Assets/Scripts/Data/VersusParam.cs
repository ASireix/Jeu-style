using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Versus Manager")]
public class VersusParam : ScriptableObject
{
    public List<GameObject> playersList;
    public int[] playersPalettes;
}
