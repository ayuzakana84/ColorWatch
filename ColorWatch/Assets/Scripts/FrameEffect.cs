using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FrameEffect : MonoBehaviour
{
    [SerializeField] VideoPlayer frameEffect;

    private int effectCount = 0;

    public void PlayEffect()
    {
        frameEffect.Play(); //�G�t�F�N�g���Đ�
        effectCount++; //�ǂ��������Ă��鐔�������₷
    }

    public void StopEffect()
    {
        if (effectCount > 0)
            effectCount--; //�ǂ�������̂���߂����������炷

        if (effectCount == 0) //�N�ɂ��ǂ��������Ă��Ȃ���
            frameEffect.Stop(); //�G�t�F�N�g���~�߂�
    }
}
