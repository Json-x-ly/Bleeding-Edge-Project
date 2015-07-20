Shader "RenderFX/Lightning bolt - OS" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_LightningColor ("Color ", color) = (1,1,1,1)
		_WidthScale ("Width scale", Range(0.1, 1)) = 0.5
		_CameraDistanceLimit ("Camera distance limit. X - min. Y - max", Vector) = (5, 20, 0, 0)
	}
	SubShader 
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent"}
		Fog {mode off}
		Cull off
		ZTest LEqual ZWrite off
		
		Blend SrcAlpha one 
		BlendOp add
		
		Pass
		{
			CGPROGRAM
		
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"


			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 tangent : TANGENT;
				float4 color : COLOR;
			};

			struct v2f 
			{
				float4 pos : POSITION;
				float2 uvMain: TEXCOORD0;
				float4 color: COLOR;
			};

			float4 _MainTex_ST;
			float4 _CameraDistanceLimit;
			float _WidthScale;

			inline float3 GetWSOffsetVector(in float4 wsVertex, in appdata v)
			{
				float3 toCamera = _WorldSpaceCameraPos.xyz - wsVertex.xyz;
				float distanceToCamera = length(toCamera);
				float distanceFactor = clamp(distanceToCamera, _CameraDistanceLimit.x, _CameraDistanceLimit.y);
				float widthScale = _WidthScale * v.color.r * distanceFactor * v.tangent.w;

				float3 up = toCamera / distanceToCamera;

				return cross(up, v.tangent.xyz) * widthScale;
			}

			inline float3 GetOSOffsetVector(in appdata v)
			{
				float3 osCameraDir = ObjSpaceViewDir(v.vertex);
				float distanceToCamera =  length(osCameraDir);
				float distanceFactor = clamp(distanceToCamera, _CameraDistanceLimit.x, _CameraDistanceLimit.y);
				float widthScale = _WidthScale * v.color.r * distanceFactor * v.tangent.w;

				float3 up = osCameraDir / distanceToCamera;

				return cross(up, v.tangent.xyz) * widthScale;
			}

			v2f vert(appdata v)
			{
				v2f o;
				
				#if TANGENT_AS_WS_DIR
					float4 wsVertex = mul(_Object2World, v.vertex);  
					wsVertex.xyz += GetWSOffsetVector(wsVertex, v);
					o.pos = mul(UNITY_MATRIX_VP, wsVertex);
				#else
					v.vertex.xyz += GetOSOffsetVector(v);
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				#endif

				o.uvMain = TRANSFORM_TEX(v.texcoord, _MainTex);
				
				o.color.a = v.color.r * v.color.g * v.color.a;

				return o;
			}
			
			sampler2D _MainTex;
			fixed4 _LightningColor;
			half4 frag(v2f i ) : COLOR
			{	
				//return i.color;
				return _LightningColor * tex2D(_MainTex, i.uvMain.xy).a * i.color.a * 2;
			}

			ENDCG
		}
	} 
}