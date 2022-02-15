using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShootAbility : Ability
{

    public GameObject bulletPrefab;

    public override void Activate(PlayerController player)
    {
        player.anim.SetTrigger("Shoot");
        player.AnimationFunctionToCall = ShootBullet;
    }

    void ShootBullet(PlayerController player)
    {
        GameObject tempBullet = Instantiate(bulletPrefab, player.shootingPos.position, player.transform.rotation);
        tempBullet.GetComponent<Bullet>().SetBullet(player.color);
    }
}
