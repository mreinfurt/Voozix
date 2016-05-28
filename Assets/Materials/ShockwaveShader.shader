Shader "Sprites/Shockwave"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_Color("Tint", Color) = (1,1,1,1)
		_ShockwaveCenter("Shockwave Center", Vector) = (0.5, 0.5, 0, 0)
		_StartTime("Start Time", Float) = 0.5
	}
	
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog{ Mode Off }
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;
			fixed4 _ShockwaveCenter;
			fixed _StartTime;

			// Vertex-Shader
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;

				return OUT;
			}

			sampler2D _MainTex;

			// Fragment-Shader
			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
				c.rgb *= c.a;
			
				float time = (_Time.y - _StartTime);

				float distanceToCenter = distance(IN.texcoord, _ShockwaveCenter.xy);
				if (distanceToCenter <= time + 0.025 && distanceToCenter >= time - 0.025) {

					float ecart = (distanceToCenter - time); // -0.02 to 0.02
					float powEcart = 1.0 - pow(abs(ecart * 40.0), 0.4); // -1 to 1
					float ecartTime = ecart  * powEcart; // -0.02 to 0.02

					float2 diff = normalize(IN.texcoord - _ShockwaveCenter.xy); // Direction of the shockwave
					float2 newTexCoord = IN.texcoord + (diff * ecartTime);

					c = tex2D(_MainTex, newTexCoord) * IN.color * 1.25; // Make it a bit brighter as well
				}

				return c;
			}
			ENDCG
		}
	}
}