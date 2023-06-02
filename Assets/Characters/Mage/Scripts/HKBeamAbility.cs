using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HK Beam", menuName = "Abilities/Haraki K/Beam")]
public class HKBeamAbility : Ability
{
    public Vector3 spawnPos;
    public GameObject beamPrefab;

    public float beamDuration;
    public override void ActivateAbility(PlayerController player, PlayerState pState)
    {
        player.anim.SetTrigger(triggerAnimName);
        player.AnimationFunctionToCall = StartBeam;
    }

    void StartBeam(PlayerController player)
    {
        player.DecreaseEnergy(requiredEnergy);
        GameObject tempBeam = Instantiate(beamPrefab, player.shootingPos.position, player.transform.rotation);
        tempBeam.transform.SetParent(player.transform);
        tempBeam.transform.localPosition = spawnPos;
        tempBeam.transform.SetParent(null);
        Sigil sigil = tempBeam.GetComponentInChildren<Sigil>();
        if (player.wind)
        {
            sigil.SetWind(player.wind);
        }
        sigil.dmg = damage;
        sigil.playCtrl = player;
        sigil.gameObject.layer = player.gameObject.layer;
        sigil.SetBeamDuration(beamDuration);
        HarakiColorPalette palette = (HarakiColorPalette)player.GetComponent<HKColorPicker>().GetCurrentPalette();
        sigil.SetBeamColor(palette.mainBeamColor, palette.particleBeamColor);

    }
}
