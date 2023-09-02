using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this namespace for TextMesh Pro
using UnityEngine.Networking;

public class GuessNumberClient : MonoBehaviour
{
    public TMP_InputField inputField; // Change InputField to TMP_InputField
    public TextMeshProUGUI resultText; // Change Text to TextMeshProUGUI
    public Button submitButton;

    private string apiUrl = "http://127.0.0.1:8000/guess/";

    void Start()
    {
        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }

    void OnSubmitButtonClick()
    {
        int guessedNumber;
        if (int.TryParse(inputField.text, out guessedNumber))
        {
            StartCoroutine(SendGuess(guessedNumber));
        }
        else
        {
            resultText.text = "Please enter a valid number.";
        }
    }

    private IEnumerator SendGuess(int guessedNumber)
    {
        string fullUrl = apiUrl + guessedNumber.ToString();
        UnityWebRequest webRequest = UnityWebRequest.Get(fullUrl);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string resultJson = webRequest.downloadHandler.text;
            resultText.text = "Result: " + resultJson;
        }
        else
        {
            resultText.text = "Error: " + webRequest.error;
        }
    }
}
