using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Light Projectile", menuName = "Abilities/Haraki K/Projectile")]
public class HKProjectiles : Ability
{
    [HideInInspector]
    public Color trailColor;
    [HideInInspector]
    public Color particleColor;
    public GameObject bulletPrefab;
    public override void ActivateAbility(PlayerController player, PlayerState pState)
    {
        player.anim.SetTrigger(triggerAnimName);
        int r = Random.Range(0, 2);
        player.anim.SetFloat("ShootingAnim", r);
        player.AnimationFunctionToCall = ShootBullet;

    }

    void ShootBullet(PlayerController player)
    {
        HarakiColorPalette palette = (HarakiColorPalette)player.GetComponent<HKColorPicker>().GetCurrentPalette();
        player.DecreaseEnergy(requiredEnergy);
        GameObject tempBullet = Instantiate(bulletPrefab, player.shootingPos.position, player.transform.rotation);
        player.GetComponent<ProjectileStock>().AddProjectiles(tempBullet);
        tempBullet.GetComponentInChildren<VisualEffect>().SetVector4("Color", palette.projectileColorMain);
        tempBullet.GetComponentInChildren<TrailRenderer>().gameObject.GetComponent<Renderer>().material.SetColor("_Color02", palette.projectileColorTrail);
        Bullet tempBulletComp = tempBullet.GetComponent<Bullet>();
        tempBullet.layer = player.gameObject.layer;
        tempBulletComp.dmg = damage;
    }
}
