Shader "Hidden/Scanline" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_RampTex ("Base (RGB)", 2D) = "grayscaleRamp" {}
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
				
CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform sampler2D _RampTex;
uniform half _RampOffset;

fixed4 frag (v2f_img i) : SV_Target
{
	fixed4 original = tex2D(_MainTex, i.uv);
	original.r = tex2D(_MainTex, fixed2(i.uv.x,i.uv.y+0.002)).r;
	original.b = tex2D(_MainTex, fixed2(i.uv.x,i.uv.y-0.002)).b;


	float v = sin((i.uv.y+_Time.x*0.2)*300);
	
	fixed4 sample = tex2D(_MainTex, fixed2(i.uv.x+(0.005*v),i.uv.y));
	sample += tex2D(_MainTex, fixed2(i.uv.x-(0.005*v),i.uv.y));
	sample*=0.5;
	sample += tex2D(_MainTex, fixed2(i.uv.x+(0.001*v),i.uv.y));
	sample += tex2D(_MainTex, fixed2(i.uv.x-(0.001*v),i.uv.y));
	sample*=0.5;
	sample += tex2D(_MainTex, fixed2(i.uv.x+(0.0005*v),i.uv.y));
	sample += tex2D(_MainTex, fixed2(i.uv.x-(0.0005*v),i.uv.y));
	if(v>0){
		if(original.r+original.b+original.g<0.2&&sample.r+sample.g+sample.b>0.6){
			//return fixed4(0,1,0,1);
			return original+v*(sample*0.2);
		}
	}else if(original.r+original.b+original.g>1){
		//return fixed4(1,0,0,1);
		//return sample*0.1;
		return original+v*(1-sample*.25);
	}
	//return fixed4(0,0,1,1);
	return original;
	/*
	fixed grayscale = Luminance(original.rgb);
	half2 remap = half2 (grayscale + _RampOffset, .5);
	fixed4 output = tex2D(_RampTex, remap);
	output.a = original.a;
	if((i.uv.y+_Time.x*0.5)%0.01>0.005&&
	original.r+original.b+original.g<0.2){
		fixed4 sample = tex2D(_MainTex, fixed2(i.uv.x+0.01,i.uv.y));
		sample += tex2D(_MainTex, fixed2(i.uv.x-0.01,i.uv.y));
		sample *= 0.8;

		sample = tex2D(_MainTex, fixed2(i.uv.x+0.005,i.uv.y));
		sample += tex2D(_MainTex, fixed2(i.uv.x-0.005,i.uv.y));
		sample *= 0.5;
		
		sample += tex2D(_MainTex, fixed2(i.uv.x+0.001,i.uv.y));
		sample += tex2D(_MainTex, fixed2(i.uv.x-0.001,i.uv.y));
		return original+sample*0.1;
	}
	if(original.r+original.b+original.g>1){
		fixed4 sample = 1-tex2D(_MainTex, fixed2(i.uv.x+0.01,i.uv.y));
		sample += 1-tex2D(_MainTex, fixed2(i.uv.x-0.01,i.uv.y));
		sample *= 0.5;
		
		sample += 1-tex2D(_MainTex, fixed2(i.uv.x+0.005,i.uv.y));
		sample += 1-tex2D(_MainTex, fixed2(i.uv.x-0.005,i.uv.y));
		return original-sample*0.1;
	}
	return original;*/
}
ENDCG

	}
}

Fallback off

}
