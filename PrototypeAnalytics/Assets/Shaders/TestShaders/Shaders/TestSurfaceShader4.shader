Shader "Custom/TestSurfaceShader4" {
    Properties 
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Density ("Density", Range(2,50)) = 30
        _Amount ("Extrusion Amount", Range(-0.5,1)) = 0.5

    }
    
    SubShader 
    {
        Tags { "RenderType" = "Opaque" }
        cull off

        CGPROGRAM

        #pragma surface surf Lambert vertex:vert
        uniform sampler2D _MainTex;
        float _Amount;
        float _Density;

        struct Input 
        {
            float2 uv_MainTex;

            float3 worldPos;
        };


      void vert (inout appdata_full v) {
          v.vertex.xyz += v.normal * (_Amount + abs(sin(_Time.y)));
      }

        

        void surf (Input IN, inout SurfaceOutput o) 
        {
            clip (frac((IN.worldPos.y + (IN.worldPos.x*0.1 - (_Time.y))) * _Density) - 0.5);
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb ;



        }       

        ENDCG
    }
    
    FallBack "Diffuse"
}


