using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PageTransition
{
    public int from;          // Starting tab
    public int to;            // Target tab
    public string pageFlip;   // Animation name (e.g., "chapterFlipRight" or "chapterFlipLeft")
}

public class JournalTabManager : MonoBehaviour
{
    public Animator Journal;                 // Reference to the Animator
    public List<PageTransition> pageTransitions; // List of all possible transitions

    public float bookOpeningDuration = 2f;   // Duration of the book-opening animation
    public float animationDuration = 1f;     // Duration of the page flip animation

    private int currentPage = 1;             // Current active tab

    private void Start()
    {
        // Start the book-opening animation and activate Tab 1 afterward
        StartCoroutine(OpenBookAndInitialize());
    }

    private IEnumerator OpenBookAndInitialize()
    {
        // Wait for the book-opening animation to complete
        yield return new WaitForSeconds(bookOpeningDuration);

        // Play the idle animation for Tab 1
        Journal.Play("idleOpen1", 0, 0f);
        currentPage = 1;

        Debug.Log("Initialized on Tab 1");
    }

    public void GoToPage(int targetPage)
    {
        if (currentPage == targetPage)
            return; // Already on the target page, no action required

        // Find the appropriate transition
        PageTransition transition = pageTransitions.Find(t => t.from == currentPage && t.to == targetPage);

        if (transition != null)
        {
            // Play the specified page flip animation
            Journal.Play(transition.pageFlip, 0, 0f);

            // Reset triggers to ensure smooth animation playback
            Journal.ResetTrigger("flipRight");
            Journal.ResetTrigger("flipLeft");

            if (transition.pageFlip == "chapterFlipRight")
            {
                Journal.SetTrigger("flipRight");
            }
            else if (transition.pageFlip == "chapterFlipLeft")
            {
                Journal.SetTrigger("flipLeft");
            }

            // Transition to the idle animation for the target tab
            StartCoroutine(TransitionToIdle(targetPage));
        }
        else
        {
            Debug.LogWarning($"No transition defined from {currentPage} to {targetPage}");
        }
    }

    private IEnumerator TransitionToIdle(int targetPage)
    {
        // Wait for the flip animation to complete
        yield return new WaitForSeconds(animationDuration);

        // Construct the idle animation name based on the target page
        string idleAnimationName = $"idleOpen{targetPage}";

        // Play the idle animation for the target tab
        Journal.Play(idleAnimationName, 0, 0f);

        // Log for debugging
        Debug.Log($"Transitioned to {idleAnimationName}");

        // Update the current page
        currentPage = targetPage;
    }
}
