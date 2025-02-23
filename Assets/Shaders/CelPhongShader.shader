﻿// Original Cg/HLSL code stub copyright (c) 2010-2012 SharpDX - Alexandre Mutel
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// Adapted for COMP30019 by Jeremy Nicholson, 10 Sep 2012
// Adapted further by Chris Ewin, 23 Sep 2013
// Adapted further (again) by Alex Zable (port to Unity), 19 Aug 2016

/* Further adapted by Stewart Collins - 326206
** 17/10/16
** Adapted the shader to make it a cel shader
*/

Shader "Unlit/CelShader"
{
	Properties
	{
		_PointLightColor("Point Light Color", Color) = (255, 255, 255)
		_PointLightPosition("Point Light Position", Vector) = (132.0, 66.0, 7.0)
	}
		SubShader
	{
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
	//The amount of colors in the cel shader palette
	#define MAX_COLORS 8

	uniform float3 _PointLightColor;
	uniform float3 _PointLightPosition;

	//The available cel shader colors
	uniform float3 _CelShadingColors[MAX_COLORS];


	struct vertIn
	{
		float4 vertex : POSITION;
		float4 normal : NORMAL;
		float4 color : COLOR;
	};

	struct vertOut
	{
		float4 vertex : SV_POSITION;
		float4 color : COLOR;
		float4 worldVertex : TEXCOORD0;
		float3 worldNormal : TEXCOORD1;
	};

	// Implementation of the vertex shader
	vertOut vert(vertIn v)
	{
		vertOut o;

		// Convert Vertex position and corresponding normal into world coords
		// Note that we have to multiply the normal by the transposed inverse of the world 
		// transformation matrix (for cases where we have non-uniform scaling; we also don't
		// care about the "fourth" dimension, because translations don't affect the normal) 
		float4 worldVertex = mul(_Object2World, v.vertex);
		float3 worldNormal = normalize(mul(transpose((float3x3)_World2Object), v.normal.xyz));

		// Transform vertex in world coordinates to camera coordinates, and pass colour
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.color = v.color;

		// Pass out the world vertex position and world normal to be interpolated
		// in the fragment shader (and utilised)
		o.worldVertex = worldVertex;
		o.worldNormal = worldNormal;

		return o;
	}

	// Implementation of the fragment shader
	fixed4 frag(vertOut v) : SV_Target
	{
		// Our interpolated normal might not be of length 1
		float3 interpNormal = normalize(v.worldNormal);

		// Calculate ambient RGB intensities
		float Ka = 1;
		float3 amb = v.color.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

		// Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
		// (when calculating the reflected ray in our specular component)
		float fAtt = 1;
		float Kd = 1;
		float3 L = normalize(_PointLightPosition - v.worldVertex.xyz);
		float LdotN = dot(L, interpNormal);
		float3 dif = fAtt * _PointLightColor.rgb * Kd * v.color.rgb * saturate(LdotN);

		// Calculate specular reflections
		// Specular reflections were reduced as mushrooms do not provide much direct reflection
		float Ks = 0.5;
		float specN = 5; // Values>>1 give tighter highlights
		float3 V = normalize(_WorldSpaceCameraPos - v.worldVertex.xyz);
		// Using Blinn-Phong approximation:
		specN = 25; // We usually need a higher specular power when using Blinn-Phong
		float3 H = normalize(V + L);
		float3 spe = fAtt * _PointLightColor.rgb * Ks * pow(saturate(dot(interpNormal, H)), specN);

		// Combine Phong illumination model components
		float4 returnColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
		returnColor.rgb = amb.rgb + dif.rgb + spe.rgb;

		//Below is the cel shading component of the shader
		//It calculates the return color by finding the color which has the lowest total difference in the
		//rgb values from the available color palette and then returning that color
		float4 celColor = float4(0.0f, 0.0f, 0.0f, 1.0); 
		celColor.rgb = _CelShadingColors[0].rgb;
		//All color values are between 0 and 1
		float currentCelDiff = 500;
		currentCelDiff = (abs(returnColor.r - celColor.r) + abs(returnColor.g - celColor.g) + abs(returnColor.b - celColor.b));
		float4 newCelColor = float4(0.0f, 0.0f, 0.0f, 1.0);
		float newCelDiff = 500;
		for (int i = 1; i < MAX_COLORS; i++) {
			newCelColor.rgb = _CelShadingColors[i].rgb;
			newCelDiff = (abs(returnColor.r - newCelColor.r) + abs(returnColor.g - newCelColor.g) + abs(returnColor.b - newCelColor.b));
			if (newCelDiff < currentCelDiff) {
				celColor = newCelColor;
				currentCelDiff = newCelDiff;
			}
		}
		return celColor;
	}
		ENDCG
	}
	
	}
	Fallback "Diffuse"
}
