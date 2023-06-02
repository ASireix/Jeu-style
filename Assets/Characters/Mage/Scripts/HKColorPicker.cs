using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKColorPicker : ColorPicker
{
    public Renderer coat;
    public Renderer body;
    public AbilityHolder projectile;

    HarakiColorPalette customPalette;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetColors(int index)
    {
        base.SetColors(index);
        customPalette = (HarakiColorPalette)colorPalettes[index];
        SetShadowsAndTexture(coat, customPalette.coatColor, customPalette.coatColorShadow);
        SetShadowsAndTexture(body, customPalette.bodyColor, customPalette.bodyColorShadow);
    }


}
