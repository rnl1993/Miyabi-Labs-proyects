Shader "Test/MyTests/TestShader2"
{

        Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // include file that contains UnityObjectToWorldNormal helper function
            #include "UnityCG.cginc"

            struct v2f {
              
                half3 worldNormal : TEXCOORD1;
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            // vertex shader: takes object space normal as input too
            v2f vert (float4 vertex : POSITION, float3 normal : NORMAL, appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);
                o.uv = v.texcoord + float2(_Time.x,0);
             
                o.worldNormal = UnityObjectToWorldNormal(normal) + half3(cos(_Time.x), cos(_Time.y), cos(_Time.z));
                return o;
            }

            sampler2D _MainTex;
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = 0;
          
                c.rgb = (i.worldNormal/5)  + (tex2D(_MainTex, i.uv));
                c.r = 1;
                return c;
            }
            ENDCG
        }
    }
}
