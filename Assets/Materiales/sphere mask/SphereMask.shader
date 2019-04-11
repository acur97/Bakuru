Shader "Custom/SphereMask"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color2 ("Color2", Color) = (1,1,1,1)
		_MainTex2 ("Albedo2 (RGB)", 2D) = "white" {}
        _BumpMap ("Bumpmap", 2D) = "bump" {}
		_ColorStrength ("Color Strength", Range(0,5)) = 1
		_EmissionColor ("Emission Color", Color) = (1,1,1,1)
		_EmissionTex ("Emission (RGB)", 2D) = "white" {}
		_EmissionStrength ("Emission Strength", Range(0,50)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_Position ("Position", Vector) = (0,0,0,0)
		_Radius ("Radius", Range(0,100)) = 0
		_Softness ("Sphere Softness", Range(0,100)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        //Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
		//#pragma surface surf Lambert alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _EmissionTex;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv_EmissionTex;
			float3 worldPos;
            float2 uv_BumpMap;
        };

        half _Glossiness;
        half _Metallic;
		half _ColorStrength;
		half _EmissionStrength;
        fixed4 _Color;
		fixed4 _Color2;
		fixed4 _EmissionColor;
        sampler2D _BumpMap;

		//Spherical Mask
		float4 _Position;
		half _Radius;
		half _Softness;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        //void surf (Input IN, inout SurfaceOutput o)
        {
			//Color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 j = tex2D (_MainTex2, IN.uv_MainTex2) * _Color2;
			//Grayscale
	        //half grayscale = (c.r + c.g + c.b) * 0.333;
			//half grayscale = (j.r + j.g + j.b + j.a);
			//fixed4 c_g = fixed4(j.r, j.g, j.b, j.a);
			//Emission
			fixed4 e = tex2D(_EmissionTex, IN.uv_EmissionTex) * _EmissionColor * _EmissionStrength;
            o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));

			half d = distance(_Position, IN.worldPos);
			half sum = saturate((d - _Radius) / -_Softness);
			fixed4 lerpColor = lerp(fixed4(j.r, j.g, j.b, j.a), c * _ColorStrength, sum);
			fixed4 lerpEmission = lerp(fixed4(0,0,0,0), e, sum);

            // Albedo comes from a texture tinted by color
            o.Albedo = lerpColor.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
			o.Emission = lerpEmission.rgb;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
