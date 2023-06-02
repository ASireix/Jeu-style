using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDetection : MonoBehaviour
{
    public Team team;
    public List<Bullet> detectedProjs = new List<Bullet>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            Bullet b = other.GetComponent<Bullet>();
            if (b.CompareTeam(team))
            {
                detectedProjs.Add(b);
                Debug.Log("Adding projectile");
            }
        }
    }
}
