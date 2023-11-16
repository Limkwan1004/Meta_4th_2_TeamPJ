Shader "SkyBox/BlendTwoSkybox"
{
    Properties
    {
        _Texture("Texture",2D) = "white"{}
        _Texture2("Texture2",2D) = "white"{}
        // _LerpTexture("LerpTexture", 2D) = "white"{}
         _LerpControl("LerpControl", Range(0,1)) = 0
             // [Gamma]  _Exposure("Exposure", Range(0.000000,8.000000)) = 1.000000

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




                    #define M_PI 3.141592653589793
                   inline float2 ToRadialCoords(float3 coords)
                   {
                        float3 normalizedCoords = normalize(coords);
                        float latitude = acos(normalizedCoords.y);
                        float longitude = atan2(normalizedCoords.z, normalizedCoords.x);
                        float2 sphereCoords = float2(longitude, latitude) * float2(0.5 / M_PI, 1.0 / M_PI);
                        return float2(0.5, 1.0) - sphereCoords;
                   }



         // Attributes ����ü�� ���ؽ� ���̴��� ��ǲ ����ü�� ���
         struct Attributes
         {
             float4 positionOS   : POSITION;
             float2 uv           : TEXCOORD0;

             // ���̴����� ���� ���� �ؽ�ó�� ���ÿ� ����� ��, �� �̻��� UV ��ǥ�� ����� ��찡 �ִµ� �׷� ������ TEXCOORD0, TEXCOORD1������ �ø�ƽ�� ���
             //����Ƽ�� ���� �Ʒ��� 0,0 (�𸮾��� ������)
         };

         //�����׸�Ʈ ���̴� ��ǲ ����ü
     //�� ����ü�� ������ ������ �ݵ�� SV_POSITION �ø�ƽ�� ������ �־�� ��
         struct Varyings
         {
              float4 positionHCS  : SV_POSITION;
              float2 uv           : TEXCOORD0;

         };


            TEXTURE2D(_Texture);
            TEXTURE2D(_Texture2);
            //  TEXTURE2D(_Texture3);

              SAMPLER(sampler_Texture);
              SAMPLER(sampler_Texture2);
              //  SAMPLER(sampler_Texture3);

                CBUFFER_START(UnityPerMaterial)
                    float4 _Texture_ST;
                    float4 _Texture2_ST;
                    //  float4 _Texture3_ST;

                    float4 _finalTexture;

                    float _LerpControl;
                CBUFFER_END




                //�������̴�
                Varyings vert(Attributes IN)
                {

                    Varyings OUT;
                    OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                    OUT.uv = IN.uv;
                    // OUT.uv = TRANSFORM_TEX(IN.uv, _Texture);
                     return OUT;

                     //Varyings OUT;
                     //UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                     ////���ؽ� �������� Ŭ�������̽��� ��ȯ
                     //OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                     //OUT.uv = IN.uv;
                     //return OUT;
                 }



                 //���� ���̴�
                 half4 frag(Varyings IN) : SV_Target
                 {



                     //float2 equiUV = ToRadialCoords(IN.normal);
                     //tex2D(sampler_Texture3, equiUV); //float4 ��

                     //half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv); //��ũ��
                     //half4 color = _BaseMap.Sample(sampler_BaseMap, IN.uv); //����

                     float4 colorA = SAMPLE_TEXTURE2D(_Texture, sampler_Texture, TRANSFORM_TEX(IN.uv, _Texture));
                     float4 colorB = SAMPLE_TEXTURE2D(_Texture2, sampler_Texture2, TRANSFORM_TEX(IN.uv, _Texture2));
                     //finalColor ������ ���� , �� �ؽ��ĸ� lerp �� ���� �ִ´�. 
                     // �� �� ���ڰ� 0�ϰ�� colorA �� ������, 1�� ��� colorB�� ������ 0.5 �� ��� ����
                     float4 finalColor;

                     finalColor = lerp(colorA, colorB, _LerpControl);
                     //SAMPLER()�� 
                    //SAMPLE_TEXTURE2D�� ���÷� ������ �ؽ��� �ֱ�

                //finalColor(float4)�� Sampler_Textrue3(Texture2D)�� ���� ������ �ְ� tex2D()ȣ��

                    return finalColor;

                }




                ENDHLSL
            }
         }

             Fallback Off
}