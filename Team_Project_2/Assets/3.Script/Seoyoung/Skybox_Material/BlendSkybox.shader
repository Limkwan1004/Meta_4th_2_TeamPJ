Shader "SkyBox/BlendTwoSkybox"
{
    Properties
    {
        _Texture("Texture",2D) = "white"{}
        _Texture2("Texture2",2D) = "white"{}
        _LerpControl("LerpControl", Range(0,1)) = 0
        [Gamma]  _Exposure("Exposure", Range(0.000000,8.000000)) = 1.000000

    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

            Pass
            {
                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                struct Attributes
                {
                    float4 positionOS   : POSITION;
                    float2 uv           : TEXCOORD0;
                    // ���̴����� ���� ���� �ؽ�ó�� ���ÿ� ����� ��, �� �̻��� UV ��ǥ�� ����� ��찡 �ִµ� �׷� ������ TEXCOORD0, TEXCOORD1������ �ø�ƽ�� ���
                    //����Ƽ�� ���� �Ʒ��� 0,0 (�𸮾��� ������)
                };

                struct Varyings
                {
                    float4 positionHCS  : SV_POSITION;
                    float2 uv           : TEXCOORD0;
                    //   float3 normal : NORMAL;
                   };

                   TEXTURE2D(_Texture);
                   TEXTURE2D(_Texture2);
                   SAMPLER(sampler_Texture);
                   SAMPLER(sampler_Texture2);

                   CBUFFER_START(UnityPerMaterial)
                       float4 _Texture_ST;
                       float4 _Texture2_ST;
                       float _LerpControl;
                   CBUFFER_END


                  #define PI 3.141592653589793
                  inline float2 ToRadialCoords(float3 coords)
                  {
                       float3 normalizedCoords = normalize(coords);
                       float latitude = acos(normalizedCoords.y);
                       float longitude = atan2(normalizedCoords.z, normalizedCoords.x);
                       float2 sphereCoords = float2(longitude, latitude) * float2(0.5 / PI, 1.0 / PI);
                       return float2(0.5, 1.0) - sphereCoords;
                  }


                   Varyings vert(Attributes IN)
                   {
                       Varyings OUT;
                       OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                       OUT.uv = IN.uv;
                       return OUT;
                   }

                   half4 frag(Varyings IN) : SV_Target
                   {


                       half4 colorA = SAMPLE_TEXTURE2D(_Texture, sampler_Texture, TRANSFORM_TEX(IN.uv, _Texture));
                       half4 colorB = SAMPLE_TEXTURE2D(_Texture2, sampler_Texture2, TRANSFORM_TEX(IN.uv, _Texture2));
                       //finalColor ������ ���� , �� �ؽ��ĸ� lerp �� ���� �ִ´�. 
                       // �� �� ���ڰ� 0�ϰ�� colorA �� ������, 1�� ��� colorB�� ������ 0.5 �� ��� ����
                       half4 finalColor;
                       finalColor = lerp(colorA, colorB, _LerpControl);
                       // float2 tc = ToRadialCoords(IN.normal);

                        return finalColor;


                    }




                    ENDHLSL
                }
        }

            Fallback Off
}
