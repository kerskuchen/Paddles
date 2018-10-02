using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuiOverlay : MonoBehaviour
{
    public Text scoreLeft;
    public Text scoreRight;
    public Button playButton;
    public Button exitButton;
    public Image menuOverlay;
    public Image screenTransitionFader;
    public MatchState matchState;

    public PaddleMover paddleLeft;
    public PaddleMover paddleRight;

    public Pong pong;
    public Text readySetGo;

    const float MENU_OVERLAY_DEFAULT_ALPHA = 0.7f;
    const float MENU_DEFAULT_FADE_DURATION = 0.2f;
    State state = State.MainMenu;
    Glower paddleLeftGlower;

    Coroutine lastCoroutine = null;

    enum State { InGame, MainMenu }
    enum Transition { ToGame, ToMainMenu, ToDesktop }

    void Start()
    {
        this.paddleLeftGlower = paddleLeft.gameObject.GetComponent<Glower>();
        this.scoreLeft.gameObject.SetActive(false);
        this.scoreRight.gameObject.SetActive(false);
        SetImageAlpha(menuOverlay, MENU_OVERLAY_DEFAULT_ALPHA);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (this.state == State.MainMenu)
            {
                FadeToDesktop();
            }
            else if (this.state == State.InGame)
            {
                FadeToMainMenu();
            }
        }
    }

    public void FadeToMainMenu()
    {
        if (this.lastCoroutine != null)
            StopCoroutine(this.lastCoroutine);

        paddleLeft.isPlayerControlled = false;
        paddleLeft.Reset();
        paddleRight.Reset();

        this.state = State.MainMenu;
        this.readySetGo.text = "";
        this.lastCoroutine = StartCoroutine(ScreenTransition(MENU_DEFAULT_FADE_DURATION, Transition.ToMainMenu));
    }

    public void FadeInGame()
    {
        if (this.lastCoroutine != null)
            StopCoroutine(this.lastCoroutine);

        paddleLeft.isPlayerControlled = true;
        paddleLeft.Reset();
        paddleRight.Reset();

        this.state = State.InGame;
        this.playButton.interactable = false;
        this.exitButton.interactable = false;
        this.readySetGo.text = "";
        this.lastCoroutine = StartCoroutine(ScreenTransition(MENU_DEFAULT_FADE_DURATION, Transition.ToGame));
    }

    public void FadeToDesktop()
    {
        if (this.lastCoroutine != null)
            StopCoroutine(this.lastCoroutine);
        this.playButton.interactable = false;
        this.exitButton.interactable = false;
        this.readySetGo.text = "";
        this.lastCoroutine = StartCoroutine(ScreenTransition(MENU_DEFAULT_FADE_DURATION, Transition.ToDesktop));
    }


    IEnumerator ScreenTransition(float fadeDuration, Transition transition)
    {
        // Fade out
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime / fadeDuration)
        {
            SetImageAlpha(this.screenTransitionFader, alpha);
            yield return null;
        }
        SetImageAlpha(this.screenTransitionFader, 1);

        switch (transition)
        {
            case Transition.ToDesktop:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
				// TODO(JaSc): Maybe show some dialog box with stacktrace here?
				Application.Quit();
#endif
                break;

            case Transition.ToGame:
                this.scoreLeft.gameObject.SetActive(true);
                this.scoreRight.gameObject.SetActive(true);

                this.playButton.gameObject.SetActive(false);
                this.exitButton.gameObject.SetActive(false);
                SetImageAlpha(this.menuOverlay, 0);
                break;

            case Transition.ToMainMenu:
                this.scoreLeft.gameObject.SetActive(false);
                this.scoreRight.gameObject.SetActive(false);

                this.playButton.gameObject.SetActive(true);
                this.exitButton.gameObject.SetActive(true);
                this.playButton.interactable = true;
                this.exitButton.interactable = true;
                SetImageAlpha(this.menuOverlay, MENU_OVERLAY_DEFAULT_ALPHA);

                // Reselect play button so that it is highlighted
                this.playButton.OnSelect(null);
                break;
        }
        matchState.ResetMatch();
        this.pong.ResetPosition();

        // Fade in
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime / fadeDuration)
        {
            SetImageAlpha(this.screenTransitionFader, alpha);
            yield return null;
        }
        SetImageAlpha(this.screenTransitionFader, 0);

        switch (transition)
        {
            case Transition.ToGame:
                yield return new WaitForSeconds(0.5f);
                this.readySetGo.text = "READY";
                this.readySetGo.GetComponent<FontGlower>().StartGlow();
                this.paddleLeftGlower.StartGlow();
                yield return new WaitForSeconds(0.5f);
                this.paddleLeftGlower.StartGlow();
                yield return new WaitForSeconds(0.5f);
                this.readySetGo.text = "SET";
                this.readySetGo.GetComponent<FontGlower>().StartGlow();
                this.paddleLeftGlower.StartGlow();
                yield return new WaitForSeconds(0.5f);
                this.paddleLeftGlower.StartGlow();
                yield return new WaitForSeconds(0.5f);
                this.paddleLeftGlower.StartGlow();
                this.readySetGo.text = "GO!";
                this.readySetGo.GetComponent<FontGlower>().StartGlow();
                this.paddleLeftGlower.StartGlow();
                this.pong.StartMoving();
                yield return new WaitForSeconds(1);
                this.readySetGo.text = "";
                break;
            case Transition.ToMainMenu:
                this.pong.ResetPosition();
                yield return new WaitForSeconds(0.3f);
                this.pong.StartMoving();
                break;
        }
    }


    void SetImageAlpha(Image image, float newAlpha)
    {
        Color temp = image.color;
        temp.a = newAlpha;
        image.color = temp;
    }
}
