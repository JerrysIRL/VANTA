// Made with Amplify Shader Editor v1.9.1.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Master"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,0)
		_Screentone_Color("Screentone_Color", Color) = (1,1,1,0)
		_Outline_Color("Outline_Color", Color) = (0.254717,0.254717,0.254717,0)
		_LightGradient_Inner("LightGradient_Inner", Range( 0 , 10)) = 0.25
		_LightGradient_Middle("LightGradient_Middle", Range( 0 , 10)) = 0.5
		_Screentone_Map("Screentone_Map", 2D) = "white" {}
		_AlbedoPattern_map("AlbedoPattern_map", 2D) = "white" {}
		_Emissive_Map("Emissive_Map", 2D) = "white" {}
		_AlphaMask_Map("AlphaMask_Map", 2D) = "white" {}
		_OpacityPattern_Map("OpacityPattern_Map", 2D) = "white" {}
		_Albedo_Scale("Albedo_Scale", Float) = 1
		_Screentone_Scale("Screentone_Scale", Float) = 15
		_Screentone_Fade("Screentone_Fade", Range( 0 , 1)) = 0
		_Pattern_Fade("Pattern_Fade", Range( 0 , 1)) = 0
		_Emissive_Fade("Emissive_Fade", Range( 0 , 1)) = 0
		_OutlineWidth("OutlineWidth", Range( 0 , 0.1)) = 0.02
		_OutlineDIstance("OutlineDIstance", Float) = 2
		_OpacityScreentone_Scale("OpacityScreentone_Scale", Float) = 15
		_Opacity_Fade("Opacity_Fade", Range( 0 , 1)) = 0
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline nofog  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float4 unityObjectToClipPos209 = UnityObjectToClipPos( ase_vertex3Pos );
			float Opacity_Fade271 = _Opacity_Fade;
			float3 outlineVar = ( ( ase_vertexNormal * _OutlineWidth * min( unityObjectToClipPos209.w , _OutlineDIstance ) ) + ( Opacity_Fade271 * 10000.0 ) );
			v.vertex.xyz += outlineVar;
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _Outline_Color.rgb;
		}
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldNormal;
			float3 worldPos;
			float2 uv_texcoord;
			float4 screenPos;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float _LightGradient_Inner;
		uniform sampler2D _AlbedoPattern_map;
		uniform float _Albedo_Scale;
		uniform float _Pattern_Fade;
		uniform float4 _Color;
		uniform float _LightGradient_Middle;
		uniform sampler2D _Screentone_Map;
		uniform float _Screentone_Scale;
		uniform float4 _Screentone_Color;
		uniform float _Screentone_Fade;
		uniform sampler2D _Emissive_Map;
		uniform float4 _Emissive_Map_ST;
		uniform float _Emissive_Fade;
		uniform sampler2D _OpacityPattern_Map;
		uniform float _OpacityScreentone_Scale;
		uniform float _Opacity_Fade;
		uniform sampler2D _AlphaMask_Map;
		uniform float4 _AlphaMask_Map_ST;
		uniform float _Cutoff = 0.5;
		uniform float4 _Outline_Color;
		uniform float _OutlineWidth;
		uniform float _OutlineDIstance;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 Outline215 = 0;
			v.vertex.xyz += Outline215;
			v.vertex.w = 1;
		}

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 appendResult264 = (float2(( _OpacityScreentone_Scale * 2.0 ) , _OpacityScreentone_Scale));
			float2 uv_AlphaMask_Map = i.uv_texcoord * _AlphaMask_Map_ST.xy + _AlphaMask_Map_ST.zw;
			float Opacity267 = ( pow( tex2D( _OpacityPattern_Map, ( ase_screenPosNorm * float4( appendResult264, 0.0 , 0.0 ) ).xy ).r , ( _Opacity_Fade * 2.0 ) ) * tex2D( _AlphaMask_Map, uv_AlphaMask_Map ).r );
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = Unity_SafeNormalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult1 = dot( ase_normWorldNormal , ase_worldlightDir );
			float temp_output_28_0 = min( saturate( dotResult1 ) , ase_lightAtten );
			float temp_output_3_0_g7 = ( (temp_output_28_0*_LightGradient_Inner + 0.0) - 0.1 );
			float temp_output_83_0 = saturate( ( temp_output_3_0_g7 / fwidth( temp_output_3_0_g7 ) ) );
			float temp_output_146_0 = ( 1.0 - ( ( 1.0 - tex2D( _AlbedoPattern_map, ( i.uv_texcoord * _Albedo_Scale ) ).r ) * _Pattern_Fade ) );
			float temp_output_3_0_g6 = ( (temp_output_28_0*_LightGradient_Middle + 0.0) - 0.1 );
			float temp_output_81_0 = saturate( ( temp_output_3_0_g6 / fwidth( temp_output_3_0_g6 ) ) );
			float2 appendResult99 = (float2(( _Screentone_Scale * 2.0 ) , _Screentone_Scale));
			float4 temp_output_38_0 = ( tex2D( _Screentone_Map, ( ase_screenPosNorm * float4( appendResult99, 0.0 , 0.0 ) ).xy ) * _Screentone_Color );
			float4 temp_output_119_0 = ( temp_output_146_0 * ( _Color * 0.5 ) );
			float4 lerpResult253 = lerp( ( temp_output_38_0 + temp_output_119_0 ) , temp_output_119_0 , _Screentone_Fade);
			float temp_output_3_0_g8 = ( (temp_output_28_0*1000.0 + 0.0) - 0.1 );
			float4 lerpResult251 = lerp( temp_output_38_0 , ( temp_output_146_0 * ( _Color * 0.2 ) ) , _Screentone_Fade);
			float4 diffuse41 = ( ( temp_output_83_0 * ( temp_output_146_0 * _Color ) ) + ( ( temp_output_81_0 - temp_output_83_0 ) * lerpResult253 ) + ( ( saturate( ( temp_output_3_0_g8 / fwidth( temp_output_3_0_g8 ) ) ) - temp_output_81_0 ) * lerpResult251 ) );
			c.rgb = diffuse41.rgb;
			c.a = 1;
			clip( Opacity267 - _Cutoff );
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = Unity_SafeNormalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult1 = dot( ase_normWorldNormal , ase_worldlightDir );
			float temp_output_28_0 = min( saturate( dotResult1 ) , 1 );
			float temp_output_3_0_g7 = ( (temp_output_28_0*_LightGradient_Inner + 0.0) - 0.1 );
			float temp_output_83_0 = saturate( ( temp_output_3_0_g7 / fwidth( temp_output_3_0_g7 ) ) );
			float temp_output_146_0 = ( 1.0 - ( ( 1.0 - tex2D( _AlbedoPattern_map, ( i.uv_texcoord * _Albedo_Scale ) ).r ) * _Pattern_Fade ) );
			float temp_output_3_0_g6 = ( (temp_output_28_0*_LightGradient_Middle + 0.0) - 0.1 );
			float temp_output_81_0 = saturate( ( temp_output_3_0_g6 / fwidth( temp_output_3_0_g6 ) ) );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 appendResult99 = (float2(( _Screentone_Scale * 2.0 ) , _Screentone_Scale));
			float4 temp_output_38_0 = ( tex2D( _Screentone_Map, ( ase_screenPosNorm * float4( appendResult99, 0.0 , 0.0 ) ).xy ) * _Screentone_Color );
			float4 temp_output_119_0 = ( temp_output_146_0 * ( _Color * 0.5 ) );
			float4 lerpResult253 = lerp( ( temp_output_38_0 + temp_output_119_0 ) , temp_output_119_0 , _Screentone_Fade);
			float temp_output_3_0_g8 = ( (temp_output_28_0*1000.0 + 0.0) - 0.1 );
			float4 lerpResult251 = lerp( temp_output_38_0 , ( temp_output_146_0 * ( _Color * 0.2 ) ) , _Screentone_Fade);
			float4 diffuse41 = ( ( temp_output_83_0 * ( temp_output_146_0 * _Color ) ) + ( ( temp_output_81_0 - temp_output_83_0 ) * lerpResult253 ) + ( ( saturate( ( temp_output_3_0_g8 / fwidth( temp_output_3_0_g8 ) ) ) - temp_output_81_0 ) * lerpResult251 ) );
			float2 uv_Emissive_Map = i.uv_texcoord * _Emissive_Map_ST.xy + _Emissive_Map_ST.zw;
			float4 Emissive221 = ( ( diffuse41 + tex2D( _Emissive_Map, uv_Emissive_Map ) ) * _Emissive_Fade );
			o.Emission = Emissive221.rgb;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float3 worldNormal : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT( UnityGI, gi );
				o.Alpha = LightingStandardCustomLighting( o, worldViewDir, gi ).a;
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19103
Node;AmplifyShaderEditor.CommentaryNode;270;77.72702,-2481.494;Inherit;False;2078.5;896.0952;Comment;15;254;256;259;262;263;264;265;266;260;261;181;180;268;267;271;Opacity;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;223;-1773.14,-338.8452;Inherit;False;1497.389;750.882;Comment;7;185;182;221;183;184;225;227;Emissive;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;220;-741.7191,989.8921;Inherit;False;1430.125;1015.065;Comment;12;215;213;214;212;210;209;206;208;207;205;274;275;Outline;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;219;-4217.115,-1750.726;Inherit;False;3231.521;807.2803;Comment;22;81;85;83;30;80;106;107;86;87;35;82;88;117;112;118;27;28;3;2;29;1;128;Cel Shade;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;218;-4025.262,-467.6695;Inherit;False;1774.853;927.8808;Comment;18;230;229;228;187;104;149;142;40;150;124;146;123;122;119;247;248;249;277;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;217;-3701.502,-2809.368;Inherit;False;1373.604;774.1387;Screentone;9;38;98;102;99;103;108;37;186;101;Screentone;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;41;-526.9159,-1490.4;Inherit;False;diffuse;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;119;-2429.409,-127.1404;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;122;-2434.13,-325.6502;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;40;-3154.994,-60.35321;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;104;-3717.276,-417.6695;Inherit;True;Property;_Pattern_Mask;Pattern_Mask;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;187;-3975.262,-411.034;Inherit;True;Property;_AlbedoPattern_map;AlbedoPattern_map;6;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.FunctionNode;81;-2512.79,-1477.657;Inherit;False;Step Antialiasing;-1;;6;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0.1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;85;-3041.442,-1611.856;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.25;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;83;-2509.547,-1640.756;Inherit;False;Step Antialiasing;-1;;7;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0.1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;30;-2528.097,-1299.703;Inherit;False;Step Antialiasing;-1;;8;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0.1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;80;-3037.636,-1432.232;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;106;-3037.687,-1251.322;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-1377.804,-1285.088;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-1375.582,-1471.163;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;88;-1138.594,-1489.489;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;117;-1376.398,-1641.64;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;112;-1937.499,-1295.859;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;118;-1931.838,-1474.508;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;27;-3696.224,-1190.049;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMinOpNode;28;-3494.117,-1186.993;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;3;-4167.115,-1318.19;Inherit;False;True;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;2;-4163.982,-1133.324;Inherit;False;True;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LightAttenuation;29;-3753.362,-1054.446;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;1;-3862.467,-1188.989;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OutlineNode;205;117.8168,1120.915;Inherit;False;2;True;None;0;0;Front;True;True;True;True;0;False;;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;207;-162.0806,1293.521;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;208;-524.0264,1398.319;Inherit;False;Property;_OutlineWidth;OutlineWidth;15;0;Create;True;0;0;0;False;0;False;0.02;0;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;206;-427.0195,1207.285;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.UnityObjToClipPosHlpNode;209;-461.6194,1554.453;Inherit;False;1;0;FLOAT3;0,0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;210;-691.7191,1557.052;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;212;-463.5849,1784.957;Inherit;False;Property;_OutlineDIstance;OutlineDIstance;16;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMinOpNode;214;-230.7674,1693.798;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;183;-1598.352,-60.12843;Inherit;True;Property;_Emissive_Map;Emissive_Map;7;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode;182;-1347.2,-63.73756;Inherit;True;Property;_TextureSample1;Texture Sample 0;15;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;184;-779.0071,51.70445;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;221;-517.6448,97.99885;Inherit;False;Emissive;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;225;-1242.85,-176.8206;Inherit;False;41;diffuse;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;227;-985.6833,-80.65415;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;213;-183.1805,1039.892;Inherit;False;Property;_Outline_Color;Outline_Color;2;0;Create;True;0;0;0;False;0;False;0.254717,0.254717,0.254717,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;185;-1125.879,194.6359;Inherit;False;Property;_Emissive_Fade;Emissive_Fade;14;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;107;-3540.2,-1302.525;Inherit;False;Constant;_LightGradient_Outer;LightGradient_Outer;5;0;Create;True;0;0;0;False;0;False;1000;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-3541.75,-1442.154;Inherit;False;Property;_LightGradient_Middle;LightGradient_Middle;4;0;Create;True;0;0;0;False;0;False;0.5;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;86;-3541.513,-1562.256;Inherit;False;Property;_LightGradient_Inner;LightGradient_Inner;3;0;Create;True;0;0;0;False;0;False;0.25;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;228;-3996.737,28.3228;Inherit;False;Property;_Albedo_Scale;Albedo_Scale;10;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;229;-4001.489,-133.6453;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;230;-3775.489,-74.64532;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-2489.898,-2495.42;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;37;-2893.585,-2544.458;Inherit;True;Property;_Screentone_Sample;Screentone_Sample;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;186;-3315.75,-2759.368;Inherit;True;Property;_Screentone_Map;Screentone_Map;5;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RegisterLocalVarNode;215;366.4054,1134.631;Inherit;False;Outline;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;-2646.508,1.9514;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;124;-2936.701,160.2113;Inherit;False;Constant;_Float0;Float 0;13;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;247;-2643.519,166.704;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;249;-2421.836,27.1948;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;248;-2933.712,324.9639;Inherit;False;Constant;_Float1;Float 0;13;0;Create;True;0;0;0;False;0;False;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;128;-1969.028,-1709.144;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;251;-1681.976,-715.5656;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;253;-1678.258,-876.1742;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;252;-2608.866,-774.4225;Inherit;False;Property;_Screentone_Fade;Screentone_Fade;12;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;108;-2681.079,-2296.385;Inherit;False;Property;_Screentone_Color;Screentone_Color;1;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenPosInputsNode;98;-3383.168,-2523.567;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-3098.214,-2433.332;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;99;-3271.021,-2299.172;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;-3484.317,-2285.128;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;101;-3676.717,-2151.229;Inherit;False;Property;_Screentone_Scale;Screentone_Scale;11;0;Create;True;0;0;0;False;0;False;15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;269;1750.795,-103.9011;Inherit;False;267;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;60;1756.252,11.14477;Inherit;False;41;diffuse;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;216;1756.348,102.2095;Inherit;False;215;Outline;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;222;1748.855,-274.3188;Inherit;False;221;Emissive;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;254;999.985,-2431.494;Inherit;True;Property;_TextureSample2;Texture Sample 0;15;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;259;1367.11,-2161.13;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;262;529.2766,-2212.811;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;263;814.2293,-2122.575;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;264;641.4236,-1988.415;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;265;428.1275,-1974.371;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;266;127.727,-1856.472;Inherit;False;Property;_OpacityScreentone_Scale;OpacityScreentone_Scale;17;0;Create;True;0;0;0;False;0;False;15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;260;857.2208,-1994.669;Inherit;False;Property;_Opacity_Fade;Opacity_Fade;18;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;261;1180.067,-1989.505;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;181;1125.45,-1815.398;Inherit;True;Property;_TextureSample0;Texture Sample 0;15;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;180;857.5139,-1822.603;Inherit;True;Property;_AlphaMask_Map;AlphaMask_Map;8;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;268;1703.161,-2048.756;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;267;1932.227,-2047.041;Inherit;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;179;2046.049,-285.0232;Float;False;True;-1;2;ASEMaterialInspector;0;0;CustomLighting;Master;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;ForwardOnly;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;True;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;19;-1;-1;-1;0;False;0;0;False;;-1;0;False;_MaskClipValue;0;0;0;False;0.1;False;;0;False;;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.SimpleAddOpNode;276;91.26837,1347.137;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;274;-236.5237,1876.36;Inherit;False;271;Opacity_Fade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;275;-55.60461,1789.622;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;10000;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;271;1117.455,-2132.729;Inherit;False;Opacity_Fade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;256;715.3347,-2425.439;Inherit;True;Property;_OpacityPattern_Map;OpacityPattern_Map;9;0;Create;True;0;0;0;False;0;False;e81a4b8e242927344ac4a2589c71ce6e;e81a4b8e242927344ac4a2589c71ce6e;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleSubtractOpNode;146;-2921.199,-391.7285;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;150;-3129.433,-385.6707;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;142;-3142.434,-182.9874;Inherit;False;Constant;_Float2;Float 2;14;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;149;-3430.839,-195.2152;Inherit;False;Property;_Pattern_Fade;Pattern_Fade;13;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;277;-3350.686,-390.8131;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
WireConnection;41;0;88;0
WireConnection;119;0;146;0
WireConnection;119;1;123;0
WireConnection;122;0;146;0
WireConnection;122;1;40;0
WireConnection;104;0;187;0
WireConnection;104;1;230;0
WireConnection;81;2;80;0
WireConnection;85;0;28;0
WireConnection;85;1;86;0
WireConnection;83;2;85;0
WireConnection;30;2;106;0
WireConnection;80;0;28;0
WireConnection;80;1;87;0
WireConnection;106;0;28;0
WireConnection;106;1;107;0
WireConnection;35;0;112;0
WireConnection;35;1;251;0
WireConnection;82;0;118;0
WireConnection;82;1;253;0
WireConnection;88;0;117;0
WireConnection;88;1;82;0
WireConnection;88;2;35;0
WireConnection;117;0;83;0
WireConnection;117;1;122;0
WireConnection;112;0;30;0
WireConnection;112;1;81;0
WireConnection;118;0;81;0
WireConnection;118;1;83;0
WireConnection;27;0;1;0
WireConnection;28;0;27;0
WireConnection;28;1;29;0
WireConnection;1;0;3;0
WireConnection;1;1;2;0
WireConnection;205;0;213;0
WireConnection;205;1;276;0
WireConnection;207;0;206;0
WireConnection;207;1;208;0
WireConnection;207;2;214;0
WireConnection;209;0;210;0
WireConnection;214;0;209;4
WireConnection;214;1;212;0
WireConnection;182;0;183;0
WireConnection;184;0;227;0
WireConnection;184;1;185;0
WireConnection;221;0;184;0
WireConnection;227;0;225;0
WireConnection;227;1;182;0
WireConnection;230;0;229;0
WireConnection;230;1;228;0
WireConnection;38;0;37;0
WireConnection;38;1;108;0
WireConnection;37;0;186;0
WireConnection;37;1;102;0
WireConnection;215;0;205;0
WireConnection;123;0;40;0
WireConnection;123;1;124;0
WireConnection;247;0;40;0
WireConnection;247;1;248;0
WireConnection;249;0;146;0
WireConnection;249;1;247;0
WireConnection;128;0;38;0
WireConnection;128;1;119;0
WireConnection;251;0;38;0
WireConnection;251;1;249;0
WireConnection;251;2;252;0
WireConnection;253;0;128;0
WireConnection;253;1;119;0
WireConnection;253;2;252;0
WireConnection;102;0;98;0
WireConnection;102;1;99;0
WireConnection;99;0;103;0
WireConnection;99;1;101;0
WireConnection;103;0;101;0
WireConnection;254;0;256;0
WireConnection;254;1;263;0
WireConnection;259;0;254;1
WireConnection;259;1;261;0
WireConnection;263;0;262;0
WireConnection;263;1;264;0
WireConnection;264;0;265;0
WireConnection;264;1;266;0
WireConnection;265;0;266;0
WireConnection;261;0;260;0
WireConnection;181;0;180;0
WireConnection;268;0;259;0
WireConnection;268;1;181;1
WireConnection;267;0;268;0
WireConnection;179;2;222;0
WireConnection;179;10;269;0
WireConnection;179;13;60;0
WireConnection;179;11;216;0
WireConnection;276;0;207;0
WireConnection;276;1;275;0
WireConnection;275;0;274;0
WireConnection;271;0;260;0
WireConnection;146;0;142;0
WireConnection;146;1;150;0
WireConnection;150;0;277;0
WireConnection;150;1;149;0
WireConnection;277;0;104;1
ASEEND*/
//CHKSM=EEADB2FC62E9A175ED658FFC143989D607D814B8