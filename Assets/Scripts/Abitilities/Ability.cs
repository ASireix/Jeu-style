using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldDownTime;
    public float activeTime;

    public virtual void Activate(PlayerController player)
    {

    }

}
