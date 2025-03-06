using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuoteRandomizer : MonoBehaviour
{
    public TextMeshProUGUI randomizedQuote; // Assign your TextMesh object in the Inspector


    private string[] quotes =
    {
        "The moon still waits for me, just as I wait for the right moment to rise again.",
        "One night, when the world is still and the ocean calm, I will rise again.",
        "The tides will bring me back.",
        "We will meet again.",
        "The sky still calls my name.",
        "Drifted away, but never lost.",
        "The tides will carry me back to where I belong—beside the moon, beneath the stars.",
        "Some journeys take longer than others, but the path always leads back to you.",
        "The night is patient. So am I. The story is not over.",
        "Even the waves must wait.",
        "Some light is meant to be chased, and some bonds are never truly broken.",
        "I am meant to return, and when I do, the sky will open for me once more.",
        "The moonlight still reaches me, even beneath the waves. It has not forgotten me, and I have not forgotten it."
    };

    void Start()
    {
        DisplayRandomQuote();
        StartCoroutine(FadeInText());
    }

    void DisplayRandomQuote()
    {
        if (randomizedQuote != null && quotes.Length > 0)
        {
            int randomIndex = Random.Range(0, quotes.Length); // Get a random index
            randomizedQuote.text = quotes[randomIndex]; // Set the text
            Color textColor = randomizedQuote.color;
            textColor.a = 0; // Start fully transparent
            randomizedQuote.color = textColor;
        }
    }

    IEnumerator FadeInText()
    {
        yield return new WaitForSeconds(0.8f);

        float duration = 2f; // Fade duration in seconds
        float elapsedTime = 0f;
        Color textColor = randomizedQuote.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Lerp(0, 1, elapsedTime / duration);
            randomizedQuote.color = textColor;
            yield return null;
        }

        textColor.a = 1; // Ensure it’s fully visible
        randomizedQuote.color = textColor;
    }
}
