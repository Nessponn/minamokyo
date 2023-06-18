using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovelTaker : MonoBehaviour
{
    [Space]
    public int NovelNumber_Debug;//デバッグ用のノベル番号指定。必要なければprivateか消してしまっても問題はない
    [Space]
    public NovelClass[] NS;//ノベルテキスト本体
    [Space]
    public Text[] Fonts;//使用するテキスト。テキスト自体はゲームオブジェクトとして制御できる形でなければならない

    private NovelTaker prevNovel;//前に表示させたノベルテイカー。
    public NovelTaker nextNovel;//次に表示させるノベルテイカー。会話が終わるとこの会話をアクティブにする

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //データの取得
        NovelTaker DATA = gameObject.GetComponent<NovelTaker>();

        //データをNovelHistoriaに送る
        NovelHistoria.Historia.HistoriaSystem_Setup(DATA);
    }

    //前のノベルデータを記憶しておく
    public void prevMemory(NovelTaker DATA)
    {
        prevNovel = DATA;
    }

    // Update is called once per frame
    void Update()
    {

    }

}

