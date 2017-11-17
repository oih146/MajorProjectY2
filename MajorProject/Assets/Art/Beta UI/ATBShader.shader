// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:34208,y:32674,varname:node_9361,prsc:2|emission-5102-OUT,alpha-1162-A;n:type:ShaderForge.SFN_Tex2d,id:1162,x:33467,y:32868,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1162,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:769cfe11d144dad4fa7acc82ca0cde6d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1334,x:32565,y:32645,ptovrint:False,ptlb:Clouds,ptin:_Clouds,varname:node_1334,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:caf02fecc965a454d96ee52a1a8df9ce,ntxv:2,isnm:False|UVIN-1672-OUT;n:type:ShaderForge.SFN_Multiply,id:3151,x:33419,y:32719,varname:node_3151,prsc:2|A-1391-OUT,B-5880-RGB;n:type:ShaderForge.SFN_Add,id:631,x:33683,y:32770,varname:node_631,prsc:2|A-3151-OUT,B-1162-RGB;n:type:ShaderForge.SFN_Panner,id:8476,x:32160,y:32647,varname:node_8476,prsc:2,spu:-0.1,spv:0.1|UVIN-8816-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8816,x:31980,y:32647,varname:node_8816,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:4651,x:32565,y:32461,ptovrint:False,ptlb:Clouds2,ptin:_Clouds2,varname:_node_1334_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:caf02fecc965a454d96ee52a1a8df9ce,ntxv:2,isnm:False|UVIN-3048-OUT;n:type:ShaderForge.SFN_Panner,id:2521,x:32160,y:32463,varname:node_2521,prsc:2,spu:-0.2,spv:-0.1|UVIN-9988-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9988,x:31980,y:32463,varname:node_9988,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:5011,x:32763,y:32445,varname:node_5011,prsc:2|A-8636-OUT,B-4651-RGB;n:type:ShaderForge.SFN_Vector1,id:8636,x:32664,y:32377,varname:node_8636,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:1170,x:32907,y:32573,varname:node_1170,prsc:2|A-5011-OUT,B-1334-RGB,C-6570-RGB,D-9876-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:1391,x:33259,y:32573,varname:node_1391,prsc:2,a:0,b:0.1|IN-5585-OUT;n:type:ShaderForge.SFN_Power,id:5585,x:33083,y:32573,varname:node_5585,prsc:2|VAL-1170-OUT,EXP-6436-OUT;n:type:ShaderForge.SFN_Vector1,id:6436,x:32924,y:32360,varname:node_6436,prsc:2,v1:3;n:type:ShaderForge.SFN_Tex2d,id:5880,x:32873,y:32794,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_5880,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:19a659980e0f7664a915456a5387a156,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6570,x:32565,y:32250,ptovrint:False,ptlb:Sparkles,ptin:_Sparkles,varname:node_6570,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0e5728f4000959140a6bda238c7b1874,ntxv:2,isnm:False|UVIN-6856-UVOUT;n:type:ShaderForge.SFN_Panner,id:6856,x:32402,y:32220,varname:node_6856,prsc:2,spu:-0.3,spv:0|UVIN-1920-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1920,x:32233,y:32220,varname:node_1920,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector3,id:9876,x:32740,y:32696,varname:node_9876,prsc:2,v1:0.75,v2:0.75,v3:0.75;n:type:ShaderForge.SFN_Clamp01,id:5102,x:33873,y:32770,varname:node_5102,prsc:2|IN-631-OUT;n:type:ShaderForge.SFN_Multiply,id:1672,x:32402,y:32856,varname:node_1672,prsc:2|A-8476-UVOUT,B-8317-OUT;n:type:ShaderForge.SFN_Multiply,id:3048,x:32416,y:32502,varname:node_3048,prsc:2|A-2521-UVOUT,B-4065-OUT;n:type:ShaderForge.SFN_Vector2,id:8317,x:32233,y:32895,varname:node_8317,prsc:2,v1:2,v2:0.1;n:type:ShaderForge.SFN_Vector2,id:4065,x:32270,y:32580,varname:node_4065,prsc:2,v1:2,v2:0.1;proporder:1162-1334-4651-5880-6570;pass:END;sub:END;*/

Shader "Shader Forge/ATBShader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Clouds ("Clouds", 2D) = "black" {}
        _Clouds2 ("Clouds2", 2D) = "black" {}
        _Mask ("Mask", 2D) = "black" {}
        _Sparkles ("Sparkles", 2D) = "black" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Clouds; uniform float4 _Clouds_ST;
            uniform sampler2D _Clouds2; uniform float4 _Clouds2_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _Sparkles; uniform float4 _Sparkles_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_8974 = _Time + _TimeEditor;
                float2 node_3048 = ((i.uv0+node_8974.g*float2(-0.2,-0.1))*float2(2,0.1));
                float4 _Clouds2_var = tex2D(_Clouds2,TRANSFORM_TEX(node_3048, _Clouds2));
                float2 node_1672 = ((i.uv0+node_8974.g*float2(-0.1,0.1))*float2(2,0.1));
                float4 _Clouds_var = tex2D(_Clouds,TRANSFORM_TEX(node_1672, _Clouds));
                float2 node_6856 = (i.uv0+node_8974.g*float2(-0.3,0));
                float4 _Sparkles_var = tex2D(_Sparkles,TRANSFORM_TEX(node_6856, _Sparkles));
                float3 node_1170 = ((0.5*_Clouds2_var.rgb)+_Clouds_var.rgb+_Sparkles_var.rgb+float3(0.75,0.75,0.75));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = saturate(((lerp(0,0.1,pow(node_1170,3.0))*_Mask_var.rgb)+_MainTex_var.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,_MainTex_var.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
