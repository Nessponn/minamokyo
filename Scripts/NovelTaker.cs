using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovelTaker : MonoBehaviour
{
    [Space]
    public int NovelNumber_Debug;//�f�o�b�O�p�̃m�x���ԍ��w��B�K�v�Ȃ����private�������Ă��܂��Ă����͂Ȃ�
    [Space]
    public NovelClass[] NS;//�m�x���e�L�X�g�{��
    [Space]
    public Text[] Fonts;//�g�p����e�L�X�g�B�e�L�X�g���̂̓Q�[���I�u�W�F�N�g�Ƃ��Đ���ł���`�łȂ���΂Ȃ�Ȃ�

    private NovelTaker prevNovel;//�O�ɕ\���������m�x���e�C�J�[�B
    public NovelTaker nextNovel;//���ɕ\��������m�x���e�C�J�[�B��b���I���Ƃ��̉�b���A�N�e�B�u�ɂ���

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //�f�[�^�̎擾
        NovelTaker DATA = gameObject.GetComponent<NovelTaker>();

        //�f�[�^��NovelHistoria�ɑ���
        NovelHistoria.Historia.HistoriaSystem_Setup(DATA);
    }

    //�O�̃m�x���f�[�^���L�����Ă���
    public void prevMemory(NovelTaker DATA)
    {
        prevNovel = DATA;
    }

    // Update is called once per frame
    void Update()
    {

    }

}

