using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShootAbility : Ability
{
    public int requiredEnergy;
    public GameObject bulletPrefab;

    public override void Activate(PlayerController player)
    {
        if (!player.anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            
            player.anim.SetTrigger("Shoot");
            int r = Random.Range(0, 2);
            player.anim.SetFloat("ShootingAnim", r);
            player.AnimationFunctionToCall = ShootBullet;
        }
        
    }

    void ShootBullet(PlayerController player)
    {
        player.abilityManager.DecreaseEnergy(player.color,requiredEnergy);
        GameObject tempBullet = Instantiate(bulletPrefab, player.shootingPos.position, player.transform.rotation);
        tempBullet.GetComponent<Bullet>().SetBullet(player.color);
    }
}
