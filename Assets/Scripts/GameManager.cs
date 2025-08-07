using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // --- Reference na UI elemente ---
    [Header("Task UI")] // Zaglavlja za bolju organizaciju u Inspector-u
    public TMP_Text taskBooksText;
    public TMP_Text taskLampText;
    public TMP_Text taskLaptopText;

    [Header("End Screen UI")]
    public GameObject gameOverScreen; // Koristimo GameObject da možemo da ga palimo/gasimo
    public GameObject levelCompleteScreen;

    // --- Statusi zadataka ---
    private bool booksTaskComplete = false;
    private bool lampTaskComplete = false;
    private bool isGameOver = false;

    // --- Originalni tekstovi zadataka ---
    private string booksTaskString = "Knock down the books";
    private string lampTaskString = "Knock down the lamp";
    private string laptopTaskString = "Don't touch the laptop";

    void Start()
    {
        // Osiguraj da su ekrani ugašeni na početku
        gameOverScreen.SetActive(false);
        levelCompleteScreen.SetActive(false);

        // Resetuj vreme ako je prethodno bilo zaustavljeno
        Time.timeScale = 1f;

        UpdateUI();
    }

    public void CompleteTask(InteractableObject.ObjectType type)
    {
        if (isGameOver) return;

        if (type == InteractableObject.ObjectType.Book)
        {
            booksTaskComplete = true;
        }
        else if (type == InteractableObject.ObjectType.Lamp)
        {
            lampTaskComplete = true;
        }

        UpdateUI();
        CheckForWin();
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverScreen.SetActive(true); // Prikaži GAME OVER ekran
        Time.timeScale = 0f; // Zamrzni igru
        UpdateUI(); // Ažuriraj status laptopa na FAILED
    }

    private void UpdateUI()
    {
        if (isGameOver)
        {
            // Ako je kraj igre, ne ažuriraj ništa osim eventualno finalnog statusa
            taskLaptopText.text = laptopTaskString + " (FAILED!)";
            return;
        }

        taskBooksText.text = booksTaskComplete ? booksTaskString + " (DONE)" : booksTaskString;
        taskLampText.text = lampTaskComplete ? lampTaskString + " (DONE)" : lampTaskString;
        taskLaptopText.text = laptopTaskString;
    }

    private void CheckForWin()
    {
        if (booksTaskComplete && lampTaskComplete)
        {
            isGameOver = true;
            levelCompleteScreen.SetActive(true); // Prikaži LEVEL COMPLETE ekran
            Time.timeScale = 0f; // Zamrzni igru
        }
    }
}
