Shader "Test/MyTests/TestExtrudeShader"
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
            v.vertex.xyz += v.normal * float3(abs(sin(_Time.y)),abs(sin(_Time.y)),abs(sin(_Time.y))) * (_NormalExtrusion);
            output.uv = v.texcoord + float2(0, _Time.y);
            output.pos = UnityObjectToClipPos(v.vertex); 
            output.worldSpaceNormal = normalize(mul((float3x3)unity_ObjectToWorld,v.normal)); 
            return output;
            }
            
            float4 frag(v2f i) : COLOR 
                                        
            {
                
                float4 finalColor = tex2D(_texture,i.uv);
        
                return finalColor;
            }
            
            
            
            
        ENDCG   
    } 
    }
    }