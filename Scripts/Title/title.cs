using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class title : SingletonMonoBehaviourFast<title>
{
    public string GameScene;

    public Image TitleLogo;
    public Button button;

    public Image Background;


    private void Start()
    {
        button.gameObject.SetActive(false);

        TitleLogo.DOFade(1, 2);

        DOVirtual.DelayedCall(3f, () =>
        {
            button.gameObject.SetActive(true);
        }
        );
    }

    public void GameStart()
    {
        button.gameObject.SetActive(false);

        Background.DOFade(1, 2);

        DOVirtual.DelayedCall(2f, () =>
        {
            //ƒQ[ƒ€‰æ–Ê‚ÉˆÚ“®

            SceneManager.LoadScene(GameScene);
        }
        );
    }
}
