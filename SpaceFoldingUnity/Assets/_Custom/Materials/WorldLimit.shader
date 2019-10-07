/* This shader limits drawing to a certain area of the world.

HOW TO USE:

Set CutPoint to the 3D coordinates of a reference point where
you want to limit the drawing.  Then set the first three elements
of CutSign to how you want to cut relative to that point:

1:  draw only geometry AHEAD of that point on that X, Y, or Z axis
0:  don't cut on this axis (draw everything)
-1: draw only geometry BEHIND that point on that X, Y, or Z axis

Then set _LineBound1 and LineBound2 to define lines in the XZ plane
to further limit the drawing.  The four elements of each of these
vectors should be two points on the line: (X1, Z1, X2, Z2).
Finally, use the last element of _CutSign to control how drawing is
affected by these lines: +1 means draw to the right of _LineBound1
and to the left of _LineBound2, while -1 means the opposite, and a
zero here means not to cut by the line bounds at all.
*/
Shader "Custom/WorldLimit" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_CutPoint ("CutPoint", Vector) = (0, 0, 0, 0)
		_LineBound1 ("LineBound1", Vector) = (0,0,10,10)
		_LineBound2 ("LineBound2", Vector) = (0,0,10,10)
		_CutSign ("CutSign", Vector) = (1, 0, 1, 0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float3 _CutPoint;
		float4 _LineBound1;
		float4 _LineBound2;
		float4 _CutSign;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float3 axisClip = (IN.worldPos - _CutPoint) * _CutSign * _CutSign.w;
			float4 line1Clip = 
				((IN.worldPos.x - _LineBound1[0]) * (_LineBound1[3] - _LineBound1[1])
			   - (IN.worldPos.z - _LineBound1[1]) * (_LineBound1[2] - _LineBound1[0]));
			float4 line2Clip = 
				((IN.worldPos.z - _LineBound2[1]) * (_LineBound2[2] - _LineBound2[0])
			   - (IN.worldPos.x - _LineBound2[0]) * (_LineBound2[3] - _LineBound2[1]));
			   
			clip(_CutSign.w * max(axisClip, max(line2Clip, line1Clip)));

			
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
