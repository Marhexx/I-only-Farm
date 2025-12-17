Shader "Unlit/StarDome"
{
    Properties
    {
        _MainTex ("Star Texture", 2D) = "white" {}
        _TintColor ("Tint Color", Color) = (1,1,1,0)
        _Brightness ("Brightness", Range(0,3)) = 1
    }
    
    SubShader
    {
        Tags { 
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Cull Front          // Se ve por dentro de la esfera
        ZWrite Off          // No bloquea otros objetos
        Lighting Off        // Sin iluminación
        Blend SrcAlpha OneMinusSrcAlpha // Transparencia

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;
            float _Brightness;

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _TintColor;
                col.rgb *= _Brightness;
                return col;
            }
            ENDCG
        }
    }
}
