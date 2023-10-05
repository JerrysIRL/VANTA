// Made with Amplify Shader Editor v1.9.1.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SH_Glow"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Texture0("Texture 0", 2D) = "white" {}
		_Panner_TimeScale("Panner_TimeScale", Float) = 1
		_PannerX_01("PannerX_01", Range( 0 , 1)) = 1
		_PannerY_01("PannerY_01", Range( 0 , 1)) = 0
		_Screentone_Map("Screentone_Map", 2D) = "white" {}
		_PannerX_02("PannerX_02", Range( 0 , 1)) = 0
		_PannerY_02("PannerY_02", Range( 0 , 1)) = 1
		_Panner_StretchX("Panner_StretchX", Range( 0 , 1)) = 0.1802055
		_Panner_StretchY("Panner_StretchY", Range( 0 , 2)) = 1
		_Albedo("Albedo", Color) = (1,1,1,0)
		_Albedo_Mult("Albedo_Mult", Float) = 1
		_Screentone_Scale1("Screentone_Scale", Float) = 15
		_Contrast("Contrast", Float) = 1
		_Gradient_Int("Gradient_Int", Float) = 2
		_CameraFade_Distance("CameraFade_Distance", Float) = 1
		_CameraFade_Falloff("CameraFade_Falloff", Float) = 1
		_Opacity("Opacity", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
			float3 worldPos;
		};

		uniform float4 _Albedo;
		uniform float _Albedo_Mult;
		uniform float _Opacity;
		uniform sampler2D _Texture0;
		uniform float _Panner_StretchX;
		uniform float _Panner_StretchY;
		uniform float _Panner_TimeScale;
		uniform float _PannerX_01;
		uniform float _PannerY_01;
		uniform float _PannerX_02;
		uniform float _PannerY_02;
		uniform float _Contrast;
		uniform float _Gradient_Int;
		uniform sampler2D _Screentone_Map;
		uniform float _Screentone_Scale1;
		uniform float _CameraFade_Distance;
		uniform float _CameraFade_Falloff;
		uniform float _Cutoff = 0.5;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = ( _Albedo * _Albedo_Mult ).rgb;
			o.Alpha = _Opacity;
			float2 appendResult43 = (float2(_Panner_StretchX , _Panner_StretchY));
			float mulTime19 = _Time.y * _Panner_TimeScale;
			float2 appendResult15 = (float2(_PannerX_01 , _PannerY_01));
			float2 panner1 = ( mulTime19 * appendResult15 + i.uv_texcoord);
			float2 appendResult16 = (float2(_PannerX_02 , _PannerY_02));
			float2 panner2 = ( mulTime19 * appendResult16 + i.uv_texcoord);
			float temp_output_51_0 = ( pow( ( tex2D( _Texture0, ( appendResult43 * panner1 ) ).r + tex2D( _Texture0, ( appendResult43 * panner2 ) ).g ) , _Contrast ) * pow( ( 1.0 - i.uv_texcoord.x ) , _Gradient_Int ) );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 appendResult83 = (float2(( _Screentone_Scale1 * 2.0 ) , _Screentone_Scale1));
			float3 ase_worldPos = i.worldPos;
			float CameraDepthFade67 = saturate( pow( ( distance( ase_worldPos , _WorldSpaceCameraPos ) / _CameraFade_Distance ) , _CameraFade_Falloff ) );
			float PanningClouds21 = ( ( temp_output_51_0 + ( tex2D( _Screentone_Map, ( ase_screenPosNorm * float4( appendResult83, 0.0 , 0.0 ) ).xy ).r * temp_output_51_0 ) ) * CameraDepthFade67 );
			clip( PanningClouds21 - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19103
Node;AmplifyShaderEditor.PannerNode;1;-2945.855,-1049.939;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;2;-2941.262,-664.1652;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;5;-2394.321,-777.3987;Inherit;True;Property;_TextureSample1;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;-1;639d878678ccd17488f059e5882d14fa;639d878678ccd17488f059e5882d14fa;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-2027.351,-870.8709;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;38;-1868.796,-803.442;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-2659.418,-1198.987;Inherit;False;2;2;0;FLOAT2;0.51,5;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-2646.687,-756.6989;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-2914.574,-1283.51;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;50;-2006.817,-566.976;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;45;-2286.281,-532.5149;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;55;-1802.237,-567.7327;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-2004.33,-460.9704;Inherit;False;Property;_Gradient_Int;Gradient_Int;14;0;Create;True;0;0;0;False;0;False;2;9.58;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2029.492,-708.3295;Inherit;False;Property;_Contrast;Contrast;13;0;Create;True;0;0;0;False;0;False;1;-1.82;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;63;-2320.462,289.9503;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;64;-2094.097,356.0913;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceCameraPos;61;-2884.385,292.7552;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DistanceOpNode;62;-2569.833,205.6115;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;60;-2826.485,102.2966;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;35;-150.5573,-124.6495;Inherit;False;Property;_Albedo;Albedo;10;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;136.4426,-15.64973;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-91.5575,91.35036;Inherit;False;Property;_Albedo_Mult;Albedo_Mult;11;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;19;-3589.979,-873.8127;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-3829.586,-880.1649;Inherit;False;Property;_Panner_TimeScale;Panner_TimeScale;2;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;15;-3328,-1152;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;16;-3278.456,-587.4582;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-3630.363,-618.7948;Inherit;False;Property;_PannerX_02;PannerX_02;6;0;Create;True;0;0;0;False;0;False;0;0.065;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-3627.363,-517.6935;Inherit;False;Property;_PannerY_02;PannerY_02;7;0;Create;True;0;0;0;False;0;False;1;0.129;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;3;-2970.349,-906.0394;Inherit;True;Property;_Texture0;Texture 0;1;0;Create;True;0;0;0;False;0;False;639d878678ccd17488f059e5882d14fa;639d878678ccd17488f059e5882d14fa;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;34;408.7496,26.92368;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;SH_Glow;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;False;0;True;TransparentCutout;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;False;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.GetLocalVarNode;22;111.3903,265.7184;Inherit;False;21;PanningClouds;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;70;74.37396,182.4742;Inherit;False;Property;_Opacity;Opacity;17;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;59;-2615.761,361.5002;Inherit;False;Property;_CameraFade_Distance;CameraFade_Distance;15;0;Create;True;0;0;0;False;0;False;1;2.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;65;-2364.994,454.0913;Inherit;False;Property;_CameraFade_Falloff;CameraFade_Falloff;16;0;Create;True;0;0;0;False;0;False;1;2.38;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;66;-1887.212,359.6118;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;67;-1684.302,350.7785;Inherit;False;CameraDepthFade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-3327.852,-1393.647;Inherit;False;Property;_Panner_StretchX;Panner_StretchX;8;0;Create;True;0;0;0;False;0;False;0.1802055;0.1802055;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-3322.355,-1278.247;Inherit;False;Property;_Panner_StretchY;Panner_StretchY;9;0;Create;True;0;0;0;False;0;False;1;1.54;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-3698.372,-1107.901;Inherit;False;Property;_PannerY_01;PannerY_01;4;0;Create;True;0;0;0;False;0;False;0;0.075;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-3698.372,-1203.901;Inherit;False;Property;_PannerX_01;PannerX_01;3;0;Create;True;0;0;0;False;0;False;1;0.284;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-2405.038,-1025.396;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;71;-2091.707,-1343.789;Inherit;True;Property;_TextureSample2;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;72;-2362.736,-1343.33;Inherit;True;Property;_Texture1;Texture 1;18;0;Create;True;0;0;0;False;0;False;715679140d6e4c14aa2ed19a86107a9a;715679140d6e4c14aa2ed19a86107a9a;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TexCoordVertexDataNode;6;-3265.652,-858.8776;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexCoordVertexDataNode;76;-2520.185,-1628.317;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-2272.191,-1568.59;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-2529.273,-1481.597;Inherit;False;Property;_Screentone_Scale;Screentone_Scale;19;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-1662.369,-840.0717;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;21;-618.2047,-834.5491;Inherit;False;PanningClouds;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-872.8977,-833.8169;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;69;-1079.848,-555.521;Inherit;False;67;CameraDepthFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-1411.811,-1010.423;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;79;-1195.405,-845.6619;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;81;-3340.035,-2643.896;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-3055.081,-2553.662;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;83;-3227.888,-2419.502;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-3441.184,-2405.458;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;86;-2875.667,-2664.788;Inherit;True;Property;_Screentone_Sample;Screentone_Sample;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;87;-3633.584,-2271.559;Inherit;False;Property;_Screentone_Scale1;Screentone_Scale;12;0;Create;True;0;0;0;False;0;False;15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;88;-3297.832,-2879.698;Inherit;True;Property;_Screentone_Map;Screentone_Map;5;0;Create;True;0;0;0;False;0;False;715679140d6e4c14aa2ed19a86107a9a;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
WireConnection;1;0;6;0
WireConnection;1;2;15;0
WireConnection;1;1;19;0
WireConnection;2;0;6;0
WireConnection;2;2;16;0
WireConnection;2;1;19;0
WireConnection;5;0;3;0
WireConnection;5;1;44;0
WireConnection;12;0;4;1
WireConnection;12;1;5;2
WireConnection;38;0;12;0
WireConnection;38;1;39;0
WireConnection;40;0;43;0
WireConnection;40;1;1;0
WireConnection;44;0;43;0
WireConnection;44;1;2;0
WireConnection;43;0;41;0
WireConnection;43;1;42;0
WireConnection;50;0;45;1
WireConnection;55;0;50;0
WireConnection;55;1;53;0
WireConnection;63;0;62;0
WireConnection;63;1;59;0
WireConnection;64;0;63;0
WireConnection;64;1;65;0
WireConnection;62;0;60;0
WireConnection;62;1;61;0
WireConnection;36;0;35;0
WireConnection;36;1;37;0
WireConnection;19;0;20;0
WireConnection;15;0;14;0
WireConnection;15;1;13;0
WireConnection;16;0;18;0
WireConnection;16;1;17;0
WireConnection;34;2;36;0
WireConnection;34;9;70;0
WireConnection;34;10;22;0
WireConnection;66;0;64;0
WireConnection;67;0;66;0
WireConnection;4;0;3;0
WireConnection;4;1;40;0
WireConnection;71;0;72;0
WireConnection;71;1;77;0
WireConnection;77;0;76;0
WireConnection;77;1;78;0
WireConnection;51;0;38;0
WireConnection;51;1;55;0
WireConnection;21;0;68;0
WireConnection;68;0;79;0
WireConnection;68;1;69;0
WireConnection;73;0;86;1
WireConnection;73;1;51;0
WireConnection;79;0;51;0
WireConnection;79;1;73;0
WireConnection;82;0;81;0
WireConnection;82;1;83;0
WireConnection;83;0;84;0
WireConnection;83;1;87;0
WireConnection;84;0;87;0
WireConnection;86;0;88;0
WireConnection;86;1;82;0
ASEEND*/
//CHKSM=62849DD1B55D7E2391FD5A0304862B18E240A30D