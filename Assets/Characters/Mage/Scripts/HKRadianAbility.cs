using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "HK Radian Light", menuName = "Abilities/Haraki K/Radian Light")]
public class HKRadianAbility : Ability
{
    public float newProjSpeed;
    public float deleteTimer;
    public float detectionRange;
    public float newSize;
    GameObject sphere;
    public VisualEffect particlePrefab;
    VisualEffect instancedParticle;
    public Vector3 spawnPos;
    public override void ActivateAbility(PlayerController player, PlayerState pState)
    {
        player.anim.SetTrigger(triggerAnimName);
        player.AnimationFunctionToCall = RadianLight;
    }

    void RadianLight(PlayerController player)
    {
        player.DecreaseEnergy(requiredEnergy);
        if (sphere)
        {
            Destroy(sphere);
        }
        else
        {
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = player.shootingPos.position;
            sphere.GetComponent<SphereCollider>().isTrigger = true;
            sphere.GetComponent<MeshRenderer>().enabled = false;
            sphere.transform.localScale = sphere.transform.localScale * detectionRange;
            sphere.AddComponent<ProjectileDetectionEnbiggen>().size = newSize;
        }

        if (instancedParticle && sphere)
        {
            instancedParticle.Play();
        }
        else
        {
            instancedParticle = Instantiate(particlePrefab);
            instancedParticle.transform.SetParent(player.transform);
            instancedParticle.transform.localPosition = spawnPos;
            instancedParticle.transform.localRotation = Quaternion.identity;
            instancedParticle.transform.localScale = Vector3.one;
        }
    }
}
