// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:1,stmr:255,stmw:255,stcp:2,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:34655,y:32668,varname:node_9361,prsc:2|emission-8337-OUT;n:type:ShaderForge.SFN_Clamp,id:8337,x:34368,y:32768,varname:node_8337,prsc:2|IN-2045-OUT,MIN-3538-OUT,MAX-1156-OUT;n:type:ShaderForge.SFN_Vector1,id:3538,x:33986,y:32857,varname:node_3538,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:1156,x:34036,y:32936,ptovrint:False,ptlb:mainClamp,ptin:_mainClamp,varname:node_1156,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:5585,x:33448,y:32953,ptovrint:False,ptlb:UnderCloud1,ptin:_UnderCloud1,varname:node_5585,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1ae3a150ac829c34b81cb9ba741e496c,ntxv:2,isnm:False|UVIN-4801-OUT;n:type:ShaderForge.SFN_Panner,id:2579,x:33063,y:33049,varname:node_2579,prsc:2,spu:-0.1,spv:0|UVIN-1368-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1368,x:32905,y:33049,varname:node_1368,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:3452,x:34144,y:33097,varname:node_3452,prsc:2|A-9304-RGB,B-466-OUT,C-9304-RGB,D-9724-RGB;n:type:ShaderForge.SFN_Multiply,id:4801,x:33219,y:33049,varname:node_4801,prsc:2|A-2579-UVOUT,B-4515-OUT;n:type:ShaderForge.SFN_Vector1,id:4515,x:33063,y:33203,varname:node_4515,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Color,id:3614,x:33779,y:32605,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_3614,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.02844828,c2:0.2426471,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:2045,x:34056,y:32710,varname:node_2045,prsc:2|A-3614-RGB,B-3452-OUT;n:type:ShaderForge.SFN_Tex2d,id:8551,x:33234,y:33300,ptovrint:False,ptlb:Sparkle,ptin:_Sparkle,varname:node_8551,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0e5728f4000959140a6bda238c7b1874,ntxv:2,isnm:False|UVIN-9011-UVOUT;n:type:ShaderForge.SFN_Panner,id:9011,x:33006,y:33307,varname:node_9011,prsc:2,spu:-0.2,spv:0|UVIN-2566-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2566,x:32826,y:33307,varname:node_2566,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:466,x:33643,y:32835,varname:node_466,prsc:2|A-5585-RGB,B-4357-OUT;n:type:ShaderForge.SFN_Tex2d,id:635,x:33234,y:33539,ptovrint:False,ptlb:SparkkleMask,ptin:_SparkkleMask,varname:node_635,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:caf02fecc965a454d96ee52a1a8df9ce,ntxv:0,isnm:False|UVIN-7978-UVOUT;n:type:ShaderForge.SFN_Panner,id:7978,x:33069,y:33539,varname:node_7978,prsc:2,spu:-0.4,spv:0|UVIN-4913-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4913,x:32903,y:33532,varname:node_4913,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:4357,x:33478,y:33378,varname:node_4357,prsc:2|A-8551-RGB,B-635-RGB;n:type:ShaderForge.SFN_Tex2d,id:9304,x:33950,y:33187,ptovrint:False,ptlb:Wobble,ptin:_Wobble,varname:node_9304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:53ba9e673dff167469430259add8b6fe,ntxv:0,isnm:False|UVIN-7611-OUT;n:type:ShaderForge.SFN_Panner,id:4506,x:33696,y:33187,varname:node_4506,prsc:2,spu:-0.1,spv:0|UVIN-9077-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9077,x:33478,y:33187,varname:node_9077,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:7611,x:33831,y:33524,varname:node_7611,prsc:2|A-4506-UVOUT,B-635-R,T-7784-OUT;n:type:ShaderForge.SFN_Vector1,id:7784,x:33561,y:33606,varname:node_7784,prsc:2,v1:0.05;n:type:ShaderForge.SFN_Tex2d,id:9724,x:34196,y:33388,ptovrint:False,ptlb:BarMask,ptin:_BarMask,varname:node_9724,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:029a4a83a38883f4e8330831daf2b6e4,ntxv:0,isnm:False;proporder:1156-5585-3614-8551-635-9304-9724;pass:END;sub:END;*/

Shader "Shader Forge/HealthTop" {
    Properties {
		_mainClamp ("mainClamp", Float ) = 1
        _UnderCloud1 ("UnderCloud1", 2D) = "black" {}
        _Colour ("Colour", Color) = (0.02844828,0.2426471,0,1)
        _Sparkle ("Sparkle", 2D) = "black" {}
        _SparkkleMask ("SparkkleMask", 2D) = "white" {}
        _Wobble ("Wobble", 2D) = "white" {}
        _BarMask ("BarMask", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)

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
		
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
		  Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _mainClamp;
            uniform sampler2D _UnderCloud1; uniform float4 _UnderCloud1_ST;
            uniform float4 _Colour;
            uniform sampler2D _Sparkle; uniform float4 _Sparkle_ST;
            uniform sampler2D _SparkkleMask; uniform float4 _SparkkleMask_ST;
            uniform sampler2D _Wobble; uniform float4 _Wobble_ST;
            uniform sampler2D _BarMask; uniform float4 _BarMask_ST;
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
                float4 node_4534 = _Time + _TimeEditor;
                float2 node_7978 = (i.uv0+node_4534.g*float2(-0.4,0));
                float4 _SparkkleMask_var = tex2D(_SparkkleMask,TRANSFORM_TEX(node_7978, _SparkkleMask));
                float2 node_7611 = lerp((i.uv0+node_4534.g*float2(-0.1,0)),float2(_SparkkleMask_var.r,_SparkkleMask_var.r),0.05);
                float4 _Wobble_var = tex2D(_Wobble,TRANSFORM_TEX(node_7611, _Wobble));
                float2 node_4801 = ((i.uv0+node_4534.g*float2(-0.1,0))*0.2);
                float4 _UnderCloud1_var = tex2D(_UnderCloud1,TRANSFORM_TEX(node_4801, _UnderCloud1));
                float2 node_9011 = (i.uv0+node_4534.g*float2(-0.2,0));
                float4 _Sparkle_var = tex2D(_Sparkle,TRANSFORM_TEX(node_9011, _Sparkle));
                float4 _BarMask_var = tex2D(_BarMask,TRANSFORM_TEX(i.uv0, _BarMask));
                float3 emissive = clamp((_Colour.rgb*(_Wobble_var.rgb+(_UnderCloud1_var.rgb+(_Sparkle_var.rgb*_SparkkleMask_var.rgb))+_Wobble_var.rgb+_BarMask_var.rgb)),0.0,_mainClamp);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
