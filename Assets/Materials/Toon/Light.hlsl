#ifndef GETLIGHT_INCLUDED
#define GETLIGHT_INCLUDED

// #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
 
void GetMainLight_float(out float3 Direction, out float3 Color)
{
    #if defined(SHADERGRAPH_PREVIEW)
    Direction = float3(0, 0.5, 0.5);
    Color = 1;
    #else
    Light light = GetMainLight();
    Direction = -light.direction;
    Color = light.color;
    #endif
}
 
#endif