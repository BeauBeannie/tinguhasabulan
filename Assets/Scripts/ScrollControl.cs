using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollControl : MonoBehaviour
{
    public Animator animationController;    // Animation reference
    public CanvasGroup textCanvasGroup;     // The text or panel to fade in
    public float delayAfterAnimation = 1f;  // Delay before fade
    public float fadeDuration = 1f;         // Fade-in duration

    private bool hasStartedFade = false;

    void Start()
    {
        if (textCanvasGroup != null)
        {
            textCanvasGroup.alpha = 0f;     // Fully transparent at start
            textCanvasGroup.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!hasStartedFade && animationController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            hasStartedFade = true;
            StartCoroutine(FadeInAfterDelay());
        }
    }

    IEnumerator FadeInAfterDelay()
    {
        yield return new WaitForSeconds(delayAfterAnimation);

        if (textCanvasGroup != null)
        {
            textCanvasGroup.gameObject.SetActive(true);

            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                textCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            textCanvasGroup.alpha = 1f; // Ensure full opacity at the end
        }
    }
}
