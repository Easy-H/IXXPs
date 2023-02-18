Shader "Custom/OutLine"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _OutLineColor("OutLine Color", Color) = (0,0,0,0)
        _OutLineWidth("OutLine Width", Range(0.001, 1)) = 0.01
    }
    SubShader
        {
        Tags { "RenderType" = "Opaque" }
            LOD 200

            cull front
            CGPROGRAM
            #pragma surface surf NoLight vertex:vert noshadow noambient
            #pragma target 3.0

            float4 _OutLineColor;
            float _OutLineWidth;

            void vert(inout appdata_full v) {
                float3 newAdded = v.normal.xyz;
                float3 forward = UNITY_MATRIX_IT_MV[2].xyz;
                forward = forward * mul(forward, newAdded);
                float3 vectorUp = v.normal.xyz;
                float3 vectorRight = v.tangent.xyz;
                float3 vectorForward = dot(vectorUp, vectorRight);
                newAdded -= forward;
                //v.vertex.xyz += newAdded * _OutLineWidth;
                v.vertex.xyz += vectorUp * _OutLineWidth + vectorRight * _OutLineWidth + vectorForward * _OutLineWidth;
                //v.vertex.xyz += forward;
                //v.vertex.xyz += v.tangent.xyz * _OutLineWidth;
            }

            struct Input
            {
                float4 color;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {

            }

            float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten) {
                return float4(_OutLineColor.rgb, 1);
            }
            ENDCG
            
            cull back
            CGPROGRAM
            #pragma surface surf Lambert
            #pragma target 3.0

            sampler2D _MainTex;
            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}