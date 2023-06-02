using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicaCloth;
using UnityEngine.VFX;
using MilkShake;
public class Sigil : MonoBehaviour
{
    public float fillValue;
    public float colorIntensity;
    public GameObject beam;
    VisualEffect beamVFX;

    public float fillSpeed;

    Material sigilMat;
    MagicaAreaWind wind;
    Animator anim;

    float beamTime = 5f;
    public PlayerController playCtrl;
    public ShakePreset shakePreset;
    ShakeInstance shakeInstance;

    public Hitbox hitbox;
    public float dmg;
    // Start is called before the first frame update
    void Awake()
    {
        sigilMat = GetComponent<Renderer>().material;
        fillValue = 0;
        anim = GetComponent<Animator>();
        FillSigil();
        beamVFX = beam.GetComponent<VisualEffect>();
        
    }

    public void SetBeamDuration(float d)
    {
        if (beamVFX)
        {
            beamVFX.SetFloat("Duration", d);
            beamTime = d;
        }
        hitbox.SetDamage(dmg, d, gameObject.layer);
    }

    public void SetBeamColor(Color main, Color particle)
    {
        beamVFX.SetVector4("MainBeamColor", main);
        beamVFX.SetVector4("ParticleColor", particle);

    }

    public void SetWind(MagicaAreaWind w)
    {
        wind = w;
    }

    void ShootBeam()
    {
        beam.SetActive(true);
        shakeInstance = Shaker.ShakeAll(shakePreset);
        shakeInstance.Start(0.2f);
        if (wind)
        {
            wind.Main = 60;
            wind.DirectionAngleX = -180;
        }
        StartCoroutine("WaitForBeam");
        
    }

    public void FillSigil()
    {
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        float i = 0f;
        while (i < 1f)
        {
            sigilMat.SetFloat("_Fill_Amount", i);
            i += fillSpeed/10f;
            yield return null;
        }
        sigilMat.SetFloat("_Fill_Amount", 1f);
        anim.enabled = true;
    }

    void DestroyRoot()
    {
        Destroy(transform.root.gameObject);
    }

    IEnumerator WaitForBeam()
    {
        yield return new WaitForSeconds(beamTime * 0.85f);
        playCtrl.anim.SetTrigger("EnterRecovery");
        if (wind)
        {
            wind.Main = 1;
            wind.DirectionAngleX = 0;
        }
        beam.SetActive(false);
        shakeInstance.Stop(0.2f,true);
        yield return new WaitForSeconds(beamTime - beamTime * 0.8f);
        anim.SetTrigger("Die");
    }
}
