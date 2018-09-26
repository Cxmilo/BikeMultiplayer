using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Screens")]
    public Image mainScreen;
    public Image selectAvatarScreen;
    public Image instructionsScreen;

    [Header("Main Screen Componetns")]
    public Sprite touchToContinue;
    public Sprite[] touchExtraImages;

    [Header("Avatar Selection Componets")]
    public RenderTexture maleTexture;
    public RenderTexture feMaleTexture;

    public Image btnPlayer_1;
    public Image btnPlayer_2;
    public Image btnPlayer_3;

    public RawImage renderP1;
    public RawImage renderP2;
    public RawImage renderP3;

    public Sprite maleButton;
    public Sprite femaleButton;



    private int isMaleP1 = 0;
    private int isMaleP2 = 0;
    private int isMaleP3 = 0;

    private IEnumerator MainLoop;

    public void Start()
    {
        MainLoop = PlayMainLoop();
        StartCoroutine(MainLoop);
    }

    private IEnumerator PlayMainLoop()
    {
        mainScreen.gameObject.SetActive(true);
        int i = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            mainScreen.sprite = touchToContinue;
            mainScreen.DOFade(1, 0.5f);
            yield return new WaitForSeconds(2.5f);
            mainScreen.DOFade(0, 0.5f);
            yield return new WaitForSeconds(0.5f);
            mainScreen.sprite = touchExtraImages[i];
            mainScreen.DOFade(1, 0.5f);
            yield return new WaitForSeconds(2.5f);
            mainScreen.DOFade(0, 0.5f);
            i = (i < touchExtraImages.Length -1) ? i + 1 : 0;

        }
    }


    public void OnMainScreenTouched ()
    {
        StopCoroutine(MainLoop);
        mainScreen.DOFade(0, 0.5f).OnComplete(delegate { mainScreen.gameObject.SetActive(false); });
        selectAvatarScreen.gameObject.SetActive(true);
        selectAvatarScreen.DOFade(1, 0.5f);

    }

    public void OnPlayerAvatarPresed (int playerIndex)
    {
        switch (playerIndex)
        {
            case 1:
                btnPlayer_1.sprite = (btnPlayer_1.sprite == maleButton) ? femaleButton : maleButton;
                isMaleP1 = (btnPlayer_1.sprite == maleButton) ? 1 : 0;
                renderP1.texture = (btnPlayer_1.sprite == maleButton) ? maleTexture : feMaleTexture;
                break;

            case 2:
                btnPlayer_2.sprite = (btnPlayer_2.sprite == maleButton) ? femaleButton : maleButton;
                isMaleP2 = (btnPlayer_2.sprite == maleButton) ? 1 : 0;
                renderP2.texture = (btnPlayer_2.sprite == maleButton) ? maleTexture : feMaleTexture;
                break;

            case 3:
                btnPlayer_3.sprite = (btnPlayer_3.sprite == maleButton) ? femaleButton : maleButton;
                isMaleP3 = (btnPlayer_3.sprite == maleButton) ? 1 : 0;
                renderP3.texture = (btnPlayer_3.sprite == maleButton) ? maleTexture : feMaleTexture;
                break;
        }
    }

    public void OnAvatarsSelected ()
    {
        selectAvatarScreen.DOFade(0,0.5f).OnComplete(delegate { selectAvatarScreen.gameObject.SetActive(false); });
        instructionsScreen.gameObject.SetActive(true);
        instructionsScreen.DOFade(1, 0.5f);
        Invoke("LoadGamePlayScene", 5);
    }

    void LoadGamePlayScene ()
    {
        SceneManager.LoadScene("GamePlayScene");
    }
}