using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ProjectileDetectionEnbiggen : ProjectileDetection
{
    public float size;
    // Update is called once per frame
    void Update()
    {
        foreach (var item in detectedProjs)
        {
            item.gameObject.GetComponentInChildren<VisualEffect>().SetFloat("Size", size);
        }
    }
}
