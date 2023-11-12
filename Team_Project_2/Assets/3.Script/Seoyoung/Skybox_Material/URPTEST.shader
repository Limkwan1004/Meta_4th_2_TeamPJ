Shader "Codercat/URP_Test"
{
	Properties
	{
		_MainTex("RGB 01", 2D) = "white" {}
		_MainTex2("RGB 01", 2D) = "white" {}
		_lerpTexture("lerp", Range(0,1)) = 0

		 //_���۷��� �̸� ("����Ƽ ������Ƽ���� �������� �̸�", �ڷ���) = "�ڷ����� �´� �⺻ ��"{}
	}

	SubShader
	{

			Tags
			{
				"RenderPipeline" = "UniversalPipeline"
				"RenderType" = "Opaque" // ���� Ÿ���� �������̶�� ����
										//������ = "Opaque", ���� Ŭ�� "TransparentCutout"
										//������ = "Transparent", ���(��ī�̹ڽ�) = "Background"
										//��������(GUI �ؽ���, �ı� �� ȿ��) = "Overlay"
				"Queue" = "Geometry"	//��� = "Background", ������ = "Geometry", ���� Ŭ�� = "AlphaTest", ������ = "Transparent", �������� = "Overlay"
			}

			LOD 100		//Subshader�� ���� ����� ���̴��� �����,
						//�� ���� LOD ���� �̿��� ���� ������ ��⿡�� ���� ����� ���̴���
						//���ư� �� �ֵ��� ������ �� �ִٰ� �Ѵ�. ���� ���̴� �������� ��������
						//��ũ��Ʈ���� Maximum LOD ��ġ�� ���� ��������Ѵٰ� �Ѵ�.


				Pass
				{
				 Name "Universal Forward"
					  Tags { "LightMode" = "UniversalForward" }

				HLSLPROGRAM
			#pragma prefer_hlslcc gles
					#pragma exclude_renderers d3d11_9x
					#pragma vertex vert
					#pragma fragment frag

					#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"        	

					struct VertexInput
					{
						float4 vertex : POSITION;
						float2 uv 	: TEXCOORD0;
					};

					struct VertexOutput
					{
					float4 vertex  	: SV_POSITION;
					float2 uv 	: TEXCOORD0;
					};

					Texture2D _MainTex;
					float4 _MainTex_ST;
					SamplerState sampler_MainTex;

					Texture2D _MainTex2;
					float4 _MainTex_ST2;
					SamplerState sampler_MainTex2;

					float _lerpTexture;


					VertexOutput vert(VertexInput v)
					{

						VertexOutput o;
						o.vertex = TransformObjectToHClip(v.vertex.xyz);
						o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
						return o;
					}


					half4 frag(VertexOutput i) : SV_Target
					{

					float4 _tex = _MainTex.Sample(sampler_MainTex, i.uv);
					float4 _tex2 = _MainTex2.Sample(sampler_MainTex2, i.uv);
					i.RGB = lerp(_tex.rgb, _tex2.rgb, _lerpTexture);
					return _tex;
					}




					ENDHLSL
				}
	}
}


