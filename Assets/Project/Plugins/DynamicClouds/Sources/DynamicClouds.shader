Shader "DynamicClouds"
{
    Properties
    {
        //_skyColor("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex("Noise", 2D) = "gray" {}
        _Clouds("Top Clouds", Range(0,2)) = 0.5
        _HorizonClouds("Horizon Clouds", Range(0,2)) = 0.5
        _CloudThickness("Cloud Thickness", Range(0,10)) = 1
        _Turbulence("Turbulence", Float) = 0.35
        _ShapeChangeSpeed("Shape Change Speed", Float) = 0.02
        _BumpMap("Normalmap", 2D) = "bump" {}
        _BumpMap2("Normalmap2", 2D) = "bump" {}
        _Offset("Offset", Vector) = (0, 0, 0, 0)
        _Scale("Scale", Range(0.2,1)) = 0.4
        _SunDir("Sun Dir", Vector) = (0.5, 0.5, 0, 0)
        _cloudColorLight("cloud Light", Color) = (1,1,1,1)
        _cloudColorDark("cloud Dark", Color) = (0.5,0.5,0.5,1)
        _fogColor("fog Color", Color) = (1,1,1,1)
        //_fogStart("fog Start", Range(0,1)) = 0
        //_fogEnd("fog End", Range(0,1)) = 0
        _fog("fog", Range(0.01,1)) = 0.01
        _FadeOut("fade out", Range(0.01,1)) = 0.25
    }
    SubShader
    {
        Tags {"Queue" = "Transparent-1" "RenderType"="Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            //fixed4 _skyColor;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _NoiseTex;
            half _Clouds;
            half _HorizonClouds;
            half _CloudThickness;
            half _Turbulence;
            half _ShapeChangeSpeed;
            sampler2D _BumpMap;
            sampler2D _BumpMap2;
            float2 _Offset;
            half _Scale;
            float2 _SunDir;
            fixed4 _cloudColorLight;
            fixed4 _cloudColorDark;
            fixed4 _fogColor;
            //half _fogStart;
            //half _fogEnd;
            half _fog;
            half _FadeOut;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                //float3 sundir = float3(0, 0, 1);
                //acos(dot(float3(i.uv.x, i.uv.y,), sundir));
                float2 sundir = i.uv.xy - _SunDir.xy;

                half2 distancexy = (i.uv-0.5) * 2;// -0.5;
                /*distancexy.x *= distancexy.x;
                distancexy.y *= distancexy.y;
                half distance = saturate((distancexy.x + distancexy.y) *2);*/
                half distance = saturate(length(distancexy));
                /*fixed4 col = fixed4(distance, distance, distance,1);
                return col; */     
                half cloudfactor = _Clouds * (1 - distance) + _HorizonClouds * distance;
                half cloudfactor2 = cloudfactor;
                //i.uv += _Time.x * 0.01; //wind
                i.uv = i.uv + _Offset;
                i.uv += (tex2D(_NoiseTex, i.uv * 20 + _Time.x * _Turbulence).rg - 0.5) * 0.007; //turbulence
                i.uv /= _Scale;
                float2 shapeOffset = float2(_Time.x * _ShapeChangeSpeed, 0);
                float3 cloudtex = tex2D(_MainTex, i.uv - shapeOffset);
                cloudtex.g = tex2D(_MainTex, i.uv + shapeOffset).g;
                float factor = tex2D(_MainTex, i.uv * 0.4 + float2(0,_Time.x * _ShapeChangeSpeed * 1.67)).b; //(_SinTime.x + 1) * 0.5;
                float clouds = cloudtex.r * factor + cloudtex.g * (1- factor); //shape change
                half lightst = saturate((clouds - (1 - cloudfactor))* _CloudThickness*0.5);
                cloudfactor = saturate((clouds - (1- cloudfactor))* _CloudThickness);
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv - shapeOffset));
                half3 tnormal2 = UnpackNormal(tex2D(_BumpMap2, i.uv + shapeOffset));
                tnormal = tnormal * factor + tnormal2 * (1 - factor);
                //tnormal.z *= distance*0.6 + 0.5; //lowering the z before normalizing enhances bump (at horizon bump remains the same)
                //tnormal.z *= cloudfactor2 *cloudfactor/(1.1-clouds);
                //tnormal.z *= 2 * cloudfactor2;
                tnormal.z *= cloudfactor2 + 0.5 *cloudfactor2 * cloudfactor / (1.1 - clouds);
                tnormal.z *= length(sundir * 2)+0.1;
                tnormal = normalize(tnormal);
                //float lighting = (tnormal.y + 3) * 0.25;
                //half2 clouddir = tnormal.xy;// *4;
                //half cloudlength = clouddir.x * clouddir.x - clouddir.y * clouddir.y;
                /*half cloudlength2 = min(cloudlength * 2, 1 - (1 - cloudlength) * (1 - cloudlength) * 2);
                clouddir.x *= cloudlength2 / cloudlength;
                clouddir.y *= cloudlength2 / cloudlength;
                cloudlength = cloudlength2;*/
                //clouddir.x = -clouddir.x;
                //float lighting = saturate(length(sundir - clouddir * 1));
                //half3 clouddir3 = half3(clouddir.x, clouddir.y, sqrt(1 - cloudlength));
                /*if (clouddir3.z < 0)
                {
                    clouddir3.z = 0;
                    clouddir3 = normalize(clouddir3);
                }*/
                //half3 sundir3 = half3(sundir3.x, sundir3.y, sqrt(1 - sundir3.x * sundir3.x - sundir3.y * sundir3.y));

                half lighting = acos(dot(tnormal, sundir));
                lighting /= 3.14159265;
                lighting = saturate(lighting * 4 - 1.5);// *5 - 2);

                ////sundir *= 2;
                //half sundist = length(sundir);
                //if (sundist > 1)
                //{
                //    sundir *= (2 - sundist) / sundist;
                //}
                //half3 sundir3 = half3(sundir.x, sundir.y, sqrt(1 - sundir.x * sundir.x - sundir.y * sundir.y));
                //if (sundist > 1)
                //{
                //    sundir3.z = -sundir3.z;
                //}
                //half lighting = acos(dot(tnormal, sundir3));
                //lighting /= 3.14159265;
                //lighting = saturate(lighting*3-0.1);// *4 - 1.5);// *5 - 2);

                //return lighting;
                //lighting = min(lighting, saturate(length(sundir - clouddir*0.75)));
                half seethrough = saturate(1-length(sundir));
                seethrough *= seethrough;
                seethrough *= seethrough;
                seethrough = (seethrough * seethrough + seethrough) * 0.5;
                seethrough *= seethrough*1.5;
                lighting = saturate(lighting + seethrough * (1 - lightst));
                //lighting = lighting * cloudfactor + seethrough*(1 - cloudfactor);
                //lighting = 1 - lighting;
                //lighting *= cloudfactor;
                //lighting = 1 - lighting;
                //lighting = (lighting + 1) * 0.5;
                float4 cloudcolor = _cloudColorLight * lighting + _cloudColorDark * (1- lighting);
                fixed4 col = cloudcolor;// _skyColor* (1 - cloudfactor) + cloudcolor * cloudfactor;
                col.a = cloudfactor;
                //s-curve:
                float factorL = cloudfactor * cloudfactor;
                float factorH = 1 - cloudfactor;
                factorH *= factorH;
                factorH = 1 - factorH;
                col.a = factorL * (1 - cloudfactor) + factorH * cloudfactor;

                //factor = distance *distance;
                //factor = saturate((factor -1 + _fog) / _fog);
                /*distance = 1 - distance;
                distance *= distance;
                distance = 1 - distance;*/
                factor = pow(distance, 1/ _FadeOut);
                col.a *= 1 - factor;

                //factor = _fogEnd * distance + _fogStart * (1 - distance);
                factor = pow(distance, 1 / _fog);
                _fogColor.rgb = _fogColor.rgb * factor + col.rgb * (1 - factor);
                col.rgb = _fogColor.rgb*0.5f + max(_fogColor.rgb, col.rgb)*0.5;

                return col;
                //return fixed4(tnormal.x, tnormal.y, tnormal.z, 1);
            }
            ENDCG
        }
    }
}
