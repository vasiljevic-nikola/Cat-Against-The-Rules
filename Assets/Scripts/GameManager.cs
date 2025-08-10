using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Task UI")]
    public TMP_Text taskBookText;
    public TMP_Text taskLampText;
    public TMP_Text taskLaptopText;

    [Header("End Screen UI")]
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;

    private bool bookTaskDone = false;
    private bool lampTaskDone = false;
    private bool laptopTaskFailed = false;

    void Start()
    {
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (levelCompleteScreen != null) levelCompleteScreen.SetActive(false);
        Time.timeScale = 1f;
        UpdateUI();
    }

    public void CompleteTask(InteractableObject.ObjectType type)
    {
        if (type == InteractableObject.ObjectType.Book)
        {
            bookTaskDone = true;
        }
        else if (type == InteractableObject.ObjectType.Lamp)
        {
            lampTaskDone = true;
        }
        UpdateUI();
        CheckForWin();
    }

    public void GameOver(InteractableObject.ObjectType typeOfFailedObject)
    {
        if (typeOfFailedObject == InteractableObject.ObjectType.Laptop) // Samo laptop izaziva Game Over
        {
            laptopTaskFailed = true;
        }
        UpdateUI();
        StartCoroutine(EndGameAfterDelay(false));
    }

    void UpdateUI()
    {
        if (taskBookText != null) taskBookText.text = (bookTaskDone ? "(DONE) " : "") + "Knock down the books.";
        if (taskLampText != null) taskLampText.text = (lampTaskDone ? "(DONE) " : "") + "Knock down the lamp.";
        if (taskLaptopText != null) taskLaptopText.text = (laptopTaskFailed ? "(FAILED) " : "") + "Don't touch the laptop.";
    }

    void CheckForWin()
    {
        if (bookTaskDone && lampTaskDone && !laptopTaskFailed) // Oba zadatka moraju biti urađena, laptop ne sme biti dodirnut
        {
            StartCoroutine(EndGameAfterDelay(true));
        }
    }

    IEnumerator EndGameAfterDelay(bool didWin)
    {
        yield return new WaitForSeconds(0.1f);

        if (didWin)
        {
            if (levelCompleteScreen != null) levelCompleteScreen.SetActive(true);
        }
        else
        {
            if (gameOverScreen != null) gameOverScreen.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}