Shader "Custom/GradientSphere" {
   Properties {
       _Color ("Main Color", Color) = (1,1,1,1)
       _Radius ("Radius", Float) = 1.0
   }

   SubShader {
       Tags { "RenderType"="Opaque" }
       LOD 100

       CGPROGRAM
       #pragma surface surf Lambert

       struct Input {
           float2 uv_MainTex;
       };

       fixed4 _Color;
       float _Radius;

       void vert (inout appdata_full v) {
           float dist = distance(v.vertex, float4(0, 0, 0, 1));
           v.color.a = 1.0 - saturate(dist / _Radius);
       }

       void surf (Input IN, inout SurfaceOutput o) {
           o.Albedo = _Color.rgb;
       }
       ENDCG
   }

   SubShader {
       Tags { "RenderType"="Transparent" }
       LOD 100

       Blend SrcAlpha OneMinusSrcAlpha
       CGPROGRAM
       #pragma surface surf Lambert

       struct Input {
           float2 uv_MainTex;
       };

       fixed4 _Color;
       float _Radius;

       void vert (inout appdata_full v) {
           float dist = distance(v.vertex, float4(0, 0, 0, 1));
           v.color.a = 1.0 - saturate(dist / _Radius);
       }

       void surf (Input IN, inout SurfaceOutput o) {
           o.Albedo = _Color.rgb;
       }
       ENDCG
   }
}