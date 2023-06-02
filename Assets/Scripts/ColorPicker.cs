using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public List<ColorPalette> colorPalettes;
    public int paletteIndex;

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetColors(int index)
    {
        Debug.Log("Setting up color");
        paletteIndex = index;
    }

    protected virtual void SetShadowsAndTexture(Renderer mat, Color baseC, Color shadowC)
    {
        mat.material.SetColor("_BaseColor", baseC);
        mat.material.SetColor("_ShadowColor", shadowC);
    }

    public ColorPalette GetCurrentPalette()
    {
        return colorPalettes[paletteIndex];
    }
}
