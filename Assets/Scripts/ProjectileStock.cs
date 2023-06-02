using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStock : MonoBehaviour
{
    public List<GameObject> projectiles;

    private void Start()
    {
        projectiles = new List<GameObject>();
    }

    public void AddProjectiles(GameObject proj)
    {
        projectiles.Add(proj);
        proj.GetComponent<Bullet>().OnBulletDestroyed.AddListener(RemoveProjectile);
    }

    void RemoveProjectile(GameObject proj)
    {
        projectiles.Remove(proj);
    }

    public void SetProjectilesSpeed(float speed)
    {
        foreach (var item in projectiles)
        {
            item.GetComponent<Bullet>().SetSpeed(speed);
        }
    }

    public void SetProjectilesMaxSpeed(float speed)
    {
        foreach (var item in projectiles)
        {
            
        }
    }

    public void InvertProjectileSpeed()
    {
        foreach (var item in projectiles)
        {
            Bullet tempBullet = item.GetComponent<Bullet>();

            tempBullet.currentSpeed = Mathf.Abs(tempBullet.currentSpeed - tempBullet.maxSpeed);
        }
    }

    public void MultiplyProjectileSpeed(float amount)
    {
        foreach (var item in projectiles)
        {
            float currentspeed = item.GetComponent<Bullet>().currentSpeed;
            item.GetComponent<Bullet>().SetMaxSpeed(currentspeed * amount);
        }
    }
}
