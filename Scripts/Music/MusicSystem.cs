using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicSystem : SingletonMonoBehaviourFast<MusicSystem>
{
    public AudioSource BGM;
    public AudioSource SE;

    private float BGM_vol;
    private float SE_vol;

    private void Start()
    {
        BGM_vol = BGM.volume;
        SE_vol = SE.volume;


        //Debug.Log(BGM_vol);
    }
    //BGM�̓��͂�����΁ABGM�̍����ւ����s��
    public void PlayBGM(AudioClip AC)
    {
        if (BGM.clip)
        {
            BGM.DOFade(0, 1);

            DOVirtual.DelayedCall(1, ()=>
            {
                //BGM�̌�������
                BGM.clip = AC;
                BGM.Play();
                BGM.DOFade(BGM_vol, 0);
            }
            );
        }
        else
        {
            BGM.clip = AC;
            BGM.Play();
            BGM.DOFade(BGM_vol, 0);
        }
    }

    //SE�̓��͂�����΁ASE��炷
    public void PlaySE(AudioClip AC)
    {
        //SE�̌�������
        SE.PlayOneShot(AC);
    }

    public void StopBGM()
    {
        //BGM���X�g�b�v
        BGM.DOFade(0, 1);

        DOVirtual.DelayedCall(1, () =>//��
        {
            BGM.Stop();
        }
        );
    }
}
