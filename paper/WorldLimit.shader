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
    #pragma surface surf Standard fullforwardshadows
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

    UNITY_INSTANCING_BUFFER_START(Props)
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

      fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
      o.Albedo = c.rgb;
      o.Metallic = _Metallic;
      o.Smoothness = _Glossiness;
      o.Alpha = c.a;
    }
    ENDCG
  }
  FallBack "Diffuse"
}
