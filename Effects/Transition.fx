sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float Lerp(float f1, float f2, float amount)
{
    return (f1 + (f2 - f1)) * amount;
}

float4 LerpColor(float4 c1, float4 c2, float lerp)
{
    float4 output;
    output.r = Lerp(c1.r, c2.r, lerp);
    output.g = Lerp(c1.g, c2.g, lerp);
    output.b = Lerp(c1.b, c2.b, lerp);
    output.a = Lerp(c1.a, c2.a, lerp);
    return output;
}
float4 Transition(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 decoy = tex2D(uImage0, coords);
    decoy.rgb *= (0, 0, 0);
    return LerpColor(decoy, color, 1 - uOpacity);
}

technique Technique1
{
    pass Transition
    {
        PixelShader = compile ps_2_0 Transition();
    }
}