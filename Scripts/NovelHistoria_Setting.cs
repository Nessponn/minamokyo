using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NovelHistoria_Setting : SingletonMonoBehaviourFast<NovelHistoria_Setting>
{
    //����20�`30���炢�̉�b���X�g�b�N���Ă���

    //private List<GameObject> History;//���b�Z�[�W�i���o�[�A�g�p�t�H���g��������

    public GameObject HistoryContent;
    public GameObject Prefab;
    public GameObject TextPrefab;

    private GameObject StackHistoria;//�������Ƀq�X�g���[�Ƃ��Ēǉ�������\��̃I�u�W�F�N�g

    //���b�Z�[�W���i�[����
    //���A���^�C���`���ł̕����ǉ�����
    //�\�������e�L�X�g�ƁA�t�H���g�̔ԍ��������ɂ���
    public void HistoryTake(Text[] Tx)
    {
        //�q�X�g���[�ɒǉ�����e�L�X�g�p�ɐV�����I�u�W�F�N�g�𐶐�����
        StackHistoria = Instantiate(Prefab);

        //�v���n�u�̎q�I�u�W�F�N�g����Text�R���|�[�l���g��ǂݍ���Ńe�L�X�g��}��
        //�g�p�����t�H���g�̐������e�L�X�g�̐��𑝂₷

        for (int i = 0; i < Tx.Length; i++)
        {
            //�t�H���g�̐������I�u�W�F�N�g���Z�b�g
            GameObject Obj = Instantiate(TextPrefab);
            Obj.transform.parent = StackHistoria.transform;

            //�t�H���g���Z�b�g
            Obj.GetComponent<Text>().font = Tx[i].font;
            Obj.GetComponent<Text>().fontSize = Tx[i].fontSize;
        }

        StackHistoria.transform.parent = HistoryContent.transform;

        //�}��������Ńv���n�u�̈ʒu�ƃX�P�[���𒲐�
        var pos = StackHistoria.GetComponent<RectTransform>();
        StackHistoria.GetComponent<RectTransform>().anchoredPosition = new Vector3(pos.position.x, pos.position.y, 0);

        StackHistoria.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    //�ۑ�@���̂܂܂ł͂P�����ǉ����邽�тɃv���n�u������Ă��܂��B�܂Ƃ߂ăf�[�^�𑗂邩�P�����Âf�[�^�𑗂��@�ɂ��Ĉꊇ�Ńf�[�^�𑗂�`���ɂ��邩�H
    //������ɂ��Ă��A�l�^�o���h�~�p�̐ݒ肪�ł���悤�ɂ���

    //�܂��A�P�����Âǉ��܂��͈�C�ɒǉ��̐ݒ���Q�[�����ɓK�p����ꍇ�ł��݊������ێ�����@�\���쐬����

    //�P�����Âǉ�
    public void HistoryAdd(char cx, int FontNum)
    {
        //Debug.Log(StackHistoria.transform.GetChild(FontNum + 1).gameObject.GetComponent<Text>().text);
        StackHistoria.transform.GetChild(FontNum + 1).gameObject.GetComponent<Text>().text += cx;


        //�e�L�X�g���e�A�g�p�t�H���g���e�����肵�����
        //HistoryContent�ɒǉ�����
    }
    public void HistoryAdd(string cx, int FontNum)
    {
        StackHistoria.transform.GetChild(FontNum + 1).gameObject.GetComponent<Text>().text += cx;


        //�e�L�X�g���e�A�g�p�t�H���g���e�����肵�����
        //HistoryContent�ɒǉ�����
    }
    //��C�ɒǉ�
    private void HistoryAddAll()
    {

    }

    //�Q�[����ʂ���q�X�g���[�̉�ʂɈړ�����
    public void HistoryShow()
    {

    }

    //�q�X�g���[�̉�ʂ���Q�[����ʂɖ߂�
    public void HistoryDisShow()
    {

    }
}
