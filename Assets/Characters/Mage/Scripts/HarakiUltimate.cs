using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarakiUltimate : Ultimate
{
    public float trailLength;
    float baseTrailLength;
    public float speedMultiplier;
    ProjectileStock projectileStock;
    List<GameObject> projectilesToUse;
    private void Start()
    {

    }

    public override float Activate()
    {
        projectileStock = gameObject.GetComponent<ProjectileStock>();
        projectileStock.MultiplyProjectileSpeed(speedMultiplier);
        Debug.Log(projectileStock.projectiles.Count);
        projectilesToUse = projectileStock.projectiles;
        foreach (var item in projectilesToUse)
        {
            item.GetComponentInChildren<TrailRenderer>().time = trailLength;
        }
        if (projectilesToUse.Count > 0)
        {
            baseTrailLength = projectilesToUse[0].GetComponentInChildren<TrailRenderer>().time;
            
        }
        
        return base.Activate();
    }

    public override void Stop()
    {
        base.Stop();
        projectileStock.MultiplyProjectileSpeed(1 / speedMultiplier);
        foreach (var item in projectilesToUse)
        {
            item.GetComponentInChildren<TrailRenderer>().time = baseTrailLength;
        }

    }
}
