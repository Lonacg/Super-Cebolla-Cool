using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{

    public delegate void PressingPauseButton(); 
    public static event PressingPauseButton OnPressingPauseButton;

    public delegate void PressingContinueButton(); 
    public static event PressingContinueButton OnPressingContinueButton;


    public GameObject pauseView;
    public GameObject ingameView;
    void Awake()
    {
        pauseView.SetActive(false);
        //controlsView.SetActive(false);
    }



    public void OnPauseButton()
    {
        if (OnPressingPauseButton!=null)
            OnPressingPauseButton();
        StartCoroutine(FadeCanvasGroup(pauseView, from:0, to:1));
        StartCoroutine(FadeCanvasGroup(ingameView, from:1, to:0));
        Time.timeScale=0;

    }
    
    public void OnContinueButton()
    {
        if (OnPressingContinueButton!=null)
            OnPressingContinueButton();
        StartCoroutine(FadeCanvasGroup(pauseView, from:1, to:0));
        StartCoroutine(FadeCanvasGroup(ingameView, from:0, to:1));
        //controlsView.SetActive(false);
 
        Time.timeScale=1;
    }
    public void OnRetryButton()
    {
        // FONDO EN NEGRO DE CARGA DE PANTALLA
        SceneManager.LoadScene("SCCGameScene");
        Time.timeScale=1;
    }
    
    /*public void OnControlsButton()
    {
        //SI DA TIEMPO
        pauseView.SetActive(false);
        controlsView.SetActive(true);
        Time.timeScale=0;
    }*/


    public void OnQuitGameButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale=1;
    }

    IEnumerator FadeCanvasGroup(GameObject view, float from, float to)
    {
        CanvasGroup canvasGroup= view.GetComponent<CanvasGroup>();
        if(to>0)
            view.SetActive(true);
        float animationTime=0.25f;
        float elapsedTime=0;

        while(elapsedTime<=animationTime)
        {
            canvasGroup.alpha=Mathf.Lerp(from, to, elapsedTime/animationTime);
            elapsedTime+=Time.unscaledDeltaTime;
            yield return 0;
        }

        canvasGroup.alpha=to;
        if (to==0)
            view.SetActive(false);

    }




}
