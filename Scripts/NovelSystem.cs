using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;//�{�^���Ɖ�ʃ^�b�`�ɃR���t���N�g�𐶂܂Ȃ�����
using TMPro;

public class NovelSystem : SingletonMonoBehaviourFast<NovelSystem>
{
    private Text[] NovelText;//�g�p����t�H���g�I�u�W�F�N�g�̃X�^�b�N��p
    private int TextNumber;//���݂̕����̈ʒu�ԍ��@�i�[�ϐ�

    private int FontNumber;
    private NovelClass[] NovelTexts;
    private int Novelnumber;//���݂̃e�L�X�g�̔ԍ�

    private bool stop;

    //��b�̃V�[�����J�n���ꂽ����A��b�f�[�^�������ɂ����Ă�����
    public void Setup(NovelTaker DATA)
    {
        //��������e�L�X�g�̔z������o���A�X�e�[�^�X�����L�̃R���[�`���Ŕ��΂�����
        Novelnumber = DATA.NovelNumber_Debug;

        //�g�p����e�L�X�g�t�H���g��NovelText�ɓK�p����
        NovelText = DATA.Fonts;

        //�ēx�\���O�ɕ����̓��e����x���Z�b�g
        for (int i = 0; i < NovelText.Length; i++)
        {
            //Debug.Log("NovelText = " + NovelText[i].text);

            NovelText[i].text = "";
        }
    }

    //�������Ƃ���͌�Őݒ��b�̂Ƃ����C�ӂ̕b���ɕς���悤�ɕύX����
    public IEnumerator While(NovelTaker DATA, int Number)
    {
        //���̃e�L�X�g�������p���̂łȂ����
        //��{�e�L�X�g�̓��e��������ۂɂ���
        if (!DATA.NS[Number].NovelParameter.ResidualText)
        {
            for (int i = 0; i < NovelText.Length; i++)
            {
                NovelText[i].text = "";
            }
            //�q�X�g���[�̃v���n�u�𐶐�����
            HistoryAdd(NovelText);
        }

        //�������͂���Ă��Ȃ���΁A�����ŏ������I��
        //�����āA���ɉ�b�̃f�[�^�������Ă��Ă���������E�B���h�E�������i�������j
        if (DATA.NS[Number].NovelParameter.Text == "")
        {
            Stop();
            yield break;
        }
        else
        {
            if (stop)
            {
                ReStart();
                yield return new WaitForSeconds(0.3f);
            }
        }

        //��b�̊J�n���͂��Ȃ���Ă��Ȃ���΁A��b�̊J�n����
        if (!NovelHistoria.Historia.Talking)
        {
            //��ʂ̕\���i��ʂ̓m�x���o�͖��߂����������̂ݎg�p���邽�߁A�����ɋL�q�j
            NovelHistoria.Historia.WindowCanvas.DOFade(1f, 1);//��
            DOVirtual.DelayedCall(1, () =>//��
            {
                //��b�̊J�n
                NovelHistoria.Historia.Talking = true;
            }
            );
            yield return new WaitForSeconds(1f);//��

        }
        //���O��\��
        NovelHistoria.Historia.NameText.text = DATA.NS[Number].NovelParameter.Name;

        //�ǂݍ��ޕ����̈ʒu���ŏ��ɖ߂�
        TextNumber = 0;

        //�t�H���g�t���O�̏�����
        FontNumber = 0;

        //�ҋ@���Ԃ̌v�Z


        //�e�L�X�g���ꕶ���Â\�����Ă���
        while (TextNumber < DATA.NS[Number].NovelParameter.Text.Length)
        {
            //�t�H���g��F�̈Ⴄ���̂��������ނɂ�
            //���ꂱ��Text���̂��Ⴄ�I�u�W�F�N�g�ő��݂�����K�v������
            //�R�}���h���͈ʒu�ō�������

            //�q�X�g���[�I�𒆂�I�v�V�������̓|�[�Y�������s��
            yield return new WaitUntil(() => !NovelHistoria.Historia.Paused);

            //�A�N�V�������͖��߂�����Ύ��s����
            //if (AP.pos != null) ActionSystemMaster.Instance.ActionStart(AP.pos, AP.TransTime);

            //�R�}���h���͂̊m�F
            if (DATA.NS[Number].NovelParameter.Text[TextNumber].ToString() == "'")
            {
                TextNumber++;//�ꕶ������Q��
                //������x�R�}���h���͂��m�F����܂ő�����
                while (DATA.NS[Number].NovelParameter.Text[TextNumber].ToString() != "'")
                {
                    if (DATA.NS[Number].NovelParameter.Text[TextNumber].ToString() == "F")
                    {
                        TextNumber++;//�ꕶ������Q��

                        //�w��̐��l��FontNumber�ɐݒ肷��
                        FontNumber = int.Parse(DATA.NS[Number].NovelParameter.Text[TextNumber].ToString());

                        TextNumber++;//�ꕶ������Q��(���̎��_�ŁA���̖��߂������̓R�}���h���͏I���̕���������)
                    }
                }

                //while���E�o��
                TextNumber++;//�ꕶ������Q��(�R�}���h�I���p��'���Q�Ƃ��Ȃ��悤�ɂ��邽��)
            }

            for (int i = 0; i < NovelText.Length; i++)
            {
                if (FontNumber == i)
                {
                    //���s���߂́A���̃t�H���g���ɂ͓K�p����Ȃ����߁A�����Ŏ��O�Ɏ�������

                    //Debug.Log("1 = " + NovelText[i]);
                    //Debug.Log("2 = " + DATA.NS[Number].NovelParameter.Text[TextNumber]);
                    //Debug.Log("3 = " + i);

                    //�P�����ǉ�
                    TextAdd(NovelText[i], DATA.NS[Number].NovelParameter.Text[TextNumber], i);
                }
                else
                {
                    //���s���ꂽ�Ƃ��Ƀo�O���Ĉꕶ���󔒂������\��������������ς肻���������I�I�I�I�I
                    //�u�A�v����͂��������R�}���h�󔒃o�O�������������A���s�̎��ɋ󔒂͖����Ȃ��悤�ɐݒ肵����ꏏ�ɒ����Ă��̂ŁA����������Ƌ�_�ɂ͉��s�R�[�h�Ɠ����R�[�h���p�����Ă���H�H�H�i�悭�킩���j
                    //Mac���ƁAWindows�ł�" "��"?@"�ɂȂ��Ă��܂�
                    if (DATA.NS[Number].NovelParameter.Text[TextNumber] == '\n')
                    {
                        for (int j = 0; j < NovelText.Length; j++)
                        {
                            //���s�ǉ�
                            if (FontNumber != j) TextAdd(NovelText[j], '\n', j);
                        }
                    }
                    else
                    {
                        //�󔒂��P�����ǉ�
                        TextAdd(NovelText[i], "�@", i);
                    }
                }
            }
            TextNumber++;//�ǉ�������1����

            if (!NovelHistoria.Historia.Pressed) yield return new WaitForSeconds(0.06f);
        }
        //��b�����ɐi�܂Ȃ��悤�A������x�A�N���b�N�����v����
        NovelHistoria.Historia.Pressed = false;


        //�I�������o�����߂�����΁A�I�������o��
        if (DATA.NS[Number].NovelParameter.ChoicesObject)
        {
            string[] str = new string[2];
            str[0] = DATA.NS[Number].NovelParameter.Branch1;
            str[1] = DATA.NS[Number].NovelParameter.Branch2;
            //Debug.Log(str[1]);

            ChoiceButtons.Instance.NovelBranch(DATA,Number,str);
        }
        else
        {
            //SafetySystem��Novel��true�ɂ��Ď���������ւ���
            NovelSafetySystem.NovelSafety = true;
        }

        
        //Debug.Log("NovelSystem Safety() = " + NovelSafetySystem.Safety());
        //Debug.Log("NovelSystem NovelSafety = " + NovelSafetySystem.NovelSafety);
        //Debug.Log("NovelSystem ActionSafety = " + NovelSafetySystem.ActionSafety);
    }

    //�r���ŕ����񂪂Ȃ����̂��������ɂ��������b�E�B���h�E������
    private void Stop()
    {
        //Debug.Log("Stop");
        stop = true;
        NovelSafetySystem.NovelSafety = true;
        NovelHistoria.Historia.WindowCanvas.DOFade(0, 0.3f);
    }

    //Stop�ŏ������E�B���h�E���m�x���X�e�[�^�X�͂��̂܂܂ɂ�����x�\������
    private void ReStart()
    {
        //Debug.Log("Restart");
        stop = false;
        NovelHistoria.Historia.WindowCanvas.DOFade(1, 0.3f);
    }

    public void Over()
    {
        //��b�̏I��
        NovelHistoria.Historia.Talking = false;

        NovelHistoria.Historia.WindowCanvas.DOFade(0, 1f);
    }






    public void HistoryAdd(Text[] Tx)
    {
        NovelHistoria_Setting.Instance.HistoryTake(Tx);
    }

    public void TextAdd(Text tx, char cx, int FontNumber)
    {
        tx.text += cx;
        NovelHistoria_Setting.Instance.HistoryAdd(cx, FontNumber);
    }
    public void TextAdd(Text tx, string cx, int FontNumber)
    {
        tx.text += cx;
        NovelHistoria_Setting.Instance.HistoryAdd(cx, FontNumber);
    }
}
