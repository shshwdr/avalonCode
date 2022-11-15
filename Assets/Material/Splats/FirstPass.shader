// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Nature/Terrain/Diffuse" {
    Properties {
        // used in fallback on old cards & base map
        [HideInInspector] _MainTex ("BaseMap (RGB)", 2D) = "white" {}
        [HideInInspector] _Color ("Main Color", Color) = (1,1,1,1)
        [HideInInspector] _TerrainHolesTexture("Holes Map (RGB)", 2D) = "white" {}
    }

        CGINCLUDE
#pragma surface surf Lambert vertex:SplatmapVert finalcolor:SplatmapFinalColor finalprepass:SplatmapFinalPrepass finalgbuffer:SplatmapFinalGBuffer addshadow fullforwardshadows
#pragma instancing_options assumeuniformscaling nomatrices nolightprobe nolightmap forwardadd
#pragma multi_compile_fog
#include "TerrainSplatmapCommon.cginc"

    //    struct Input {
    //    float2 uv_Control : TEXCOORD0;
    //    float2 uv_Splat0 : TEXCOORD1;
    //    float2 uv_Splat1 : TEXCOORD2;
    //    float2 uv_Splat2 : TEXCOORD3;
    //    float2 uv_Splat3 : TEXCOORD4;
    //};

        void surf(Input IN, inout SurfaceOutput o)
        {
            half4 splat_control;
            half weight;
            fixed4 mixedDiffuse;
            SplatmapMix(IN, splat_control, weight, mixedDiffuse, o.Normal);

            //// texture scape for cliff texture
            //float3 scaledWorldPos = IN.localCoord / _MapScale;

            //// Get UVs for each axis based on local position of the fragment.
            //half2 yUV = IN.localCoord.xz / _MapScale;
            //half2 xUV = IN.localCoord.zy / _MapScale;
            //half2 zUV = IN.localCoord.xy / _MapScale;

            //// Texture samples from diffuse map with each of the 3 UV sets.
            //half3 yDiff = tex2D(_VerticalTex, yUV);
            //half3 xDiff = tex2D(_VerticalTex, xUV);
            //half3 zDiff = tex2D(_VerticalTex, zUV);

            //// Get the absolute value of the world normal.
            //// Put the blend weights to the power of BlendSharpness to sharpen transition
            //half3 blendWeights = pow(abs(IN.localNormal), _TriplanarBlendSharpness);

            //// Divide blend mask by the sum of it's components.
            //blendWeights = blendWeights / (blendWeights.x + blendWeights.z);

            //// Sharpen blend weight completely by rounding (currently causing black spots between blends).
            //blendWeights = round(blendWeights);

            //// Blend together all three samples based on the blend mask.
            //half3 color = xDiff * blendWeights.x + yDiff * blendWeights.y + zDiff * blendWeights.z;

            //// Automatically texture terrain with cliff texture if dot product is greater than 0.8.
            //if (dot(IN.localNormal, fixed3(0, 1, 0)) >= 0.8)
            //{
            //    o.Albedo = mixedDiffuse.rgb;
            //}
            //else {
            //    o.Albedo = color.rgb;
            //}
            o.Alpha = 1;


            o.Albedo = mixedDiffuse.rgb;
            o.Alpha = weight;
        }
    ENDCG

    Category {
        Tags {
            "Queue" = "Geometry-99"
            "RenderType" = "Opaque"
            "TerrainCompatible" = "True"
        }
        // TODO: Seems like "#pragma target 3.0 _NORMALMAP" can't fallback correctly on less capable devices?
        // Use two sub-shaders to simulate different features for different targets and still fallback correctly.
        SubShader { // for sm3.0+ targets
            CGPROGRAM
                #pragma target 3.0
                #pragma multi_compile_local_fragment __ _ALPHATEST_ON
                #pragma multi_compile_local __ _NORMALMAP
            ENDCG

            UsePass "Hidden/Nature/Terrain/Utilities/PICKING"
            UsePass "Hidden/Nature/Terrain/Utilities/SELECTION"
        }
        SubShader { // for sm2.0 targets
            CGPROGRAM
            ENDCG
        }
    }

    Dependency "AddPassShader"    = "Hidden/TerrainEngine/Splatmap/Diffuse-AddPass"
    Dependency "BaseMapShader"    = "Hidden/TerrainEngine/Splatmap/Diffuse-Base"
    Dependency "BaseMapGenShader" = "Hidden/TerrainEngine/Splatmap/Diffuse-BaseGen"
    Dependency "Details0"         = "Hidden/TerrainEngine/Details/Vertexlit"
    Dependency "Details1"         = "Hidden/TerrainEngine/Details/WavingDoublePass"
    Dependency "Details2"         = "Hidden/TerrainEngine/Details/BillboardWavingDoublePass"
    Dependency "Tree0"            = "Hidden/TerrainEngine/BillboardTree"

    Fallback "Diffuse"
}
