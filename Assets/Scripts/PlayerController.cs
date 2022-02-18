using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;

    public string color;

    public Animator anim;

    public Transform shootingPos;
    public Transform beamPos;

    public delegate void TestDelegate(PlayerController playerController); // This defines what type of method you're going to call.
    public TestDelegate AnimationFunctionToCall;

    public AbilityManager abilityManager;

    protected AnimatorOverrideController animatorOverride;

    public AbilityHolder abilityOne;
    public AbilityHolder abilityTwo;

    private void Start()
    {
        animatorOverride = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverride;

        abilityOne.SetClips(this);
        abilityTwo.SetClips(this);
    }

    private void Update()
    {
        if (abilityManager.energy < abilityManager.maxEnergy)
        {
            abilityManager.IncreaseEnergy(color, abilityManager.currentRegenSpeed * Time.deltaTime);
        }
        
    }

    public void TriggerAnimFunction()
    {
       
        AnimationFunctionToCall(this);
        
    }

    
}
