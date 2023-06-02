using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Haraki Color Palette", menuName = "ColorPalettes/Haraki Color Palette")]
public class HarakiColorPalette : ColorPalette
{
    [ColorUsage(true, true)]
    public Color bodyColor;
    [ColorUsage(true, true)]
    public Color bodyColorShadow;
    [ColorUsage(true, true)]
    public Color coatColor;
    [ColorUsage(true, true)]
    public Color coatColorShadow;
    [ColorUsage(true, true)]
    public Color projectileColorLight;
    [ColorUsage(true, true)]
    public Color projectileColorMain;
    [ColorUsage(true, true)]
    public Color projectileColorTrail;
    [ColorUsage(true, true)]
    public Color mainBeamColor;
    [ColorUsage(true, true)]
    public Color particleBeamColor;
}
