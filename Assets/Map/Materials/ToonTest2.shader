// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/ToonTest2"
{
	Properties
	{

		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	//_Glossiness("Smoothness", Range(0,1)) = 0.5
		//_Metallic("Metallic", Range(0,1)) = 0.0
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_SpecShininess ("Specular Shininess", Range(1.0,100.0)) = 2.0

		_DissolveTexture("Cheese", 2D) = "white" {}
	_DissolveAmount("Cutout", Range(0,1)) = 0

		_ExtrudeAmount("Extrude amount", float) = 0

		//_FresnelPower("Fresnel Power", Range(0.0,3.0)) = 1.4
		_FresnelScale("Fresnel Scale", Range(0.0,1.0)) = 1.0
		_FresnelColor("Fresnel Color", Color) = (1,1,1,1)

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM

	#pragma vertex vertexFunction //build object
	#pragma fragment fragmentFunction // color in object
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"




			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;

			};

			struct v2f
			{
				float4 position: SV_POSITION; //return position, SV_ makes sure that it works on dx systems like ps4
				float2 uv : TEXCOORD0; // return uvs
				float3 normal : NORMAL; 
				float4 posWorld : TEXCOORD1;
			};

			float4 _LightColor0;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;

			float4 _SpecColor;
			float _SpecShininess;

			sampler2D _DissolveTexture;
			float _DissolveAmount;

			float _ExtrudeAmount; 

			//float _FresnelPower;
			float _FresnelScale;
			float4 _FresnelColor;

			
			v2f vertexFunction(appdata IN)    // build our object
			{
				v2f OUT;
				IN.vertex.x += IN.normal.xyz * _ExtrudeAmount;
				OUT.position = UnityObjectToClipPos(IN.vertex);  //pos of objects verticies //mvp stans for model view projection, this gets the model, view from camera, projrction from camera(orthofraphic or perspective)
				OUT.normal = mul(float4(IN.normal, 0.0), unity_ObjectToWorld).xyz;
					//OUT.uv = IN.uv;
					OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
				return OUT; // pass things to screen
			}
			
			fixed4 fragmentFunction(v2f IN) : SV_Target
			{ // target to screen 

				float4 textureColor = tex2D(_MainTex, IN.uv); // combine texture with uvs

				float3 normalDirection = normalize(IN.normal);
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float3 viewDirection = normalize(_WorldSpaceCameraPos - IN.posWorld.xyz); // world space camera pos - world space position of vertex 
				float3 diffuse = _LightColor0.rgb * _Color.rgb * max(0.0, dot(normalDirection/2, lightDirection/2));
				float3 specular;
					if (dot(normalDirection, lightDirection) < 0.0)
					{
						specular = float3(0.0, 0.0, 0.0);
					}
					else
					{
						specular = _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _SpecShininess);
					}
					 
					float3 I = IN.posWorld - _WorldSpaceCameraPos.xyz;
					float refl = _FresnelScale * pow(1.0 + dot(normalize(I), normalDirection), _FresnelColor);

					float3 diffuseSpecular = diffuse + specular;

				float4 dissolveColor = tex2D(_DissolveTexture, IN.uv);

				float4 finalColor = float4(diffuseSpecular, 1) * textureColor;

				clip(dissolveColor.rgb - _DissolveAmount); //kills pixel

				//return textureColor * _Color;
				//return float4(diffuseSpecular, 1) * textureColor;
				return lerp(finalColor, _FresnelColor, refl);
			}
			ENDCG
		}
	}
}
