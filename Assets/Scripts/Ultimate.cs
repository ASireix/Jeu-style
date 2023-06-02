using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    [SerializeField] string ultiName;
    [SerializeField] float ultiLength; 

    public virtual float Activate()
    {
        Debug.Log("Ultimate named " + ultiName + " activated");
        return ultiLength;
    }

    public virtual void Stop()
    {
        Debug.Log("Ultimate named " + ultiName + " stoped");
    }
}
