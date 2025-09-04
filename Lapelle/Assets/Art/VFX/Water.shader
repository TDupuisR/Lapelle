// Made with Amplify Shader Editor v1.9.8.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Water"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		_Lava_Tex("Lava_Tex", 2D) = "white" {}
		_Alpha("Alpha", Range( 0 , 1)) = 0
		_L_X_tile("L_X_tile", Float) = 1
		_D_X_tile("D_X_tile", Float) = 1
		_L_Y_tile("L_Y_tile", Float) = 1
		_D_Y_tile("D_Y_tile", Float) = 1
		_D_X_Pan("D_X_Pan", Float) = 1
		_L_X_Pan("L_X_Pan", Float) = 1
		_L_Y_Pan("L_Y_Pan", Float) = 1
		_D_Y_Pan("D_Y_Pan", Float) = 1
		_L_Min("L_Min", Float) = 0
		_D_Min("D_Min", Float) = 0
		_L_Max("L_Max", Float) = 0
		_D_Max("D_Max", Float) = 0
		_Distorsion_Intensity("Distorsion_Intensity", Range( 0 , 1)) = 0
		_Dist_Text("Dist_Text", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextureSample2("Texture Sample 0", 2D) = "white" {}
		_Float3("Float 0", Range( 0 , 1)) = 0
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_HDR("HDR", Float) = 0

		[HideInInspector][NoScaleOffset] unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }

		Cull Off

		HLSLINCLUDE
		#pragma target 2.0
		#pragma prefer_hlslcc gles
		// ensure rendering platforms toggle list is visible

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"

		ENDHLSL

		
		Pass
		{
			Name "Sprite Unlit"
            Tags { "LightMode"="Universal2D" }

			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM

			#define ASE_VERSION 19801
			#define ASE_SRP_VERSION 170004


			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile _ DEBUG_DISPLAY SKINNED_SPRITE

            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
            #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
            #define FEATURES_GRAPH_VERTEX

			#define SHADERPASS SHADERPASS_SPRITEUNLIT

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/Core2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging2D.hlsl"

			#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"


			sampler2D _Lava_Tex;
			sampler2D _Dist_Text;
			sampler2D _TextureSample0;
			sampler2D _TextureSample1;
			sampler2D _TextureSample2;
			CBUFFER_START( UnityPerMaterial )
			float _L_Min;
			float _L_Max;
			float _L_X_Pan;
			float _L_Y_Pan;
			float _D_Min;
			float _D_Max;
			float _D_X_Pan;
			float _D_Y_Pan;
			float _D_X_tile;
			float _D_Y_tile;
			float _Distorsion_Intensity;
			float _L_X_tile;
			float _L_Y_tile;
			float _Float3;
			float _HDR;
			float _Alpha;
			CBUFFER_END


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_SKINNED_VERTEX_INPUTS
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float3 positionWS : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if ETC1_EXTERNAL_ALPHA
				TEXTURE2D(_AlphaTex); SAMPLER(sampler_AlphaTex);
				float _EnableAlphaTexture;
			#endif

			
			float4 SampleGradient( Gradient gradient, float time )
			{
				float3 color = gradient.colors[0].rgb;
				UNITY_UNROLL
				for (int c = 1; c < 8; c++)
				{
				float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, gradient.colorsLength-1));
				color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
				}
				#ifndef UNITY_COLORSPACE_GAMMA
				color = SRGBToLinear(color);
				#endif
				float alpha = gradient.alphas[0].x;
				UNITY_UNROLL
				for (int a = 1; a < 8; a++)
				{
				float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, gradient.alphasLength-1));
				alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
				}
				return float4(color, alpha);
			}
			

			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_SKINNED_VERTEX_COMPUTE( v );

				v.positionOS = UnityFlipSprite( v.positionOS, unity_SpriteProps.xy );

				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.positionOS;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.positionOS = vertexValue;
				#else
					v.positionOS += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS);

				o.positionCS = vertexInput.positionCS;
				o.positionWS = vertexInput.positionWS;
				o.texCoord0 = v.uv0;
				o.color = v.color;
				return o;
			}

			half4 frag( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				float4 positionCS = IN.positionCS;
				float3 positionWS = IN.positionWS;

				Gradient gradient118 = NewGradient( 0, 4, 2, float4( 0.01323601, 0.6138443, 0.6377357, 0.06234836 ), float4( 0, 0.8117647, 0.7882353, 0.1470665 ), float4( 0.3301886, 0.8853303, 1, 0.2152895 ), float4( 0.8283019, 0.9919142, 1, 1 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float2 appendResult164 = (float2(_L_X_Pan , _L_Y_Pan));
				float2 appendResult144 = (float2(_D_X_Pan , _D_Y_Pan));
				float2 texCoord143 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult142 = (float2(_D_X_tile , _D_Y_tile));
				float2 panner146 = ( 1.0 * _Time.y * appendResult144 + ( float2( 0,0 ) + (texCoord143*appendResult142 + 0.0) ));
				float smoothstepResult140 = smoothstep( _D_Min , _D_Max , tex2D( _Dist_Text, panner146 ).r);
				float2 texCoord162 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult160 = (float2(_L_X_tile , _L_Y_tile));
				float2 panner169 = ( 1.0 * _Time.y * appendResult164 + ( ( smoothstepResult140 * _Distorsion_Intensity ) + (texCoord162*appendResult160 + 0.0) ));
				float smoothstepResult175 = smoothstep( _L_Min , _L_Max , tex2D( _Lava_Tex, panner169 ).r);
				float2 texCoord185 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float smoothstepResult189 = smoothstep( 0.12 , 0.21 , ( ( 1.0 - texCoord185.y ) - _Float3 ));
				float2 texCoord209 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner210 = ( 1.0 * _Time.y * float2( 0.2,0 ) + texCoord209);
				float2 texCoord193 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float smoothstepResult197 = smoothstep( -0.36 , 0.71 , ( ( 1.0 - texCoord193.y ) - _Float3 ));
				float4 temp_cast_0 = (0.46).xxxx;
				float4 temp_cast_1 = (0.1).xxxx;
				float2 texCoord201 = IN.texCoord0.xy * float2( 1.11,0.19 ) + float2( 0,0 );
				float2 panner202 = ( 1.0 * _Time.y * float2( 0.18,0.05 ) + texCoord201);
				float4 smoothstepResult203 = smoothstep( temp_cast_0 , temp_cast_1 , tex2D( _TextureSample1, panner202 ));
				float4 color220 = IsGammaSpace() ? float4(0.7528302,1,0.9946267,1) : float4(0.5269415,1,0.9878201,1);
				float2 texCoord214 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner215 = ( 1.0 * _Time.y * float2( -0.1,0 ) + texCoord214);
				float4 temp_cast_2 = (smoothstepResult197).xxxx;
				float4 temp_output_2_0_g1 = ( ( ( SampleGradient( gradient118, smoothstepResult175 ) * smoothstepResult189 ) + ( saturate( ( tex2D( _TextureSample0, panner210 ) - ( smoothstepResult197 * smoothstepResult203 ) ) ) * _HDR ) ) + ( color220 * saturate( ( tex2D( _TextureSample2, panner215 ) - temp_cast_2 ) ) ) );
				float4 appendResult4_g3 = (float4((temp_output_2_0_g1).rgb , ( (temp_output_2_0_g1).a * _Alpha )));
				
				float4 Color = appendResult4_g3;

				#if ETC1_EXTERNAL_ALPHA
					float4 alpha = SAMPLE_TEXTURE2D(_AlphaTex, sampler_AlphaTex, IN.texCoord0.xy);
					Color.a = lerp( Color.a, alpha.r, _EnableAlphaTexture);
				#endif

				#if defined(DEBUG_DISPLAY)
				SurfaceData2D surfaceData;
				InitializeSurfaceData(Color.rgb, Color.a, surfaceData);
				InputData2D inputData;
				InitializeInputData(positionWS.xy, half2(IN.texCoord0.xy), inputData);
				half4 debugColor = 0;

				SETUP_DEBUG_DATA_2D(inputData, positionWS, positionCS);

				if (CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
				{
					return debugColor;
				}
				#endif

				Color *= IN.color * unity_SpriteColor;
				return Color;
			}

			ENDHLSL
		}

		
		Pass
		{
			
            Name "Sprite Unlit Forward"
            Tags { "LightMode"="UniversalForward" }

			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM

			#define ASE_VERSION 19801
			#define ASE_SRP_VERSION 170004


			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile _ DEBUG_DISPLAY SKINNED_SPRITE

            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
            #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
            #define FEATURES_GRAPH_VERTEX

			#define SHADERPASS SHADERPASS_SPRITEFORWARD

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/Core2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging2D.hlsl"

			#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"


			sampler2D _Lava_Tex;
			sampler2D _Dist_Text;
			sampler2D _TextureSample0;
			sampler2D _TextureSample1;
			sampler2D _TextureSample2;
			CBUFFER_START( UnityPerMaterial )
			float _L_Min;
			float _L_Max;
			float _L_X_Pan;
			float _L_Y_Pan;
			float _D_Min;
			float _D_Max;
			float _D_X_Pan;
			float _D_Y_Pan;
			float _D_X_tile;
			float _D_Y_tile;
			float _Distorsion_Intensity;
			float _L_X_tile;
			float _L_Y_tile;
			float _Float3;
			float _HDR;
			float _Alpha;
			CBUFFER_END


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_SKINNED_VERTEX_INPUTS
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float3 positionWS : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if ETC1_EXTERNAL_ALPHA
				TEXTURE2D( _AlphaTex ); SAMPLER( sampler_AlphaTex );
				float _EnableAlphaTexture;
			#endif

			
			float4 SampleGradient( Gradient gradient, float time )
			{
				float3 color = gradient.colors[0].rgb;
				UNITY_UNROLL
				for (int c = 1; c < 8; c++)
				{
				float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, gradient.colorsLength-1));
				color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
				}
				#ifndef UNITY_COLORSPACE_GAMMA
				color = SRGBToLinear(color);
				#endif
				float alpha = gradient.alphas[0].x;
				UNITY_UNROLL
				for (int a = 1; a < 8; a++)
				{
				float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, gradient.alphasLength-1));
				alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
				}
				return float4(color, alpha);
			}
			

			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_SKINNED_VERTEX_COMPUTE( v );

				v.positionOS = UnityFlipSprite( v.positionOS, unity_SpriteProps.xy );

				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.positionOS;
				#else
					float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.positionOS = vertexValue;
				#else
					v.positionOS += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS);

				o.positionCS = vertexInput.positionCS;
				o.positionWS = vertexInput.positionWS;
				o.texCoord0 = v.uv0;
				o.color = v.color;

				return o;
			}

			half4 frag( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				float4 positionCS = IN.positionCS;
				float3 positionWS = IN.positionWS;

				Gradient gradient118 = NewGradient( 0, 4, 2, float4( 0.01323601, 0.6138443, 0.6377357, 0.06234836 ), float4( 0, 0.8117647, 0.7882353, 0.1470665 ), float4( 0.3301886, 0.8853303, 1, 0.2152895 ), float4( 0.8283019, 0.9919142, 1, 1 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float2 appendResult164 = (float2(_L_X_Pan , _L_Y_Pan));
				float2 appendResult144 = (float2(_D_X_Pan , _D_Y_Pan));
				float2 texCoord143 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult142 = (float2(_D_X_tile , _D_Y_tile));
				float2 panner146 = ( 1.0 * _Time.y * appendResult144 + ( float2( 0,0 ) + (texCoord143*appendResult142 + 0.0) ));
				float smoothstepResult140 = smoothstep( _D_Min , _D_Max , tex2D( _Dist_Text, panner146 ).r);
				float2 texCoord162 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult160 = (float2(_L_X_tile , _L_Y_tile));
				float2 panner169 = ( 1.0 * _Time.y * appendResult164 + ( ( smoothstepResult140 * _Distorsion_Intensity ) + (texCoord162*appendResult160 + 0.0) ));
				float smoothstepResult175 = smoothstep( _L_Min , _L_Max , tex2D( _Lava_Tex, panner169 ).r);
				float2 texCoord185 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float smoothstepResult189 = smoothstep( 0.12 , 0.21 , ( ( 1.0 - texCoord185.y ) - _Float3 ));
				float2 texCoord209 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner210 = ( 1.0 * _Time.y * float2( 0.2,0 ) + texCoord209);
				float2 texCoord193 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float smoothstepResult197 = smoothstep( -0.36 , 0.71 , ( ( 1.0 - texCoord193.y ) - _Float3 ));
				float4 temp_cast_0 = (0.46).xxxx;
				float4 temp_cast_1 = (0.1).xxxx;
				float2 texCoord201 = IN.texCoord0.xy * float2( 1.11,0.19 ) + float2( 0,0 );
				float2 panner202 = ( 1.0 * _Time.y * float2( 0.18,0.05 ) + texCoord201);
				float4 smoothstepResult203 = smoothstep( temp_cast_0 , temp_cast_1 , tex2D( _TextureSample1, panner202 ));
				float4 color220 = IsGammaSpace() ? float4(0.7528302,1,0.9946267,1) : float4(0.5269415,1,0.9878201,1);
				float2 texCoord214 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner215 = ( 1.0 * _Time.y * float2( -0.1,0 ) + texCoord214);
				float4 temp_cast_2 = (smoothstepResult197).xxxx;
				float4 temp_output_2_0_g1 = ( ( ( SampleGradient( gradient118, smoothstepResult175 ) * smoothstepResult189 ) + ( saturate( ( tex2D( _TextureSample0, panner210 ) - ( smoothstepResult197 * smoothstepResult203 ) ) ) * _HDR ) ) + ( color220 * saturate( ( tex2D( _TextureSample2, panner215 ) - temp_cast_2 ) ) ) );
				float4 appendResult4_g3 = (float4((temp_output_2_0_g1).rgb , ( (temp_output_2_0_g1).a * _Alpha )));
				
				float4 Color = appendResult4_g3;

				#if ETC1_EXTERNAL_ALPHA
					float4 alpha = SAMPLE_TEXTURE2D( _AlphaTex, sampler_AlphaTex, IN.texCoord0.xy );
					Color.a = lerp( Color.a, alpha.r, _EnableAlphaTexture );
				#endif


				#if defined(DEBUG_DISPLAY)
					SurfaceData2D surfaceData;
					InitializeSurfaceData(Color.rgb, Color.a, surfaceData);
					InputData2D inputData;
					InitializeInputData(positionWS.xy, half2(IN.texCoord0.xy), inputData);
					half4 debugColor = 0;

					SETUP_DEBUG_DATA_2D(inputData, positionWS, positionCS);

					if (CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
					{
						return debugColor;
					}
				#endif

				Color *= IN.color * unity_SpriteColor;
				return Color;
			}

			ENDHLSL
		}
		
        Pass
        {
			
            Name "SceneSelectionPass"
            Tags { "LightMode"="SceneSelectionPass" }

            Cull Off

            HLSLPROGRAM

			#define ASE_VERSION 19801
			#define ASE_SRP_VERSION 170004


			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile _ DEBUG_DISPLAY SKINNED_SPRITE

            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
            #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
            #define FEATURES_GRAPH_VERTEX

            #define SHADERPASS SHADERPASS_DEPTHONLY
			#define SCENESELECTIONPASS 1

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/Core2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"


			sampler2D _Lava_Tex;
			sampler2D _Dist_Text;
			sampler2D _TextureSample0;
			sampler2D _TextureSample1;
			sampler2D _TextureSample2;
			CBUFFER_START( UnityPerMaterial )
			float _L_Min;
			float _L_Max;
			float _L_X_Pan;
			float _L_Y_Pan;
			float _D_Min;
			float _D_Max;
			float _D_X_Pan;
			float _D_Y_Pan;
			float _D_X_tile;
			float _D_Y_tile;
			float _Distorsion_Intensity;
			float _L_X_tile;
			float _L_Y_tile;
			float _Float3;
			float _HDR;
			float _Alpha;
			CBUFFER_END


            struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_SKINNED_VERTEX_INPUTS
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

            int _ObjectId;
            int _PassValue;

			
			float4 SampleGradient( Gradient gradient, float time )
			{
				float3 color = gradient.colors[0].rgb;
				UNITY_UNROLL
				for (int c = 1; c < 8; c++)
				{
				float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, gradient.colorsLength-1));
				color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
				}
				#ifndef UNITY_COLORSPACE_GAMMA
				color = SRGBToLinear(color);
				#endif
				float alpha = gradient.alphas[0].x;
				UNITY_UNROLL
				for (int a = 1; a < 8; a++)
				{
				float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, gradient.alphasLength-1));
				alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
				}
				return float4(color, alpha);
			}
			

			VertexOutput vert(VertexInput v )
			{
				VertexOutput o = (VertexOutput)0;

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_SKINNED_VERTEX_COMPUTE(v);

				v.positionOS = UnityFlipSprite( v.positionOS, unity_SpriteProps.xy );

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.positionOS;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.positionOS = vertexValue;
				#else
					v.positionOS += vertexValue;
				#endif

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS);
				float3 positionWS = TransformObjectToWorld(v.positionOS);
				o.positionCS = TransformWorldToHClip(positionWS);

				return o;
			}

			half4 frag(VertexOutput IN) : SV_TARGET
			{
				Gradient gradient118 = NewGradient( 0, 4, 2, float4( 0.01323601, 0.6138443, 0.6377357, 0.06234836 ), float4( 0, 0.8117647, 0.7882353, 0.1470665 ), float4( 0.3301886, 0.8853303, 1, 0.2152895 ), float4( 0.8283019, 0.9919142, 1, 1 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float2 appendResult164 = (float2(_L_X_Pan , _L_Y_Pan));
				float2 appendResult144 = (float2(_D_X_Pan , _D_Y_Pan));
				float2 texCoord143 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult142 = (float2(_D_X_tile , _D_Y_tile));
				float2 panner146 = ( 1.0 * _Time.y * appendResult144 + ( float2( 0,0 ) + (texCoord143*appendResult142 + 0.0) ));
				float smoothstepResult140 = smoothstep( _D_Min , _D_Max , tex2D( _Dist_Text, panner146 ).r);
				float2 texCoord162 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult160 = (float2(_L_X_tile , _L_Y_tile));
				float2 panner169 = ( 1.0 * _Time.y * appendResult164 + ( ( smoothstepResult140 * _Distorsion_Intensity ) + (texCoord162*appendResult160 + 0.0) ));
				float smoothstepResult175 = smoothstep( _L_Min , _L_Max , tex2D( _Lava_Tex, panner169 ).r);
				float2 texCoord185 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float smoothstepResult189 = smoothstep( 0.12 , 0.21 , ( ( 1.0 - texCoord185.y ) - _Float3 ));
				float2 texCoord209 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner210 = ( 1.0 * _Time.y * float2( 0.2,0 ) + texCoord209);
				float2 texCoord193 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float smoothstepResult197 = smoothstep( -0.36 , 0.71 , ( ( 1.0 - texCoord193.y ) - _Float3 ));
				float4 temp_cast_0 = (0.46).xxxx;
				float4 temp_cast_1 = (0.1).xxxx;
				float2 texCoord201 = IN.ase_texcoord.xy * float2( 1.11,0.19 ) + float2( 0,0 );
				float2 panner202 = ( 1.0 * _Time.y * float2( 0.18,0.05 ) + texCoord201);
				float4 smoothstepResult203 = smoothstep( temp_cast_0 , temp_cast_1 , tex2D( _TextureSample1, panner202 ));
				float4 color220 = IsGammaSpace() ? float4(0.7528302,1,0.9946267,1) : float4(0.5269415,1,0.9878201,1);
				float2 texCoord214 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner215 = ( 1.0 * _Time.y * float2( -0.1,0 ) + texCoord214);
				float4 temp_cast_2 = (smoothstepResult197).xxxx;
				float4 temp_output_2_0_g1 = ( ( ( SampleGradient( gradient118, smoothstepResult175 ) * smoothstepResult189 ) + ( saturate( ( tex2D( _TextureSample0, panner210 ) - ( smoothstepResult197 * smoothstepResult203 ) ) ) * _HDR ) ) + ( color220 * saturate( ( tex2D( _TextureSample2, panner215 ) - temp_cast_2 ) ) ) );
				float4 appendResult4_g3 = (float4((temp_output_2_0_g1).rgb , ( (temp_output_2_0_g1).a * _Alpha )));
				
				float4 Color = appendResult4_g3;

				half4 outColor = half4(_ObjectId, _PassValue, 1.0, 1.0);
				return outColor;
			}

            ENDHLSL
        }

		
        Pass
        {
			
            Name "ScenePickingPass"
            Tags { "LightMode"="Picking" }

			Cull Off

            HLSLPROGRAM

			#define ASE_VERSION 19801
			#define ASE_SRP_VERSION 170004


			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile _ DEBUG_DISPLAY SKINNED_SPRITE

            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
            #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
            #define FEATURES_GRAPH_VERTEX

            #define SHADERPASS SHADERPASS_DEPTHONLY
			#define SCENEPICKINGPASS 1

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/Core2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

        	#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"


			sampler2D _Lava_Tex;
			sampler2D _Dist_Text;
			sampler2D _TextureSample0;
			sampler2D _TextureSample1;
			sampler2D _TextureSample2;
			CBUFFER_START( UnityPerMaterial )
			float _L_Min;
			float _L_Max;
			float _L_X_Pan;
			float _L_Y_Pan;
			float _D_Min;
			float _D_Max;
			float _D_X_Pan;
			float _D_Y_Pan;
			float _D_X_tile;
			float _D_Y_tile;
			float _Distorsion_Intensity;
			float _L_X_tile;
			float _L_Y_tile;
			float _Float3;
			float _HDR;
			float _Alpha;
			CBUFFER_END


            struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_SKINNED_VERTEX_INPUTS
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

            float4 _SelectionID;

			
			float4 SampleGradient( Gradient gradient, float time )
			{
				float3 color = gradient.colors[0].rgb;
				UNITY_UNROLL
				for (int c = 1; c < 8; c++)
				{
				float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, gradient.colorsLength-1));
				color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
				}
				#ifndef UNITY_COLORSPACE_GAMMA
				color = SRGBToLinear(color);
				#endif
				float alpha = gradient.alphas[0].x;
				UNITY_UNROLL
				for (int a = 1; a < 8; a++)
				{
				float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, gradient.alphasLength-1));
				alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
				}
				return float4(color, alpha);
			}
			

			VertexOutput vert(VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_SKINNED_VERTEX_COMPUTE(v);

				v.positionOS = UnityFlipSprite( v.positionOS, unity_SpriteProps.xy );

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.positionOS;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.positionOS = vertexValue;
				#else
					v.positionOS += vertexValue;
				#endif

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS);
				float3 positionWS = TransformObjectToWorld(v.positionOS);
				o.positionCS = TransformWorldToHClip(positionWS);

				return o;
			}

			half4 frag(VertexOutput IN ) : SV_TARGET
			{
				Gradient gradient118 = NewGradient( 0, 4, 2, float4( 0.01323601, 0.6138443, 0.6377357, 0.06234836 ), float4( 0, 0.8117647, 0.7882353, 0.1470665 ), float4( 0.3301886, 0.8853303, 1, 0.2152895 ), float4( 0.8283019, 0.9919142, 1, 1 ), 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
				float2 appendResult164 = (float2(_L_X_Pan , _L_Y_Pan));
				float2 appendResult144 = (float2(_D_X_Pan , _D_Y_Pan));
				float2 texCoord143 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult142 = (float2(_D_X_tile , _D_Y_tile));
				float2 panner146 = ( 1.0 * _Time.y * appendResult144 + ( float2( 0,0 ) + (texCoord143*appendResult142 + 0.0) ));
				float smoothstepResult140 = smoothstep( _D_Min , _D_Max , tex2D( _Dist_Text, panner146 ).r);
				float2 texCoord162 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult160 = (float2(_L_X_tile , _L_Y_tile));
				float2 panner169 = ( 1.0 * _Time.y * appendResult164 + ( ( smoothstepResult140 * _Distorsion_Intensity ) + (texCoord162*appendResult160 + 0.0) ));
				float smoothstepResult175 = smoothstep( _L_Min , _L_Max , tex2D( _Lava_Tex, panner169 ).r);
				float2 texCoord185 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float smoothstepResult189 = smoothstep( 0.12 , 0.21 , ( ( 1.0 - texCoord185.y ) - _Float3 ));
				float2 texCoord209 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner210 = ( 1.0 * _Time.y * float2( 0.2,0 ) + texCoord209);
				float2 texCoord193 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float smoothstepResult197 = smoothstep( -0.36 , 0.71 , ( ( 1.0 - texCoord193.y ) - _Float3 ));
				float4 temp_cast_0 = (0.46).xxxx;
				float4 temp_cast_1 = (0.1).xxxx;
				float2 texCoord201 = IN.ase_texcoord.xy * float2( 1.11,0.19 ) + float2( 0,0 );
				float2 panner202 = ( 1.0 * _Time.y * float2( 0.18,0.05 ) + texCoord201);
				float4 smoothstepResult203 = smoothstep( temp_cast_0 , temp_cast_1 , tex2D( _TextureSample1, panner202 ));
				float4 color220 = IsGammaSpace() ? float4(0.7528302,1,0.9946267,1) : float4(0.5269415,1,0.9878201,1);
				float2 texCoord214 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner215 = ( 1.0 * _Time.y * float2( -0.1,0 ) + texCoord214);
				float4 temp_cast_2 = (smoothstepResult197).xxxx;
				float4 temp_output_2_0_g1 = ( ( ( SampleGradient( gradient118, smoothstepResult175 ) * smoothstepResult189 ) + ( saturate( ( tex2D( _TextureSample0, panner210 ) - ( smoothstepResult197 * smoothstepResult203 ) ) ) * _HDR ) ) + ( color220 * saturate( ( tex2D( _TextureSample2, panner215 ) - temp_cast_2 ) ) ) );
				float4 appendResult4_g3 = (float4((temp_output_2_0_g1).rgb , ( (temp_output_2_0_g1).a * _Alpha )));
				
				float4 Color = appendResult4_g3;
				half4 outColor = _SelectionID;
				return outColor;
			}

            ENDHLSL
        }
		
	}
	CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
	FallBack "Hidden/Shader Graph/FallbackError"
	
	Fallback Off
}
/*ASEBEGIN
Version=19801
Node;AmplifyShaderEditor.RangedFloatNode;148;-3200,-288;Inherit;False;Property;_D_X_tile;D_X_tile;3;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;149;-3200,-176;Inherit;False;Property;_D_Y_tile;D_Y_tile;5;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;142;-2992,-272;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;143;-3024,-432;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScaleAndOffsetNode;145;-2768,-384;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;150;-2544,224;Inherit;False;Property;_D_Y_Pan;D_Y_Pan;9;0;Create;True;0;0;0;False;0;False;1;0.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;151;-2544,128;Inherit;False;Property;_D_X_Pan;D_X_Pan;6;0;Create;True;0;0;0;False;0;False;1;0.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;144;-2336,160;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;147;-2368,-400;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;146;-2160,-352;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;137;-1744,-432;Inherit;True;Property;_Dist_Text;Dist_Text;15;0;Create;True;0;0;0;False;0;False;-1;None;e263f2b690d315149bf53e3d26fdbe77;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;138;-1472,-240;Inherit;False;Property;_D_Min;D_Min;11;0;Create;True;0;0;0;False;0;False;0;-0.39;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;139;-1424,-160;Inherit;False;Property;_D_Max;D_Max;13;0;Create;True;0;0;0;False;0;False;0;0.42;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;171;-1616,240;Inherit;False;Property;_L_X_tile;L_X_tile;2;0;Create;True;0;0;0;False;0;False;1;0.65;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;161;-1616,352;Inherit;False;Property;_L_Y_tile;L_Y_tile;4;0;Create;True;0;0;0;False;0;False;1;0.22;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;140;-1184,-352;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;152;-1248,-96;Inherit;False;Property;_Distorsion_Intensity;Distorsion_Intensity;14;0;Create;True;0;0;0;False;0;False;0;0.03;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;160;-1408,256;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;162;-1440,96;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;193;-1776,1808;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;201;-1136,2160;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1.11,0.19;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;141;-928,-160;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;170;-1184,144;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;163;-960,304;Inherit;False;Property;_L_X_Pan;L_X_Pan;7;0;Create;True;0;0;0;False;0;False;1;-0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;165;-960,400;Inherit;False;Property;_L_Y_Pan;L_Y_Pan;8;0;Create;True;0;0;0;False;0;False;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;196;-1456,1984;Inherit;False;Property;_Float3;Float 0;19;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;194;-1424,1664;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;202;-800,2128;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.18,0.05;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;166;-784,128;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;164;-752,336;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;195;-1024,1600;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;200;-480,1904;Inherit;True;Property;_TextureSample1;Texture Sample 1;20;0;Create;True;0;0;0;False;0;False;-1;None;e263f2b690d315149bf53e3d26fdbe77;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;204;-192,1952;Inherit;False;Constant;_Float6;Float 6;20;0;Create;True;0;0;0;False;0;False;0.46;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;205;-176,2080;Inherit;False;Constant;_Float7;Float 7;20;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;209;-528,1184;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;199;-816,1952;Inherit;False;Constant;_Float5;Float 2;18;0;Create;True;0;0;0;False;0;False;0.71;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;198;-912,1856;Inherit;False;Constant;_Float4;Float 1;18;0;Create;True;0;0;0;False;0;False;-0.36;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;169;-576,176;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;185;-1248,848;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;203;64,1728;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;210;-208,1168;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;214;384,1968;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;197;-224,1424;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;174;-368,80;Inherit;True;Property;_Lava_Tex;Lava_Tex;0;0;Create;True;0;0;0;False;0;False;-1;None;e263f2b690d315149bf53e3d26fdbe77;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;177;-304,384;Inherit;False;Property;_L_Min;L_Min;10;0;Create;True;0;0;0;False;0;False;0;-0.43;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;168;-256,464;Inherit;False;Property;_L_Max;L_Max;12;0;Create;True;0;0;0;False;0;False;0;1.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;187;-896,800;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;215;688,1936;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;206;144,1472;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;182;-64,1056;Inherit;True;Property;_TextureSample0;Texture Sample 0;16;0;Create;True;0;0;0;False;0;False;-1;None;d92abc23ce954fd459e9a555ccc1a03b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SmoothstepOpNode;175;-64,160;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;-0.07;False;2;FLOAT;0.22;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;186;-496,640;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;190;-288,848;Inherit;False;Constant;_Float1;Float 1;18;0;Create;True;0;0;0;False;0;False;0.12;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;191;-224,976;Inherit;False;Constant;_Float2;Float 2;18;0;Create;True;0;0;0;False;0;False;0.21;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;208;288,832;Inherit;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;216;576,1568;Inherit;True;Property;_TextureSample2;Texture Sample 0;17;0;Create;True;0;0;0;False;0;False;-1;None;d92abc23ce954fd459e9a555ccc1a03b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GradientNode;118;-112,-224;Inherit;False;0;4;2;0.01323601,0.6138443,0.6377357,0.06234836;0,0.8117647,0.7882353,0.1470665;0.3301886,0.8853303,1,0.2152895;0.8283019,0.9919142,1,1;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.GradientSampleNode;119;192,80;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;189;112,496;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;212;496,1056;Inherit;False;Property;_HDR;HDR;21;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;217;816,1168;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;213;432,720;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;192;480,304;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;211;640,688;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;218;992,960;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;220;880,688;Inherit;False;Constant;_Color0;Color 0;22;0;Create;True;0;0;0;False;0;False;0.7528302,1,0.9946267,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleAddOpNode;183;848,192;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;219;1104,592;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;222;1264,176;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;178;1488,48;Inherit;False;Alpha Split;-1;;1;07dab7960105b86429ac8eebd729ed6d;0;1;2;COLOR;0,0,0,0;False;2;FLOAT3;0;FLOAT;6
Node;AmplifyShaderEditor.RangedFloatNode;113;1408,480;Inherit;False;Property;_Alpha;Alpha;1;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;181;1728,272;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;188;-928,1024;Inherit;False;Property;_Float0;Float 0;18;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;180;1760,32;Inherit;False;Alpha Merge;-1;;3;e0d79828992f19c4f90bfc29aa19b7a5;0;2;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;73;1136,64;Float;False;False;-1;3;UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI;0;1;New Amplify Shader;cf964e524c8e69742b1d21fbe2ebcc4a;True;Sprite Unlit Forward;0;1;Sprite Unlit Forward;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;12;all;0;False;True;2;5;False;;10;False;;3;1;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalForward;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;74;1136,64;Float;False;False;-1;3;UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI;0;1;New Amplify Shader;cf964e524c8e69742b1d21fbe2ebcc4a;True;SceneSelectionPass;0;2;SceneSelectionPass;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=SceneSelectionPass;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;75;1136,64;Float;False;False;-1;3;UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI;0;1;New Amplify Shader;cf964e524c8e69742b1d21fbe2ebcc4a;True;ScenePickingPass;0;3;ScenePickingPass;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Picking;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;72;1984,-16;Float;False;True;-1;3;UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI;0;15;Water;cf964e524c8e69742b1d21fbe2ebcc4a;True;Sprite Unlit;0;0;Sprite Unlit;4;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;True;12;all;0;False;True;2;5;False;;10;False;;3;1;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=Universal2D;False;False;0;;0;0;Standard;3;Vertex Position;1;0;Debug Display;0;0;External Alpha;0;0;0;4;True;True;True;True;False;;False;0
WireConnection;142;0;148;0
WireConnection;142;1;149;0
WireConnection;145;0;143;0
WireConnection;145;1;142;0
WireConnection;144;0;151;0
WireConnection;144;1;150;0
WireConnection;147;1;145;0
WireConnection;146;0;147;0
WireConnection;146;2;144;0
WireConnection;137;1;146;0
WireConnection;140;0;137;1
WireConnection;140;1;138;0
WireConnection;140;2;139;0
WireConnection;160;0;171;0
WireConnection;160;1;161;0
WireConnection;141;0;140;0
WireConnection;141;1;152;0
WireConnection;170;0;162;0
WireConnection;170;1;160;0
WireConnection;194;0;193;2
WireConnection;202;0;201;0
WireConnection;166;0;141;0
WireConnection;166;1;170;0
WireConnection;164;0;163;0
WireConnection;164;1;165;0
WireConnection;195;0;194;0
WireConnection;195;1;196;0
WireConnection;200;1;202;0
WireConnection;169;0;166;0
WireConnection;169;2;164;0
WireConnection;203;0;200;0
WireConnection;203;1;204;0
WireConnection;203;2;205;0
WireConnection;210;0;209;0
WireConnection;197;0;195;0
WireConnection;197;1;198;0
WireConnection;197;2;199;0
WireConnection;174;1;169;0
WireConnection;187;0;185;2
WireConnection;215;0;214;0
WireConnection;206;0;197;0
WireConnection;206;1;203;0
WireConnection;182;1;210;0
WireConnection;175;0;174;1
WireConnection;175;1;177;0
WireConnection;175;2;168;0
WireConnection;186;0;187;0
WireConnection;186;1;196;0
WireConnection;208;0;182;0
WireConnection;208;1;206;0
WireConnection;216;1;215;0
WireConnection;119;0;118;0
WireConnection;119;1;175;0
WireConnection;189;0;186;0
WireConnection;189;1;190;0
WireConnection;189;2;191;0
WireConnection;217;0;216;0
WireConnection;217;1;197;0
WireConnection;213;0;208;0
WireConnection;192;0;119;0
WireConnection;192;1;189;0
WireConnection;211;0;213;0
WireConnection;211;1;212;0
WireConnection;218;0;217;0
WireConnection;183;0;192;0
WireConnection;183;1;211;0
WireConnection;219;0;220;0
WireConnection;219;1;218;0
WireConnection;222;0;183;0
WireConnection;222;1;219;0
WireConnection;178;2;222;0
WireConnection;181;0;178;6
WireConnection;181;1;113;0
WireConnection;180;2;178;0
WireConnection;180;3;181;0
WireConnection;72;1;180;0
ASEEND*/
//CHKSM=0235F37B9F3B599C685B1428E0F9363709298AD8