using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;

    public string color;

    public Animator anim;

    public Transform shootingPos;

    public float maxEnergy;
    float energy;

    public delegate void TestDelegate(PlayerController playerController); // This defines what type of method you're going to call.
    public TestDelegate AnimationFunctionToCall;

    public void TriggerAnimFunction()
    {
        Debug.Log("Launch Anim");
        AnimationFunctionToCall(this);
        
    }
}
