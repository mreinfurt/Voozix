Shader "Sprites/ShockwaveImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ShockwaveCenter("Shockwave Center", Vector) = (0.5, 0.5, 0, 0)
		_StartTime("Start Time", Float) = 0.5
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			fixed4 _ShockwaveCenter;
			fixed _StartTime;

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);

				float time = (_Time.y - _StartTime);
				float circleRadiusWidth = 0.045;

				float distanceToCenter = distance(i.uv, _ShockwaveCenter.xy);
				if (distanceToCenter <= time + circleRadiusWidth && distanceToCenter >= time - circleRadiusWidth) {

					float ecart = (distanceToCenter - time); // -0.02 to 0.02
					float powEcart = 1.0 - pow(abs(ecart * 40.0), 0.4); // -1 to 1
					float ecartTime = ecart  * powEcart; // -0.02 to 0.02

					float2 diff = normalize(i.uv - _ShockwaveCenter.xy); // Direction of the shockwave
					float2 newTexCoord = i.uv + (diff * ecartTime);

					color = tex2D(_MainTex, newTexCoord) * float4(1.35, 1.25, 1.25, 1); // Make it a bit brighter and red as well
				}

				return color;
			}
			ENDCG
		}
	}
}
