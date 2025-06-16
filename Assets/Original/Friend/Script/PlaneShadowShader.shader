Shader "Custom/BlobShadowURP_SimpleWorking"
{
    Properties
    {
        _ShadowColor ("Shadow Color", Color) = (0, 0, 0, 0.7)
        _ShadowSize ("Shadow Size", Range(0.1, 1)) = 0.5
        _ShadowSoftness ("Shadow Softness", Range(0.01, 0.5)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            Name "BlobShadowPass"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #pragma multi_compile_instancing
            #pragma instancing_options assumeuniformscaling

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _ShadowColor)
                UNITY_DEFINE_INSTANCED_PROP(float, _ShadowSize)
                UNITY_DEFINE_INSTANCED_PROP(float, _ShadowSoftness)
            UNITY_INSTANCING_BUFFER_END(Props)

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_TRANSFER_INSTANCE_ID(IN, OUT);

                float3 worldPos = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.positionHCS = TransformWorldToHClip(worldPos);
                OUT.uv = IN.uv * 2.0 - 1.0;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);

                float4 shadowColor = UNITY_ACCESS_INSTANCED_PROP(Props, _ShadowColor);
                float shadowSize = UNITY_ACCESS_INSTANCED_PROP(Props, _ShadowSize);
                float shadowSoftness = UNITY_ACCESS_INSTANCED_PROP(Props, _ShadowSoftness);

                float dist = length(IN.uv) / shadowSize;
                float alpha = smoothstep(1.0, 1.0 - shadowSoftness, dist);

                return float4(shadowColor.rgb, shadowColor.a * alpha);
            }

            ENDHLSL
        }
    }

    FallBack Off
}