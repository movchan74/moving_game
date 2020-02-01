Shader "Johannes/Bottle_Shader"
{
    Properties
    {
        _MainTex ("RTex_Bottle", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        ZWrite Off

        LOD 100


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD2;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float4 screenUV = i.screenPos / i.screenPos.w; //perspective divide
                float ratio = _ScreenParams.y / _ScreenParams.x;

                float2 screenUVNorm = float2(screenUV.x, (screenUV.y - 0.5) * ratio + 0.5);

                // sample the texture
                float2 screenUVFinal = float2(screenUVNorm.x, screenUVNorm.y);
                fixed4 col = tex2D(_MainTex, screenUVFinal);
                //col = float4((screenUVNorm.x * gradient).xxx, 0);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
