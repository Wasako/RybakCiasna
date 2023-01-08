// This shader fills the mesh shape with a color predefined in the code.
Shader "Custom2d/Lit"
{
    Properties
    { 
        _MainTex("Color", 2D) = "white"
        _Normal("Normal", 2D) = "blue"
        _Emission("Emisssion", 2D) = "black"
    }

    // The SubShader block containing the Shader code. 
    SubShader
    {
        // SubShader Tags define when and under which conditions a SubShader block or
        // a pass is executed.
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }
        
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        ZTest Always

        HLSLINCLUDE
        #pragma vertex vert
        #pragma fragment frag

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        struct Attributes
        {
            float4 positionOS   : POSITION;
            float2 uv           : TEXCOORD0;
            float4 color        : COLOR0;
        };

        struct Varyings
        {
            float4 positionHCS  : SV_POSITION;
            float2 uv           : TEXCOORD0;
            float4 color        : COLOR0;
        };
        ENDHLSL

        Pass
        {
            Name "Color"
            Tags { "LightMode" = "Custom2D-Color" }

            HLSLPROGRAM
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.color = IN.color;
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float4 OUT = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * IN.color;
                return OUT;
            }
            ENDHLSL
        }

        Pass
        {
            Name "Normal"
            Tags { "LightMode" = "Custom2D-Normal" }
            
            HLSLPROGRAM
            TEXTURE2D(_Normal);
            SAMPLER(sampler_Normal);

            CBUFFER_START(UnityPerMaterial)
                float4 _Normal_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _Normal);
                OUT.color = IN.color;
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float4 OUT = SAMPLE_TEXTURE2D(_Normal, sampler_Normal, IN.uv) * IN.color;
                return OUT;
            }
            ENDHLSL
        }

        Pass
        {
            Name "Emission"
            Tags { "LightMode" = "Custom2D-Emission" }
            
            HLSLPROGRAM
            TEXTURE2D(_Emission);
            SAMPLER(sampler_Emission);

            CBUFFER_START(UnityPerMaterial)
                float4 _Emission_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _Emission);
                OUT.color = IN.color;
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float4 OUT = SAMPLE_TEXTURE2D(_Emission, sampler_Emission, IN.uv) * IN.color;
                return OUT;
            }
            ENDHLSL
        }
    }
}