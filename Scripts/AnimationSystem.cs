using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimationSystem : SingletonMonoBehaviourFast<AnimationSystem>
{
    GameObject cam;

    Vector3 STARTpos;
    float Transtime;

    public float max_y;
    public float min_y;


    //特例で臨時処置的に設置
    public Image[] Character_Object;
    public Sprite[] CharaEmo0 = new Sprite[7];
    public Sprite[] CharaEmo1 = new Sprite[5];
    public Sprite[] CharaEmo2 = new Sprite[5];
    private Sprite[,] Character_emotion = new Sprite[3,7];

    //背景オブジェクト
    public Image BackGround;



    // Start is called before the first frame update
    void Start()
    {
        //カメラのオブジェクトを取得（カメラには元々MainCameraタグがついている）
        cam = GameObject.FindWithTag("MainCamera");

        //Character_emotion[0] = CharaEmo0;
        //Character_emotion[1] = CharaEmo1;

        for (int i = 0; i < CharaEmo0.Length; i++)
        {
            //Debug.Log("Data["+i+"] = "+ CharaEmo0[i]);
            Character_emotion[0, i] = CharaEmo0[i];
            //Debug.Log("Data[" + i + "] = " + Character_emotion[0, i]);
        }

        for (int i = 0; i < CharaEmo1.Length; i++) Character_emotion[1, i] = CharaEmo1[i];
        for (int i = 0; i < CharaEmo2.Length; i++) Character_emotion[2, i] = CharaEmo2[i];

        Setup();
    }

    // Update is called once per frame
    public void Setup()
    {
        for(int i = 0;i < Character_Object.Length; i++)
        {
            Character_Object[i].color = new Color(1,1,1,0);
        }
    }

    public void Move(NovelTaker DATA, int Number)
    {
        //偏移先が無ければ処理終了
        if (DATA.NS[Number].AnimationParameter.pos == null)
        {
            //アニメーション終了を告知
            NovelSafetySystem.ActionSafety = true;

            //Debug.Log("ActionSystem Safety() = " + NovelSafetySystem.Safety());
            //Debug.Log("ActionSystem NovelSafety = " + NovelSafetySystem.NovelSafety);
            //Debug.Log("ActionSystem ActionSafety = " + NovelSafetySystem.ActionSafety);
            return;
        }

        STARTpos = cam.transform.position;
        Transtime = DATA.NS[Number].AnimationParameter.TransTime;
        Vector3 ENDpos = DATA.NS[Number].AnimationParameter.pos.position;
        ENDpos.z = -10;

        cam.transform.DOMove(ENDpos, Transtime);

        //アニメーション終了を告知
        NovelSafetySystem.ActionSafety = true;
    }

    //スプライト(背景)を動かしたり、表示したりする
    public void SpriteAnimation(ActionParameter AnimDATA)
    {
        //スプライト(背景)があるか
        if (AnimDATA.Image)//あれば、背景の変更処理を行う
        {
            BackGround.DOColor(Color.black, 0.75f);

            DOVirtual.DelayedCall(1, () =>
            {
                BackGround.sprite = AnimDATA.Image;

                BackGround.DOColor(AnimDATA.Color, 0.75f);
            }
            );
        }
        else if (BackGround.color == Color.white && AnimDATA.Color != Color.white)
        {
            BackGround.DOColor(AnimDATA.Color, 0.75f);
        }
    }

    public void CharacterAnimation(CharacterParameter CharaDATA)
    {

        //----*要件*------
        //スプライトを左、中央、右に動かせる
        //
        //しゃべってないキャラは、暗い色で表示する
        //キャラが居なくなる場合は、フェードアウト演出をする
        //----------------

        //-----*まず一番最初の状態とは*--------
        //・スプライトは全オブジェクトフェードアウトしている（例外ありきなのでインスペクターで設定）
        //・全員場所は中央
        //
        //
        //
        //
        //
        //
        //-------------------------------------------

        //----*オブジェクトの状態はどこに保存する*-----
        //・そもそも保存する必要はあるのか
        //
        //
        //
        //
        //
        //
        //
        //-------------------------------------------
        //実行開始時、データの挿入を間に合わせるためのディレイ処理
        DOVirtual.DelayedCall(0.1f, () =>
        {
            for (int i = 0; i < CharaDATA.Character.Length; i++)
            {
                var CS = CharaDATA.Character[i];

                //キャラを移動
                if ((int)CS.Position != 3)
                {
                    //フェードアウトの状態から異動する場合、移動アニメーションはしないで、出現用の上昇モーションを入れる
                    if (Character_Object[(int)CS.Character].color.a == 0)
                    {
                        Character_Object[(int)CS.Character].DOFade(1, 0.7f);
                        Character_Object[(int)CS.Character].GetComponent<RectTransform>().DOAnchorPosY(max_y, 1.5f);

                        if ((int)CS.Position == 0)
                        {
                            Character_Object[(int)CS.Character].GetComponent<RectTransform>().DOAnchorPosX(-350, 0);
                        }
                        else if ((int)CS.Position == 1)
                        {
                            Character_Object[(int)CS.Character].GetComponent<RectTransform>().DOAnchorPosX(0, 0);
                        }
                        else
                        {
                            Character_Object[(int)CS.Character].GetComponent<RectTransform>().DOAnchorPosX(350, 0);
                        }
                    }
                    else
                    {
                        if ((int)CS.Position == 0)
                        {
                            Character_Object[(int)CS.Character].GetComponent<RectTransform>().DOAnchorPosX(-350, 1);
                        }
                        else if ((int)CS.Position == 1)
                        {
                            Character_Object[(int)CS.Character].GetComponent<RectTransform>().DOAnchorPosX(0, 1);
                        }
                        else
                        {
                            Character_Object[(int)CS.Character].GetComponent<RectTransform>().DOAnchorPosX(350, 1);
                        }
                    }
                }
                else//フェードアウトであれば、特殊な操作を入れる
                {
                    Character_Object[(int)CS.Character].DOFade(0, 0.7f);
                    Character_Object[(int)CS.Character].GetComponent<RectTransform>().DOAnchorPosY(min_y, 1.5f);
                }


                //キャラの表情差分を変更
                if ((int)CS.Emotion != 0)
                {
                    Character_Object[(int)CS.Character].sprite = Character_emotion[((int)CS.Character), (int)CS.Emotion - 1];
                }
            }
        }
        );
    }



    public void While()
    {

    }

    public void Over(NovelTaker DATA)
    {
        STARTpos.z = -10;
        cam.transform.DOMove(STARTpos, Transtime);
    }
}
