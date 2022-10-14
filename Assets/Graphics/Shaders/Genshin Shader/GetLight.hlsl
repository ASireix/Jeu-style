#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
 
void GetSun_float3(out float3 Direction, out half3 Color)
{
    Light sun = GetMainLight();
    Direction = sun.direction;
    Color = sun.color;
}