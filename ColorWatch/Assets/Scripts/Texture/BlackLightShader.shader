Shader "Unlit/BlackLight"
{
    Properties
    {
        [NoScaleOffset] _MainTex("Texture", 2D) = "red" {}
        _Position("Position",Vector) = (0,0,0) //�v���C���[���W
        _Judge("Judge",Vector) = (0,0,0) //Judge�̍��W
        _MaxAngle("MaxAngle",float) = 20 //�ő�p�x
    }
        SubShader
        {
            Tags
            {
                "RenderType" = "Opaque"
            }

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                float _Radius;
                float3 _Judge;
                float3 _Position;
                float _MaxAngle;

                sampler2D _MainTex;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float3 worldPos : TEXCOORD1;
                    float2 uv : TEXCOORD0;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);

                    float3 lightVector = _Judge - _Position; //�v���C���[����Judge�ւ̃x�N�g��
                    float3 touchVector = i.worldPos - _Position; //�v���C���[����`��s�N�Z���ւ̃x�N�g��

                    //���ꂼ��̃x�N�g���̒������v�Z
                    float lengthA = length(lightVector);
                    float lengthB = length(touchVector);

                    float dotProduct = dot(lightVector, touchVector); //�x�N�g���̓��ς��v�Z
                    float rad = acos(dotProduct / (lengthA * lengthB)); //���ς���x�N�g���̊Ԃ̊p�x���v�Z(���W�A��)

                    float angle = degrees(rad); //���W�A�����x�ɕϊ�

                    float threshold_angle = _MaxAngle - angle; //�ő�p�x���猻�݂̕`��s�N�Z���̊p�x������(�ő�𒴂���ƃ}�C�i�X�ɂȂ�)
                    float threshold_length = lengthA - lengthB; //�W���b�W�܂ł̃x�N�g���̒�������`��s�N�Z���܂ł̃x�N�g������������(Judge�̏ꏊ�𒴂���ƃ}�C�i�X�ɂȂ�)

                    //�����̂������l��0�𒴂��Ă���(�͈͂Ɏ��܂��Ă�����)1��Ԃ�
                    float v = threshold_angle >= 0 && threshold_length >= 0 ? 1 : -1;

                    //0�ȏゾ������}�X�N����
                    clip(v);

                    return col;
                }
                ENDCG
            }
        }
}