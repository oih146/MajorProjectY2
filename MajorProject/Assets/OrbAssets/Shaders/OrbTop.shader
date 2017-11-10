// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:34471,y:32826,varname:node_9361,prsc:2|emission-9352-OUT;n:type:ShaderForge.SFN_Tex2d,id:5094,x:32956,y:33391,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5094,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:db335b2b66273c242aefefab29070326,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8258,x:32956,y:33040,ptovrint:False,ptlb:WobblyBars,ptin:_WobblyBars,varname:node_8258,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:53ba9e673dff167469430259add8b6fe,ntxv:2,isnm:False|UVIN-7842-UVOUT;n:type:ShaderForge.SFN_Panner,id:7842,x:32781,y:33040,varname:node_7842,prsc:2,spu:-0.1,spv:0|UVIN-3477-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3477,x:32612,y:33040,varname:node_3477,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:2084,x:32956,y:33201,ptovrint:False,ptlb:WobblyBars2,ptin:_WobblyBars2,varname:node_2084,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:53ba9e673dff167469430259add8b6fe,ntxv:2,isnm:False|UVIN-5141-OUT;n:type:ShaderForge.SFN_Tex2d,id:5750,x:32447,y:33201,ptovrint:False,ptlb:WobbleMask1,ptin:_WobbleMask1,varname:node_5750,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:caf02fecc965a454d96ee52a1a8df9ce,ntxv:2,isnm:False|UVIN-2875-UVOUT;n:type:ShaderForge.SFN_Panner,id:2875,x:32267,y:33201,varname:node_2875,prsc:2,spu:-0.2,spv:0|UVIN-6952-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6952,x:32050,y:33201,varname:node_6952,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:5141,x:32781,y:33201,varname:node_5141,prsc:2|A-6678-UVOUT,B-5750-R,T-9590-OUT;n:type:ShaderForge.SFN_TexCoord,id:6678,x:32596,y:33201,varname:node_6678,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:9590,x:32423,y:33409,ptovrint:False,ptlb:WobbleWibbler,ptin:_WobbleWibbler,varname:node_9590,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Add,id:1019,x:33245,y:33008,varname:node_1019,prsc:2|A-8258-RGB,B-4410-OUT;n:type:ShaderForge.SFN_Multiply,id:16,x:33219,y:33318,varname:node_16,prsc:2|A-2084-RGB,B-5094-RGB;n:type:ShaderForge.SFN_Multiply,id:4410,x:33291,y:33215,varname:node_4410,prsc:2|A-8258-RGB,B-16-OUT;n:type:ShaderForge.SFN_Tex2d,id:5667,x:33137,y:32801,ptovrint:False,ptlb:SolidBar,ptin:_SolidBar,varname:node_5667,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9b662080e9b51e14792b82556e1ef868,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Add,id:1556,x:33500,y:32919,varname:node_1556,prsc:2|A-5667-RGB,B-1019-OUT,C-916-OUT;n:type:ShaderForge.SFN_Clamp,id:1482,x:33973,y:32933,varname:node_1482,prsc:2|IN-1556-OUT,MIN-3484-OUT,MAX-3161-OUT;n:type:ShaderForge.SFN_Vector1,id:3484,x:33827,y:32849,varname:node_3484,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:3161,x:33782,y:33001,varname:node_3161,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:7139,x:32665,y:32638,ptovrint:False,ptlb:Wobbly3,ptin:_Wobbly3,varname:node_7139,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:53ba9e673dff167469430259add8b6fe,ntxv:0,isnm:False|UVIN-425-UVOUT;n:type:ShaderForge.SFN_Panner,id:425,x:32420,y:32638,varname:node_425,prsc:2,spu:0.1,spv:0|UVIN-1638-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1638,x:32264,y:32680,varname:node_1638,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:916,x:32867,y:32603,varname:node_916,prsc:2|A-7139-RGB,B-158-OUT;n:type:ShaderForge.SFN_ValueProperty,id:158,x:32738,y:32528,ptovrint:False,ptlb:WobbleAdj1,ptin:_WobbleAdj1,varname:node_158,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:9352,x:34242,y:32915,varname:node_9352,prsc:2|A-5068-RGB,B-1482-OUT;n:type:ShaderForge.SFN_Color,id:5068,x:34022,y:32787,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_5068,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;proporder:5094-8258-2084-5750-9590-5667-7139-158-5068;pass:END;sub:END;*/

Shader "Shader Forge/OrbTop" {
    Properties {
        _MainTex ("MainTex", 2D) = "black" {}
        _WobblyBars ("WobblyBars", 2D) = "black" {}
        _WobblyBars2 ("WobblyBars2", 2D) = "black" {}
        _WobbleMask1 ("WobbleMask1", 2D) = "black" {}
        _WobbleWibbler ("WobbleWibbler", Range(0, 1)) = 0.5
        _SolidBar ("SolidBar", 2D) = "black" {}
        _Wobbly3 ("Wobbly3", 2D) = "white" {}
        _WobbleAdj1 ("WobbleAdj1", Float ) = 0
        _Colour ("Colour", Color) = (1,1,1,1)
    
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
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
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
            Blend One One
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
            uniform sampler2D _WobblyBars; uniform float4 _WobblyBars_ST;
            uniform sampler2D _WobblyBars2; uniform float4 _WobblyBars2_ST;
            uniform sampler2D _WobbleMask1; uniform float4 _WobbleMask1_ST;
            uniform float _WobbleWibbler;
            uniform sampler2D _SolidBar; uniform float4 _SolidBar_ST;
            uniform sampler2D _Wobbly3; uniform float4 _Wobbly3_ST;
            uniform float _WobbleAdj1;
            uniform float4 _Colour;
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
                float4 _SolidBar_var = tex2D(_SolidBar,TRANSFORM_TEX(i.uv0, _SolidBar));
                float4 node_9766 = _Time + _TimeEditor;
                float2 node_7842 = (i.uv0+node_9766.g*float2(-0.1,0));
                float4 _WobblyBars_var = tex2D(_WobblyBars,TRANSFORM_TEX(node_7842, _WobblyBars));
                float2 node_2875 = (i.uv0+node_9766.g*float2(-0.2,0));
                float4 _WobbleMask1_var = tex2D(_WobbleMask1,TRANSFORM_TEX(node_2875, _WobbleMask1));
                float2 node_5141 = lerp(i.uv0,float2(_WobbleMask1_var.r,_WobbleMask1_var.r),_WobbleWibbler);
                float4 _WobblyBars2_var = tex2D(_WobblyBars2,TRANSFORM_TEX(node_5141, _WobblyBars2));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_425 = (i.uv0+node_9766.g*float2(0.1,0));
                float4 _Wobbly3_var = tex2D(_Wobbly3,TRANSFORM_TEX(node_425, _Wobbly3));
                float3 emissive = (_Colour.rgb*clamp((_SolidBar_var.rgb+(_WobblyBars_var.rgb+(_WobblyBars_var.rgb*(_WobblyBars2_var.rgb*_MainTex_var.rgb)))+(_Wobbly3_var.rgb*_WobbleAdj1)),0.0,1.0));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
