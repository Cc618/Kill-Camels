// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Cc/BlackWhiteFX"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

		_BWRatio ("Black White Ratio", Range (0, 1)) = .5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
		
		// Enable alpha
		Blend SrcAlpha OneMinusSrcAlpha

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

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                
				o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
				return o;
            }

            sampler2D _MainTex;
			float _BWRatio;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

				fixed mean = (col.r + col.g + col.b) * .333;
                
				fixed4 bw = fixed4(mean, mean, mean, 0);

				col = _BWRatio * (col + bw);
				col.a *= .8;

				return col;
            }
            ENDCG
        }
    }
}
