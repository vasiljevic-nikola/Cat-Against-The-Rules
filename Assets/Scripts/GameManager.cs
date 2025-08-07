using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Reference na UI elemente
    public TMP_Text taskBooksText;
    public TMP_Text taskLampText;
    public TMP_Text taskLaptopText;

    // Statusi zadataka
    private bool booksTaskComplete = false;
    private bool lampTaskComplete = false;
    private bool isGameOver = false;

    // Originalni tekstovi zadataka
    private string booksTaskString = "Knock down the books";
    private string lampTaskString = "Knock down the lamp";
    private string laptopTaskString = "Don't touch the laptop";

    void Start()
    {
        UpdateUI();
    }

    // Poziva se iz InteractableObject skripte
    public void CompleteTask(InteractableObject.ObjectType type)
    {
        if (isGameOver) return;

        Debug.Log("GameManager.CompleteTask CALLED with type: " + type.ToString());

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

        Debug.Log("GAME OVER! You touched the forbidden object!");
        isGameOver = true;
        Time.timeScale = 0f; // Zamrzni igru
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Ažuriraj tekst za knjige
        if (booksTaskComplete)
        {
            taskBooksText.text = booksTaskString + " (DONE)";
        }
        else
        {
            taskBooksText.text = booksTaskString;
        }

        // Ažuriraj tekst za lampu
        if (lampTaskComplete)
        {
            taskLampText.text = lampTaskString + " (DONE)";
        }
        else
        {
            taskLampText.text = lampTaskString;
        }

        // Ažuriraj tekst za laptop
        if (isGameOver)
        {
            taskLaptopText.text = laptopTaskString + " (FAILED!)";
        }
        else
        {
            taskLaptopText.text = laptopTaskString;
        }
    }

    private void CheckForWin()
    {
        if (booksTaskComplete && lampTaskComplete)
        {
            Debug.Log("LEVEL COMPLETE! You are a master of chaos!");
            isGameOver = true;
            Time.timeScale = 0f; // Zamrzni igru
        }
    }
}
