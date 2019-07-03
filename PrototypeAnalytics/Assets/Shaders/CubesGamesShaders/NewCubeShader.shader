Shader "CubesGame/NewCubeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightRange("Range",Range(0, 0.5)) = 0.2
        _Color("Color",Color) = (1,1,1,1)
         _Alpha("Range",Range(0, 1)) = 1 

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
         Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
         Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

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
                half3 worldNormal : TEXCOORD1;

            };

            struct v2f
            {
                float2 uv : TEXCOORD1;
                half3 worldNormal : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _LightRange;
            float4 _Color;
            float _Alpha;
            
            v2f vert (appdata v )
            {
                v2f o;
                o.worldNormal = UnityObjectToWorldNormal(v.worldNormal) + half3(sin(_Time.y), sin(_Time.y), sin(_Time.y));
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 c = 0;
                // sample the texture
                c.rgb = i.worldNormal * _LightRange;
                fixed4 col = (tex2D(_MainTex, i.uv) * _Color) + c;
                col.a = _Alpha;

                return col;
            }
            ENDCG
        }
    }
}