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
    //BGMの入力があれば、BGMの差し替えを行う
    public void PlayBGM(AudioClip AC)
    {
        if (BGM.clip)
        {
            BGM.DOFade(0, 1);

            DOVirtual.DelayedCall(1, ()=>
            {
                //BGMの交換処理
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

    //SEの入力があれば、SEを鳴らす
    public void PlaySE(AudioClip AC)
    {
        //SEの交換処理
        SE.PlayOneShot(AC);
    }

    public void StopBGM()
    {
        //BGMをストップ
        BGM.DOFade(0, 1);

        DOVirtual.DelayedCall(1, () =>//★
        {
            BGM.Stop();
        }
        );
    }
}
