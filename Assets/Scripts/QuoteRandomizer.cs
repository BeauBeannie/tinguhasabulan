using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuoteRandomizer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI randomizedQuote; // Assign the TextMeshProUGUI object in the Inspector
    [SerializeField] private List<string> quotes = new List<string>(); // Dynamic list for quotes

    void Start()
    {
        if (quotes.Count > 0)
        {
            DisplayRandomQuote();
            StartCoroutine(FadeInText());
        }
        else
        {
            Debug.LogWarning("No quotes available in the list.");
        }
    }

    public void DisplayRandomQuote()
    {
        if (randomizedQuote != null && quotes.Count > 0)
        {
            int randomIndex = Random.Range(0, quotes.Count); // Get a random index
            randomizedQuote.text = quotes[randomIndex]; // Set the text

            // Set initial transparency to 0
            Color textColor = randomizedQuote.color;
            textColor.a = 0;
            randomizedQuote.color = textColor;

            // Start fade-in
            StartCoroutine(FadeInText());
        }
    }

    IEnumerator FadeInText()
    {
        yield return new WaitForSeconds(0.8f);

        float duration = 2f; // Fade duration
        float elapsedTime = 0f;
        Color textColor = randomizedQuote.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Lerp(0, 1, elapsedTime / duration);
            randomizedQuote.color = textColor;
            yield return null;
        }

        textColor.a = 1; // Ensure full visibility
        randomizedQuote.color = textColor;
    }

    // Public method to add quotes dynamically
    public void AddQuote(string newQuote)
    {
        if (!quotes.Contains(newQuote))
        {
            quotes.Add(newQuote);
        }
    }

    // Public method to clear all quotes
    public void ClearQuotes()
    {
        quotes.Clear();
    }
}
