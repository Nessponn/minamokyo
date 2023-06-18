using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;//ボタンと画面タッチにコンフリクトを生まないため
using TMPro;

public class NovelSystem : SingletonMonoBehaviourFast<NovelSystem>
{
    private Text[] NovelText;//使用するフォントオブジェクトのスタック専用
    private int TextNumber;//現在の文字の位置番号　格納変数

    private int FontNumber;
    private NovelClass[] NovelTexts;
    private int Novelnumber;//現在のテキストの番号

    private bool stop;

    //会話のシーンが開始された直後、会話データがここにもってこられる
    public void Setup(NovelTaker DATA)
    {
        //ここからテキストの配列を取り出し、ステータスを下記のコルーチンで発火させる
        Novelnumber = DATA.NovelNumber_Debug;

        //使用するテキストフォントをNovelTextに適用する
        NovelText = DATA.Fonts;

        //再度表示前に文字の内容を一度リセット
        for (int i = 0; i < NovelText.Length; i++)
        {
            //Debug.Log("NovelText = " + NovelText[i].text);

            NovelText[i].text = "";
        }
    }

    //★つけたところは後で設定一秒のところを任意の秒数に変えるように変更する
    public IEnumerator While(NovelTaker DATA, int Number)
    {
        //次のテキストを引き継ぐのでなければ
        //基本テキストの内容をからっぽにする
        if (!DATA.NS[Number].NovelParameter.ResidualText)
        {
            for (int i = 0; i < NovelText.Length; i++)
            {
                NovelText[i].text = "";
            }
            //ヒストリーのプレハブを生成する
            HistoryAdd(NovelText);
        }

        //何も入力されていなければ、ここで処理を終了
        //そして、次に会話のデータが入っていてもいったんウィンドウを消す（未実装）
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

        //会話の開始入力がなされていなければ、会話の開始処理
        if (!NovelHistoria.Historia.Talking)
        {
            //画面の表示（画面はノベル出力命令があった時のみ使用するため、ここに記述）
            NovelHistoria.Historia.WindowCanvas.DOFade(1f, 1);//★
            DOVirtual.DelayedCall(1, () =>//★
            {
                //会話の開始
                NovelHistoria.Historia.Talking = true;
            }
            );
            yield return new WaitForSeconds(1f);//★

        }
        //名前を表示
        NovelHistoria.Historia.NameText.text = DATA.NS[Number].NovelParameter.Name;

        //読み込む文字の位置を最初に戻す
        TextNumber = 0;

        //フォントフラグの初期化
        FontNumber = 0;

        //待機時間の計算


        //テキストを一文字づつ表示していく
        while (TextNumber < DATA.NS[Number].NovelParameter.Text.Length)
        {
            //フォントや色の違うものを差し込むには
            //それこそText自体を違うオブジェクトで存在させる必要がある
            //コマンド入力位置で差し込む

            //ヒストリー選択中やオプション中はポーズ処理を行う
            yield return new WaitUntil(() => !NovelHistoria.Historia.Paused);

            //アクション入力命令があれば実行する
            //if (AP.pos != null) ActionSystemMaster.Instance.ActionStart(AP.pos, AP.TransTime);

            //コマンド入力の確認
            if (DATA.NS[Number].NovelParameter.Text[TextNumber].ToString() == "'")
            {
                TextNumber++;//一文字先を参照
                //もう一度コマンド入力を確認するまで続ける
                while (DATA.NS[Number].NovelParameter.Text[TextNumber].ToString() != "'")
                {
                    if (DATA.NS[Number].NovelParameter.Text[TextNumber].ToString() == "F")
                    {
                        TextNumber++;//一文字先を参照

                        //指定の数値をFontNumberに設定する
                        FontNumber = int.Parse(DATA.NS[Number].NovelParameter.Text[TextNumber].ToString());

                        TextNumber++;//一文字先を参照(この時点で、次の命令もしくはコマンド入力終了の文字が来る)
                    }
                }

                //while文脱出後
                TextNumber++;//一文字先を参照(コマンド終了用の'を参照しないようにするため)
            }

            for (int i = 0; i < NovelText.Length; i++)
            {
                if (FontNumber == i)
                {
                    //改行命令は、他のフォント文には適用されないため、ここで自前に実装する

                    //Debug.Log("1 = " + NovelText[i]);
                    //Debug.Log("2 = " + DATA.NS[Number].NovelParameter.Text[TextNumber]);
                    //Debug.Log("3 = " + i);

                    //１文字追加
                    TextAdd(NovelText[i], DATA.NS[Number].NovelParameter.Text[TextNumber], i);
                }
                else
                {
                    //改行されたときにバグって一文字空白があく可能性が高い←やっぱりそうだった！！！！！
                    //「、」を入力した時もコマンド空白バグが発生したが、改行の時に空白は明けないように設定したら一緒に直ってたので、もしかすると句点には改行コードと同じコードが用いられている？？？（よくわからん）
                    //Macだと、Windowsでの" "は"?@"になってしまう
                    if (DATA.NS[Number].NovelParameter.Text[TextNumber] == '\n')
                    {
                        for (int j = 0; j < NovelText.Length; j++)
                        {
                            //改行追加
                            if (FontNumber != j) TextAdd(NovelText[j], '\n', j);
                        }
                    }
                    else
                    {
                        //空白を１文字追加
                        TextAdd(NovelText[i], "　", i);
                    }
                }
            }
            TextNumber++;//追加したら1増加

            if (!NovelHistoria.Historia.Pressed) yield return new WaitForSeconds(0.06f);
        }
        //会話が次に進まないよう、もう一度、クリックを強要する
        NovelHistoria.Historia.Pressed = false;


        //選択肢を出す命令があれば、選択肢を出す
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
            //SafetySystemのNovelをtrueにして次送りを解禁する
            NovelSafetySystem.NovelSafety = true;
        }

        
        //Debug.Log("NovelSystem Safety() = " + NovelSafetySystem.Safety());
        //Debug.Log("NovelSystem NovelSafety = " + NovelSafetySystem.NovelSafety);
        //Debug.Log("NovelSystem ActionSafety = " + NovelSafetySystem.ActionSafety);
    }

    //途中で文字列がないものが来た時にいったん会話ウィンドウを消す
    private void Stop()
    {
        //Debug.Log("Stop");
        stop = true;
        NovelSafetySystem.NovelSafety = true;
        NovelHistoria.Historia.WindowCanvas.DOFade(0, 0.3f);
    }

    //Stopで消したウィンドウをノベルステータスはそのままにもう一度表示する
    private void ReStart()
    {
        //Debug.Log("Restart");
        stop = false;
        NovelHistoria.Historia.WindowCanvas.DOFade(1, 0.3f);
    }

    public void Over()
    {
        //会話の終了
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
