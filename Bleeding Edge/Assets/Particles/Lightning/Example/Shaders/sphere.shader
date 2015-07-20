Shader "Custom/sphere"
 {
	Properties 
	{
		_TintColor ("color", color) = (1,1,1,1)
	}
	SubShader 
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent+10" }
		cull back zwrite off
		blend one one
		
		CGPROGRAM
		#pragma surface surf Lambert  vertex:vert

		float4 _TintColor;

		struct Input {
			float2 uv_MainTex;
			float3 customColor;
		};

		void vert (inout appdata_full v, out Input o) 
		{
			  UNITY_INITIALIZE_OUTPUT(Input, o);
			  o.customColor = pow(1 - abs(dot(normalize(ObjSpaceViewDir(v.vertex)), v.normal)), 3 + abs(_SinTime.w));
        }

		void surf (Input IN, inout SurfaceOutput o) 
		{
			o.Albedo = _TintColor.rgb * IN.customColor ;
			o.Alpha =  _TintColor.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
