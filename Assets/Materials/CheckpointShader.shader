Shader "Sprites/Checkpoint"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_Color("Tint", Color) = (1,1,1,1)
		_MainTexture("Main Texture", 2D) = "white" {}
		_AlphaTexture("Alpha Texture", 2D) = "white" {}
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
		Fog { Mode Off }
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
			fixed4 color	: COLOR;
			half2 texcoord  : TEXCOORD0;
		};

		fixed4 _Color;
		fixed4 _MainTex_TexelSize;

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
		sampler2D _AlphaTexture;
		sampler2D _MainTexture;

		// Fragment-Shader
		fixed4 frag(v2f IN) : SV_Target
		{
			float time = _Time.x * 2.2;
			float timeOffset = time - floor(time);
			
			float2 newTexCoord = IN.texcoord;
			newTexCoord.y += time;

			fixed4 c = tex2D(_MainTexture, newTexCoord) * IN.color;
			fixed4 a = tex2D(_AlphaTexture, IN.texcoord) * IN.color;
			c.rgb *= a.a;

			return c;
		}
		
			ENDCG
		}
	}
}