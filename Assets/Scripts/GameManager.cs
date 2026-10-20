
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform letterGrid;
    public GameObject letterButtonPrefab;

    public TMP_Text wordText;
    public TMP_Text messageText;
    public TMP_Text attemptsText;
    public HangmanDrawing hangmanDrawing;
    public GameObject restartButton;

    private int wrongGuesses = 0;
    private string secretWord = "UNITY";
    private int remainingAttempts = 6;
    private bool gameEnded = false;
    private char[] displayedWord;

    void Start()
    {
        displayedWord = new char[secretWord.Length];

        for (int i = 0; i < displayedWord.Length; i++)
        {
            displayedWord[i] = '_';
        }

        UpdateWordDisplay();
        GenerateLetterButtons();
        restartButton.SetActive(false);

        messageText.text = "Guess the word!";
        UpdateAttemptsDisplay();
    }
    

    void GenerateLetterButtons()
    {
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            GameObject newButton =
                Instantiate(letterButtonPrefab, letterGrid);

            TMP_Text buttonText =
                newButton.GetComponentInChildren<TMP_Text>();

            buttonText.text = letter.ToString();

            char selectedLetter = letter;

            Button button = newButton.GetComponent<Button>();

            button.onClick.AddListener(() =>
                {
                    GuessLetter(selectedLetter);
                    button.interactable = false;
                });
        }
    }

    void GuessLetter(char letter)
    {
        if (gameEnded)
        {
            return;
        }

        bool correct = false;

        for (int i = 0; i < secretWord.Length; i++)
        {
            if (secretWord[i] == letter)
            {
                displayedWord[i] = letter;
                correct = true;
            }
        }

        UpdateWordDisplay();

        if (correct)
        {
            if (System.Array.IndexOf(displayedWord, '_') == -1)
            {
                messageText.text = "You Win!";
                gameEnded = true;
                restartButton.SetActive(true);
            }
            else
            {
                messageText.text = "Good guess!";
            }
        }
        else
        {
            remainingAttempts--;
            wrongGuesses++;

            UpdateAttemptsDisplay();
            hangmanDrawing.UpdateDrawing(wrongGuesses);

            if (remainingAttempts <= 0)
            {
                messageText.text =
                    "Game Over! The word was " + secretWord;

                gameEnded = true;
                restartButton.SetActive(true);
            }
            else
            {
                messageText.text =
                    "Wrong guess!";
            }
        }
    }

    void UpdateWordDisplay()
    {
        wordText.text = string.Join(" ", displayedWord);
    }
    void UpdateAttemptsDisplay()
{
    attemptsText.text = "Attempts: " + remainingAttempts;
}

    public void RestartGame()
    {
    UnityEngine.SceneManagement.SceneManager
        .LoadScene(
            UnityEngine.SceneManagement.SceneManager
                .GetActiveScene().buildIndex
        );
    }
}
