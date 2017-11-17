// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33569,y:32766,varname:node_9361,prsc:2|emission-8558-OUT,clip-3229-A;n:type:ShaderForge.SFN_Tex2d,id:3229,x:32989,y:32734,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3229,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:62dc8c1a88798b4488fb2ed21dfb9fc8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:8558,x:33262,y:32804,varname:node_8558,prsc:2|A-3229-RGB,B-2956-OUT;n:type:ShaderForge.SFN_Tex2d,id:7357,x:32557,y:32930,ptovrint:False,ptlb:Clouds,ptin:_Clouds,varname:node_7357,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7414-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9095,x:32557,y:33111,ptovrint:False,ptlb:Sparkles,ptin:_Sparkles,varname:node_9095,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3270-UVOUT;n:type:ShaderForge.SFN_Panner,id:3270,x:32394,y:33140,varname:node_3270,prsc:2,spu:0,spv:-0.2|UVIN-6596-UVOUT;n:type:ShaderForge.SFN_Panner,id:7414,x:32394,y:32951,varname:node_7414,prsc:2,spu:0,spv:-0.2|UVIN-5123-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6596,x:32232,y:33140,varname:node_6596,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_TexCoord,id:5123,x:32232,y:32951,varname:node_5123,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:7375,x:32989,y:33083,varname:node_7375,prsc:2|A-1353-OUT,B-6989-OUT;n:type:ShaderForge.SFN_Multiply,id:7485,x:32989,y:32966,varname:node_7485,prsc:2|A-309-OUT,B-1345-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6989,x:32822,y:33220,ptovrint:False,ptlb:SparkleAmp,ptin:_SparkleAmp,varname:node_6989,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.95;n:type:ShaderForge.SFN_ValueProperty,id:309,x:32822,y:32927,ptovrint:False,ptlb:CloudAmp,ptin:_CloudAmp,varname:node_309,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:1353,x:32822,y:33083,varname:node_1353,prsc:2|A-9095-RGB,B-8714-RGB;n:type:ShaderForge.SFN_Multiply,id:1345,x:32822,y:32966,varname:node_1345,prsc:2|A-7357-RGB,B-4175-RGB;n:type:ShaderForge.SFN_Color,id:4175,x:32685,y:32788,ptovrint:False,ptlb:CloudColour,ptin:_CloudColour,varname:node_4175,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:8714,x:32675,y:33220,ptovrint:False,ptlb:SparkleColour,ptin:_SparkleColour,varname:node_8714,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:3651,x:33159,y:33019,varname:node_3651,prsc:2|A-7485-OUT,B-7375-OUT;n:type:ShaderForge.SFN_Multiply,id:2956,x:33324,y:33067,varname:node_2956,prsc:2|A-3651-OUT,B-457-RGB;n:type:ShaderForge.SFN_Tex2d,id:457,x:33171,y:33185,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_457,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:044e99c5bdad5d24eb30d96da04860bc,ntxv:0,isnm:False;proporder:3229-7357-9095-6989-309-4175-8714-457;pass:END;sub:END;*/

Shader "Shader Forge/ButtonSHD" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Clouds ("Clouds", 2D) = "white" {}
        _Sparkles ("Sparkles", 2D) = "white" {}
        _SparkleAmp ("SparkleAmp", Float ) = 1.95
        _CloudAmp ("CloudAmp", Float ) = 1
        _CloudColour ("CloudColour", Color) = (0.5,0.5,0.5,1)
        _SparkleColour ("SparkleColour", Color) = (0.5,0.5,0.5,1)
        _Mask ("Mask", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
		
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
		
		 Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
		
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Clouds; uniform float4 _Clouds_ST;
            uniform sampler2D _Sparkles; uniform float4 _Sparkles_ST;
            uniform float _SparkleAmp;
            uniform float _CloudAmp;
            uniform float4 _CloudColour;
            uniform float4 _SparkleColour;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                clip(_MainTex_var.a - 0.5);
////// Lighting:
////// Emissive:
                float4 node_590 = _Time + _TimeEditor;
                float2 node_7414 = (i.uv0+node_590.g*float2(0,-0.2));
                float4 _Clouds_var = tex2D(_Clouds,TRANSFORM_TEX(node_7414, _Clouds));
                float2 node_3270 = (i.uv0+node_590.g*float2(0,-0.2));
                float4 _Sparkles_var = tex2D(_Sparkles,TRANSFORM_TEX(node_3270, _Sparkles));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float3 emissive = (_MainTex_var.rgb+(((_CloudAmp*(_Clouds_var.rgb*_CloudColour.rgb))+((_Sparkles_var.rgb*_SparkleColour.rgb)*_SparkleAmp))*_Mask_var.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                clip(_MainTex_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
