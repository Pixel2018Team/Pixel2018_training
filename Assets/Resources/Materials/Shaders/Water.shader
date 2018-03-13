Shader "Custom/Water" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Base Texture", 2D) = "white" {}
		_NoiseTex ("Noise Texture", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_BaseSpeed("Base speed", Range(0,0.5)) = 0.1
		_NoiseScale("Noise scale", Range(0,1)) = 0.1
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
		sampler2D _NoiseTex;
		float _BaseSpeed;
		float _NoiseScale;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NoiseTex;
			float4 screenPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {

			//Calcul de la distortion de la noise texture qui sera appliquée sur main texture
			float2 uvTimeShift = IN.uv_NoiseTex + float2( -0.4, 1 ) * _Time * _BaseSpeed; //Vitesse de défilement de la texture
			float4 noise = tex2D( _NoiseTex, uvTimeShift );
			float2 uvNoisyTimeShift = IN.uv_NoiseTex + _NoiseScale * float2( noise.r, noise.g ); //quantité de noise de la texture
			float4 baseColor = tex2D( _MainTex, uvNoisyTimeShift)* _Color;

			baseColor.a = 1;

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = baseColor.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = baseColor.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
