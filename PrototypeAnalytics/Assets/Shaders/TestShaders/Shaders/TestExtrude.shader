// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/TestExtrude"
{
    Properties 
    {
        _texture ("Textura", 2D) = "white"{}
        _NormalExtrusion ("Normal Extrusion", Range(0, 5)) = 0.5
            
    }
    
    subShader
    {
    
        Tags { "Queue" = "Geometry" }
        
        Pass 
        {  

            Cull back 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag 
            #include "UnityCG.cginc" 

            uniform float _NormalExtrusion;
            uniform sampler2D _texture ;
            uniform float4 WorldSpaceLightDirection;
            uniform float4 LightColor;  
            uniform float4 WorldSpaceCameraPosition;
            
            struct v2f 
            
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldSpaceNormal : NORMAL;

            
            };
            

            v2f vert (appdata_full v)
            {
            
            v2f output;
            v.vertex.xyz += v.normal * _NormalExtrusion;
            output.uv = v.texcoord + float2(0, _Time.x);
            output.pos = UnityObjectToClipPos(v.vertex); 
            output.worldSpaceNormal = normalize(mul((float3x3)unity_ObjectToWorld,v.normal)); 
            return output;
            }
            
            float4 frag(v2f i) : COLOR 
                                        
            {
                float NdotL = dot(normalize(i.worldSpaceNormal), normalize(WorldSpaceLightDirection));
                float4 finalColor = tex2D(_texture,i.uv) * saturate(NdotL) * LightColor;
        
                return finalColor;
            }
            
            
            
            
        ENDCG   
    } 
    }
    }