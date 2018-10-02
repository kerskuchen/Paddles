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

    private const float MENU_OVERLAY_DEFAULT_ALPHA = 0.7f;
    private State state = State.MainMenu;

    enum State { InGame, MainMenu }
    enum Transition { ToGame, ToMainMenu, ToDesktop }

    void Start()
    {
        this.scoreLeft.gameObject.SetActive(false);
        this.scoreRight.gameObject.SetActive(false);
        SetImageAlpha(menuOverlay, MENU_OVERLAY_DEFAULT_ALPHA);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (this.state == State.MainMenu)
                StartCoroutine(ScreenTransition(0.1f, Transition.ToDesktop));
            if (this.state == State.InGame)
                StartCoroutine(ScreenTransition(0.1f, Transition.ToMainMenu));
        }
    }


    public void FadeInGame()
    {
        StartCoroutine(ScreenTransition(0.1f, Transition.ToGame));
    }

    public void FadeToDesktop()
    {
        StartCoroutine(ScreenTransition(0.1f, Transition.ToDesktop));
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
                SetImageAlpha(this.menuOverlay, MENU_OVERLAY_DEFAULT_ALPHA);

                // Reselect play button so that it is highlighted
                EventSystem.current.SetSelectedGameObject(this.playButton.gameObject);
                this.playButton.OnSelect(null);
                break;
        }

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
                this.state = State.InGame;
                break;
            case Transition.ToMainMenu:
                this.state = State.MainMenu;
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
