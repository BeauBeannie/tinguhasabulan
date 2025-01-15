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
    public Animator RevealAnimator;          // Reference to the Animator for the reveal animation frames
    
    public GameObject RevealAnimationObject; // The GameObject holding the reveal animation
    
    public GameObject ContentContainer;      // Parent GameObject holding all tab content

    public List<PageTransition> pageTransitions; // List of all possible transitions

    public float bookOpeningDuration = 2f;   // Duration of the book-opening animation
    public float animationDuration = 1f;     // Duration of the page flip animation

    private int currentPage = 1;             // Current active tab
    

    private void Start()
    {
        // Disable the RevealAnimation GameObject at the start
        RevealAnimationObject.SetActive(false);

        // Disable all tab content at the start
        HideAllContent();

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

        // Turn the RevealAnimation GameObject back on
        RevealAnimationObject.SetActive(true);

        // Show the content for Tab 1
        ShowContentForTab(1);

        // Now trigger the content reveal animation after switching tabs
        PlayRevealAnimation();

        Debug.Log("Initialized on Tab 1");
    }

    public void GoToPage(int targetPage)
    {
        if (currentPage == targetPage)
            return; // Already on the target page, no action required
        
        // Hide current tab content
        HideContentForTab(currentPage);

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

        // Show the content for the target tab
        ShowContentForTab(targetPage);

        // Now trigger the content reveal animation after switching tabs
        PlayRevealAnimation();

        // Update the current page
        currentPage = targetPage;
    }

    public void PlayRevealAnimation()
    {
        // Play the reveal animation as an overlay
        RevealAnimator.Play("contentReveal", 0, 0f);

        // Optionally, log the action
        Debug.Log("Playing content reveal animation.");
    }

    private void ShowContentForTab(int tabNumber)
    {
        // Activate the content for the specified tab
        Transform tabContent = ContentContainer.transform.Find($"Tab{tabNumber}Content");
        if (tabContent != null)
        {
            tabContent.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Content for Tab {tabNumber} not found.");
        }
    }

    private void HideContentForTab(int tabNumber)
    {
        // Deactivate the content for the specified tab
        Transform tabContent = ContentContainer.transform.Find($"Tab{tabNumber}Content");
        if (tabContent != null)
        {
            tabContent.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Content for Tab {tabNumber} not found.");
        }
    }

    private void HideAllContent()
    {
        // Disable all tab content at the start
        foreach (Transform child in ContentContainer.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
