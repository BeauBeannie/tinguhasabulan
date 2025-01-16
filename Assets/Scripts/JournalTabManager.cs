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

    //private int currentPage = 1;             // Current active tab


    private int currentTab = 1;              // Current active tab
    private int currentPageInTab = 1;        // Current active page in the current tab
    private int totalPagesInTab = 1;         // Total pages in the current tab

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
        
        currentTab = 1;
        currentPageInTab = 1;
        totalPagesInTab = GetTotalPagesInTab(currentTab);

        // Show the content for Tab 1
        ShowContentForTab(currentTab, currentPageInTab);

        // Turn the RevealAnimation GameObject back on
        RevealAnimationObject.SetActive(true);

        // Now trigger the content reveal animation after switching tabs
        PlayRevealAnimation();

        Debug.Log("Initialized on Tab 1");
    }

    public void GoToPage(int targetTab)
    {
        if (currentTab == targetTab)
            return; // Already on the target page, no action required
        
        // Hide current tab content
        HideContentForTab(currentTab, currentPageInTab);

        // Find the appropriate transition
        PageTransition transition = pageTransitions.Find(t => t.from == currentTab && t.to == targetTab);

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
            StartCoroutine(TransitionToIdle(targetTab));
        }
        else
        {
            Debug.LogWarning($"No transition defined from {currentTab} to {targetTab}");
        }
    }


    private IEnumerator TransitionToIdle(int targetTab)
    {
        // Wait for the flip animation to complete
        yield return new WaitForSeconds(animationDuration);

        // Construct the idle animation name based on the target page
        string idleAnimationName = $"idleOpen{targetTab}";

        // Play the idle animation for the target tab
        Journal.Play(idleAnimationName, 0, 0f);

        // Log for debugging
        Debug.Log($"Transitioned to {idleAnimationName}");

        // Switch to the new tab and reset to the first page
        currentTab = targetTab;
        currentPageInTab = 1;
        totalPagesInTab = GetTotalPagesInTab(targetTab);

        // Show the new page with reveal animation
        //StartCoroutine(ShowContentWithReveal(currentTab, currentPageInTab));

        // Show the content for the target tab
        ShowContentForTab(currentTab, currentPageInTab);

        // Now trigger the content reveal animation after switching tabs
        PlayRevealAnimation();
    
    }

    public void FlipPageRight()
    {
        if (currentPageInTab < totalPagesInTab)
        {
            // Hide the current page
            HideContentForTab(currentTab, currentPageInTab);

            // Increment to the next page
            currentPageInTab++;

            // Play the page flip right animation
            Journal.Play("pageFlipRight", 0, 0f);

            // Show the new page with reveal animation
            StartCoroutine(ShowContentWithReveal(currentTab, currentPageInTab));

            Debug.Log($"Flipped to page {currentPageInTab} of Tab {currentTab}");
        }
        else
        {
            Debug.LogWarning("Already on the last page of the tab.");
        }
    }

    public void FlipPageLeft()
    {
        if (currentPageInTab > 1)
        {
            // Hide the current page
            HideContentForTab(currentTab, currentPageInTab);

            // Decrement to the previous page
            currentPageInTab--;

            // Play the page flip left animation
            Journal.Play("pageFlipLeft", 0, 0f);

            // Show the new page with reveal animation
            StartCoroutine(ShowContentWithReveal(currentTab, currentPageInTab));

            Debug.Log($"Flipped to page {currentPageInTab} of Tab {currentTab}");
        }
        else
        {
            Debug.LogWarning("Already on the first page of the tab.");
        }
    }

    public void PlayRevealAnimation()
    {
        // Ensure the reveal animation object is active
        if (!RevealAnimationObject.activeSelf)
        {
            RevealAnimationObject.SetActive(true);
        }

        // Play the reveal animation
        RevealAnimator.Play("contentReveal", 0, 0f);

        Debug.Log("Playing content reveal animation.");
    }
    private void ShowContentForTab(int tabNumber, int pageNumber)
    {
        // Find the specific page within the tab
        Transform pageContent = ContentContainer.transform.Find($"Tab{tabNumber}Content/Page{pageNumber}");
        if (pageContent != null)
        {
            pageContent.gameObject.SetActive(true); // Activate the page

        }
        else
        {
            Debug.LogWarning($"Content for Tab {tabNumber}, Page {pageNumber} not found.");
        }
    }

    private IEnumerator ShowContentWithReveal(int tabNumber, int pageNumber)
    {
        // Wait for the page flip animation to complete
        yield return new WaitForSeconds(0.5f);

        // Show the content
        ShowContentForTab(tabNumber, pageNumber);

        // Play the content reveal animation
        PlayRevealAnimation();
    }

    private void HideContentForTab(int tabNumber, int pageNumber)
    {
        // Find the specific page within the tab
        Transform pageContent = ContentContainer.transform.Find($"Tab{tabNumber}Content/Page{pageNumber}");
        if (pageContent != null)
        {
            pageContent.gameObject.SetActive(false); // Deactivate the page
        }
        else
        {
            Debug.LogWarning($"Content for Tab {tabNumber}, Page {pageNumber} not found.");
        }
    }


    private void HideAllContent()
    {
        // Disable all tab content at the start
        foreach (Transform tab in ContentContainer.transform)
        {
            foreach (Transform page in tab)
            {
                page.gameObject.SetActive(false);
            }
        }
    }

    private int GetTotalPagesInTab(int tabNumber)
    {
        // Count the number of pages in the tab by checking the hierarchy
        Transform tabContent = ContentContainer.transform.Find($"Tab{tabNumber}Content");
        if (tabContent != null)
        {
            return tabContent.childCount;
        }
        else
        {
            Debug.LogWarning($"Tab {tabNumber} not found.");
            return 0;
        }
    }
}
