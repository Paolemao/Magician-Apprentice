// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Yogi/ImageEffect/Occlusion"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	_DepthMap("DepthMap (RGB)", 2D) = "white" {}
	_OcclusionMap("OcclusionMap (RGB)", 2D) = "white" {}
	_Intensity("Intensity", Float) = 0.0
		_Tiling("Tiling", Vector) = (1.0, 1.0, 0.0, 0.0)
	}

		SubShader
	{
		Pass
	{
		ZTest Always
		ZWrite Off
		Cull Off
		Fog{ Mode Off }

		CGPROGRAM
#include "UnityCG.cginc"  
#pragma vertex vert  
#pragma fragment frag  
#pragma fragmentoption ARB_precision_hint_fastest  

		sampler2D _MainTex;
	sampler2D _DepthMap;
	sampler2D _OcclusionMap;
	sampler2D _CameraDepthNormalsTexture;
	fixed4 _MainTex_TexelSize;
	fixed _Intensity;
	fixed _Power;
	fixed4 _Tiling;

	struct a2v
	{
		fixed4 vertex : POSITION;
		fixed2 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		fixed4 vertex : SV_POSITION;
		fixed2 uv : TEXCOORD0;
	};

	v2f vert(a2v v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;

		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 c = tex2D(_MainTex, i.uv);

	fixed4 depthMap = tex2D(_DepthMap, i.uv);
	fixed depth = DecodeFloatRG(depthMap.zw);
	fixed3 normal = DecodeViewNormalStereo(depthMap);

	fixed4 cameraDepthMap = tex2D(_CameraDepthNormalsTexture, i.uv);
	fixed cameraDepth = DecodeFloatRG(cameraDepthMap.zw);

	fixed4 o = c;
	if (depth > 0
		&& cameraDepth < depth)
	{
		fixed2 uv = i.uv * _Tiling.xy + _Tiling.zw;
		fixed3 color = tex2D(_OcclusionMap, uv);
		fixed nf = saturate(dot(normal, fixed3(0, 0, 1)));
		nf = pow(nf, _Intensity);
		o.rgb = lerp(color, c.rgb, nf);
	}

	return o;
	}

		ENDCG
	}
	}

		Fallback off
}
