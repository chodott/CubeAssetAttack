Shader "Custom/BlobShadowURP"
{
    Properties
    {
        _ShadowColor ("Shadow Color", Color) = (0, 0, 0, 0.7)
        _ShadowSize ("Shadow Size", Range(0.1, 1)) = 0.5
        _ShadowSoftness ("Shadow Softness", Range(0.01, 0.5)) = 0.1
        _ShadowStretch ("Shadow Stretch", Range(1, 3)) = 1.5 // 그림자 늘어나는 정도
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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
            float _ShadowStretch;  // 그림자 늘어나는 정도
            float3 _GlobalLightDir;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex);
                o.uv = v.uv * 2 - 1; // -1 ~ 1 범위로 변환
                o.worldPos = TransformObjectToWorld(v.vertex);

                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // 방향성 라이트의 방향을 반영하여 타원형 변형
                float3 lightDir = normalize(_GlobalLightDir);
                float2 stretchUV = i.uv;

                // X축(좌우) 방향으로 그림자를 늘리기
                stretchUV.x *= 1 + (_ShadowStretch - 1) * abs(lightDir.x);
                stretchUV.y *= 1 + (_ShadowStretch - 1) * abs(lightDir.z);

                // 타원형 거리 계산
                float dist = length(stretchUV) / _ShadowSize;

                // 부드러운 가장자리 적용
                float alpha = smoothstep(1, 1 - _ShadowSoftness, dist);

                return float4(_ShadowColor.rgb, _ShadowColor.a * alpha);
            }
            ENDHLSL
        }
    }
}