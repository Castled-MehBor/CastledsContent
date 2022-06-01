sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;

float Sine(float val)
{
    return ((0.5f * sin(uTime)) + 0.5f) * val;
}
float4 TestDye1(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 noise = tex2D(uImage1, coords.xy);
    float frameY = (coords.y * uImageSize0.y - uSourceRect.y) / uSourceRect.w;
    color.rgb *= uColor;
    if((pow(0.9f * (coords.x + (sin(uTime / 1.57f)) + 0.1f), 0.48f) + pow(0.5f * (frameY + (sin(uTime / 1.57f)) - 1), 0.48f)) < (0.75f + Sine(0.5f)))
       color.rgb *= (color.b * Sine(15), color.r * Sine(15), color.g * Sine(15), color.a / ((color.r + color.g + color.b) * Sine(9)));
    if((pow(0.9f * (coords.x + (sin(uTime) * 1.57f) + 0.1f), 2) + pow(0.9f * (frameY + (sin(uTime) * 1.25f)), 2)) < (0.75f + Sine(0.5f)))
       color.rgb /= (color.r / Sine(9), color.g / Sine(9), color.b / Sine(9), color.a / ((color.r + color.g + color.b) * Sine(3)));
    return color * sampleColor;
}
    
technique Technique1
{
    pass TestDye1
    {
        PixelShader = compile ps_2_0 TestDye1();
    }
}