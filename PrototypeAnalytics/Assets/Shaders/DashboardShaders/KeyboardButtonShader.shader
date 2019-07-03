Shader "Dashboard/KeyboardButtonShader"
{
    Properties
    {
        _lerpFactor ("Lerp Factor", Range(0,1)) = 0.5
        _MainTex ("Texture", 2D) = "white" {}
        _Tex2("Texture Two", 2D) = "white"{}
        _Color("Color",Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
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
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            uniform float _lerpFactor;
            sampler2D _Tex2;
            float4 _Tex2_ST;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv, _Tex2);
            
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                return lerp(tex2D(_MainTex,i.uv) * _Color,tex2D(_Tex2,i.uv2),(_lerpFactor));
            }
            ENDCG
        }
    }
}