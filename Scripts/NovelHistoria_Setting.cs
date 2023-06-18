using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NovelHistoria_Setting : SingletonMonoBehaviourFast<NovelHistoria_Setting>
{
    //直近20～30ぐらいの会話をストックしておく

    //private List<GameObject> History;//メッセージナンバー、使用フォント数をメモ

    public GameObject HistoryContent;
    public GameObject Prefab;
    public GameObject TextPrefab;

    private GameObject StackHistoria;//発言中にヒストリーとして追加をする予定のオブジェクト

    //メッセージを格納する
    //リアルタイム形式での文字追加方式
    //表示したテキストと、フォントの番号を引数にする
    public void HistoryTake(Text[] Tx)
    {
        //ヒストリーに追加するテキスト用に新しくオブジェクトを生成する
        StackHistoria = Instantiate(Prefab);

        //プレハブの子オブジェクトからTextコンポーネントを読み込んでテキストを挿入
        //使用したフォントの数だけテキストの数を増やす

        for (int i = 0; i < Tx.Length; i++)
        {
            //フォントの数だけオブジェクトをセット
            GameObject Obj = Instantiate(TextPrefab);
            Obj.transform.parent = StackHistoria.transform;

            //フォントをセット
            Obj.GetComponent<Text>().font = Tx[i].font;
            Obj.GetComponent<Text>().fontSize = Tx[i].fontSize;
        }

        StackHistoria.transform.parent = HistoryContent.transform;

        //挿入した先でプレハブの位置とスケールを調整
        var pos = StackHistoria.GetComponent<RectTransform>();
        StackHistoria.GetComponent<RectTransform>().anchoredPosition = new Vector3(pos.position.x, pos.position.y, 0);

        StackHistoria.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    //課題　このままでは１文字追加するたびにプレハブが作られてしまう。まとめてデータを送るか１文字づつデータを送る手法にして一括でデータを送る形式にするか？
    //いずれにしても、ネタバレ防止用の設定ができるようにする

    //また、１文字づつ追加または一気に追加の設定をゲーム中に適用する場合でも互換性を維持する機構を作成する

    //１文字づつ追加
    public void HistoryAdd(char cx, int FontNum)
    {
        //Debug.Log(StackHistoria.transform.GetChild(FontNum + 1).gameObject.GetComponent<Text>().text);
        StackHistoria.transform.GetChild(FontNum + 1).gameObject.GetComponent<Text>().text += cx;


        //テキスト内容、使用フォント内容が決定した後に
        //HistoryContentに追加する
    }
    public void HistoryAdd(string cx, int FontNum)
    {
        StackHistoria.transform.GetChild(FontNum + 1).gameObject.GetComponent<Text>().text += cx;


        //テキスト内容、使用フォント内容が決定した後に
        //HistoryContentに追加する
    }
    //一気に追加
    private void HistoryAddAll()
    {

    }

    //ゲーム画面からヒストリーの画面に移動する
    public void HistoryShow()
    {

    }

    //ヒストリーの画面からゲーム画面に戻る
    public void HistoryDisShow()
    {

    }
}
