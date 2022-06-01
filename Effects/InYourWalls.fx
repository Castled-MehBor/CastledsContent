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

float4 InYourWalls(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float mono = (color.r + color.g + color.b) / 3;
    float time = uTime * 3.14f;
    float pulsar = pow(pow((sin(coords.x * time)/15)*coords.y, 3), cos(2.25f));
    pulsar = 0.5f + frac(pulsar * ((coords.x + coords.y) / 2));
    pulsar /= 8;
    float4 monochrome = (mono, mono, mono, mono);
    float4 decoy = tex2D(uImage0, coords);
    decoy = LerpColor(color, color * monochrome, pulsar);
    decoy.rgb *= ((coords.y * (125, 125, 125)) + (1 - coords.x)) * mono;
    float4 staticCol = LerpColor(color, decoy, uOpacity);

    //Create Fade Effect
    float2 scale = uImageOffset;
    float3 col = uColor;
    float2 comparePoint = (uTargetPosition - uScreenPosition) / uScreenResolution;
    float fade = (3.14f * scale.x) * (pow(comparePoint.x - coords.x, 2)) + ((0.85f * scale.y) * pow(comparePoint.y - coords.y, 2));
    float fadeIn = (uProgress / fade) / 1.57f;
    if( fade > uProgress * pow(uIntensity, 2) )
       color = float4(staticCol.r - (uColor.r - (color.r * fadeIn)), staticCol.g - (uColor.g - (color.g * fadeIn)), staticCol.b - (uColor.b - (color.b * fadeIn)), 255);
    //
    //Outline
    float3 colorOutline = (125, 255, 255);
        if(fade > (uProgress - (0.0012)) * pow(uIntensity, 2) && fade < uProgress * pow(uIntensity, 2))
            color.rgb = colorOutline;
    //
    return color;
}

technique Technique1
{
    pass InYourWalls
    {
        PixelShader = compile ps_2_0 InYourWalls();
    }
}