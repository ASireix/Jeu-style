Shader "Unlit/AnimeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightSmooth("Light Smooth", Float) = 0.1
        _ShadowColor("Shadow Color", Color) = (1,1,1,1)
        _BaseColor("Base Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            
        }
    }
}
