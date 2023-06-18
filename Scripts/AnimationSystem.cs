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


    //����ŗՎ����u�I�ɐݒu
    public Image[] Character_Object;
    public Sprite[] CharaEmo0 = new Sprite[7];
    public Sprite[] CharaEmo1 = new Sprite[5];
    public Sprite[] CharaEmo2 = new Sprite[5];
    private Sprite[,] Character_emotion = new Sprite[3,7];

    //�w�i�I�u�W�F�N�g
    public Image BackGround;



    // Start is called before the first frame update
    void Start()
    {
        //�J�����̃I�u�W�F�N�g���擾�i�J�����ɂ͌��XMainCamera�^�O�����Ă���j
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
        //�Έڐ悪������Ώ����I��
        if (DATA.NS[Number].AnimationParameter.pos == null)
        {
            //�A�j���[�V�����I�������m
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

        //�A�j���[�V�����I�������m
        NovelSafetySystem.ActionSafety = true;
    }

    //�X�v���C�g(�w�i)�𓮂�������A�\�������肷��
    public void SpriteAnimation(ActionParameter AnimDATA)
    {
        //�X�v���C�g(�w�i)�����邩
        if (AnimDATA.Image)//����΁A�w�i�̕ύX�������s��
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

        //----*�v��*------
        //�X�v���C�g�����A�����A�E�ɓ�������
        //
        //����ׂ��ĂȂ��L�����́A�Â��F�ŕ\������
        //�L���������Ȃ��Ȃ�ꍇ�́A�t�F�[�h�A�E�g���o������
        //----------------

        //-----*�܂���ԍŏ��̏�ԂƂ�*--------
        //�E�X�v���C�g�͑S�I�u�W�F�N�g�t�F�[�h�A�E�g���Ă���i��O���肫�Ȃ̂ŃC���X�y�N�^�[�Őݒ�j
        //�E�S���ꏊ�͒���
        //
        //
        //
        //
        //
        //
        //-------------------------------------------

        //----*�I�u�W�F�N�g�̏�Ԃ͂ǂ��ɕۑ�����*-----
        //�E���������ۑ�����K�v�͂���̂�
        //
        //
        //
        //
        //
        //
        //
        //-------------------------------------------
        //���s�J�n���A�f�[�^�̑}�����Ԃɍ��킹�邽�߂̃f�B���C����
        DOVirtual.DelayedCall(0.1f, () =>
        {
            for (int i = 0; i < CharaDATA.Character.Length; i++)
            {
                var CS = CharaDATA.Character[i];

                //�L�������ړ�
                if ((int)CS.Position != 3)
                {
                    //�t�F�[�h�A�E�g�̏�Ԃ���ٓ�����ꍇ�A�ړ��A�j���[�V�����͂��Ȃ��ŁA�o���p�̏㏸���[�V����������
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
                else//�t�F�[�h�A�E�g�ł���΁A����ȑ��������
                {
                    Character_Object[(int)CS.Character].DOFade(0, 0.7f);
                    Character_Object[(int)CS.Character].GetComponent<RectTransform>().DOAnchorPosY(min_y, 1.5f);
                }


                //�L�����̕\�����ύX
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
