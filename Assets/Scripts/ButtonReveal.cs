using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReveal : MonoBehaviour
{
    public Animator cutsceneAnimator; // Reference to the cutscene Animator
    public CanvasGroup buttonCanvasGroup; // CanvasGroup on the Button
    public float fadeDuration = 1f; // Time to complete the fade-in

    void Start()
    {
        // Ensure the button is hidden and not interactive at the start
        buttonCanvasGroup.alpha = 0;
        buttonCanvasGroup.interactable = false;
        buttonCanvasGroup.blocksRaycasts = false;

        // Start checking for the cutscene to finish
        StartCoroutine(CheckCutsceneEnd());
    }

    IEnumerator CheckCutsceneEnd()
    {
        // Wait for the cutscene animation to finish
        while (cutsceneAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || cutsceneAnimator.IsInTransition(0))
        {
            yield return null; // Keep waiting while the animation is playing
        }

        // Cutscene is finished, fade in the button
        StartCoroutine(FadeInButton());
    }

    IEnumerator FadeInButton()
    {
        float elapsedTime = 0f;

        // Enable button interaction after fading in
        buttonCanvasGroup.interactable = true;
        buttonCanvasGroup.blocksRaycasts = true;

        // Gradually fade in the button
        while (elapsedTime < fadeDuration)
        {
            buttonCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it's fully visible
        buttonCanvasGroup.alpha = 1;
    }
}
