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

float4 TestShader(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float mono = (color.r + color.g + color.b) / 3;
    float time = uTime * 3.14f;
    float pulsar = pow(pow((sin(coords.x * time)/15)*coords.y, 3), cos(2.25f));
    pulsar = 0.5f + frac(pulsar * ((coords.x + coords.y) / 2));
    pulsar /= 8;
    float4 monochrome = (mono, mono, mono, mono);
    color = LerpColor(color, color * monochrome, pulsar);
    color.rgb *= ((coords.y * (125, 125, 125)) + (1 - coords.x)) * mono;
    return color;
}

technique Technique1
{
    pass TestShader
    {
        PixelShader = compile ps_2_0 TestShader();
    }
}