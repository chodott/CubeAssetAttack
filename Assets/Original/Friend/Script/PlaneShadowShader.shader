Shader"Custom/BlobShadowURP"
{
    Properties
    {
        _ShadowColor ("Shadow Color", Color) = (0, 0, 0, 0.5) // 기본 검은색 + 반투명
        _ShadowSize ("Shadow Size", Range(0.1, 1)) = 0.5       // 그림자 크기
        _ShadowSoftness ("Shadow Softness", Range(0.01, 0.5)) = 0.1 // 그림자 가장자리 부드러움
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend
SrcAlpha OneMinusSrcAlpha // 알파 블렌딩 활성화

ZWrite Off // 투명 객체는 깊이 버퍼 쓰기 비활성화

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 worldPos : TEXCOORD1;
};

float4 _ShadowColor;
float _ShadowSize;
float _ShadowSoftness;
float3 _GlobalLightDir;

v2f vert(appdata v)
{
    v2f o;
    o.pos = TransformObjectToHClip(v.vertex); // 월드 좌표 변환
    o.uv = v.uv * 2 - 1; // UV 좌표를 -1 ~ 1 범위로 변환
    o.worldPos = TransformObjectToWorld(v.vertex);
    return o;
}

half4 frag(v2f i) : SV_Target
{
                // 원형 거리 계산 (UV 기준)
    float dist = length(i.uv) / _ShadowSize;

                // 부드러운 가장자리 적용
    float alpha = smoothstep(1, 1 - _ShadowSoftness, dist);

                // 그림자 색상 적용
    return float4(_ShadowColor.rgb, _ShadowColor.a * alpha);
}
            ENDHLSL
        }
    }
}