using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject Buttons;      //Holds all the buttons

    public List<PageTransition> pageTransitions; // List of all possible transitions
    public List<Button> tabButtons; // Add references to all tab buttons here

    private bool isAnimating = false;

    public float bookOpeningDuration = 2f;   // Duration of the book-opening animation
    public float animationDuration = 1f;     // Duration of the page flip animation

    //private int currentPage = 1;             // Current active tab

    

    private int currentTab = 1;              // Current active tab
    private int currentPageInTab = 1;        // Current active page in the current tab
    private int totalPagesInTab = 1;         // Total pages in the current tab

    [SerializeField] private float fadeDuration = 0.5f;   //control the fade in of the content

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
        SetButtonsVisible(false); // Disable buttons during initialization

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

        SetButtonsVisible(true); // Re-enable buttons after initialization

        Debug.Log("Initialized on Tab 1");
    }

    public void GoToPage(int targetTab)
    {
        if (isAnimating || currentTab == targetTab)
            return; // Already on the target page, no action required
        
        isAnimating = true;
        SetButtonsVisible(false); //hide them buttons during animations

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
            isAnimating = false;
            SetButtonsVisible(true); //pops the buttons back up
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


        // Show the content for the target tab
        ShowContentForTab(currentTab, currentPageInTab);

        // Now trigger the content reveal animation after switching tabs
        PlayRevealAnimation();

        isAnimating = false;
        SetButtonsVisible(true);
    
    }

    public void FlipPageRight()
    {
        if (isAnimating || currentPageInTab >= totalPagesInTab)
            return;
        
        isAnimating = true;
        SetButtonsVisible(false);

        // Hide the current page
        HideContentForTab(currentTab, currentPageInTab);

        // Increment to the next page
        currentPageInTab++;

        // Play the page flip right animation
        Journal.Play("pageFlipRight", 0, 0f);

        // Show the new page and transition back to idle animation
        StartCoroutine(ShowContentWithIdleAnimation(currentTab, currentPageInTab));

        Debug.Log($"Flipped to page {currentPageInTab} of Tab {currentTab}");
        
    }

    public void FlipPageLeft()
    {
        if (isAnimating || currentPageInTab <= 1)
            return;
        
        isAnimating = true;
        SetButtonsVisible(false);

        // Hide the current page
        HideContentForTab(currentTab, currentPageInTab);

        // Decrement to the previous page
        currentPageInTab--;

        // Play the page flip left animation
        Journal.Play("pageFlipLeft", 0, 0f);


        // Show the new page and transition back to idle animation
        StartCoroutine(ShowContentWithIdleAnimation(currentTab, currentPageInTab));

        Debug.Log($"Flipped to page {currentPageInTab} of Tab {currentTab}");
   
    }

    private IEnumerator ShowContentWithIdleAnimation(int tabNumber, int pageNumber)
    {
        // Wait for the page flip animation to complete
        yield return new WaitForSeconds(0.5f);

        // Show the content for the page
        ShowContentForTab(tabNumber, pageNumber);

        // Transition back to the idleOpen animation for the current tab
        string idleAnimationName = $"idleOpen{tabNumber}";
        Journal.Play(idleAnimationName, 0, 0f);

        isAnimating = false;
        SetButtonsVisible(true);
        Debug.Log($"Returned to {idleAnimationName} after flipping pages.");
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
            PlayRevealAnimation();
            StartCoroutine(FadeInContent(pageContent, fadeDuration)); //Fade in overcontent

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

        isAnimating = false;
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

    private IEnumerator FadeInContent(Transform pageContent, float duration)
    {
        // Ensure the page has a CanvasGroup
        CanvasGroup canvasGroup = pageContent.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = pageContent.gameObject.AddComponent<CanvasGroup>();
        }

        // Set the alpha to 0 and make sure it's invisible before activating
        canvasGroup.alpha = 0; // Fully transparent
        pageContent.gameObject.SetActive(true); // Activate the page

        // Wait for 0.25 seconds before starting the fade-in effect
        yield return new WaitForSeconds(0.25f);

        float elapsedTime = 0f;

        // Gradually increase the alpha
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it is fully visible
        canvasGroup.alpha = 1f;
    }

    private void SetButtonsVisible(bool visible)
    {
        // Hide or show the entire buttons container (parent GameObject)
        if (Buttons != null)
        {
            Buttons.SetActive(visible);
        }
    }
}
