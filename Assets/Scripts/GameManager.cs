using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Task UI")]
    public TMP_Text taskBookText;
    public TMP_Text taskLampText;
    public TMP_Text taskLaptopText;

    [Header("End Screen UI")]
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;

    [Header("Level Transition")]
    public string nextLevelName = "Level2";
    public float nextLevelDelay = 2f;
    public bool isFinalLevel = false;
    // Task completion flags
    private bool bookTaskDone = false;
    private bool lampTaskDone = false;
    private bool laptopTaskFailed = false;

    void Start()
    {
        // Hide end screens at the start
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (levelCompleteScreen != null) levelCompleteScreen.SetActive(false);
        // Make sure game runs at normal speed
        Time.timeScale = 1f;
        UpdateUI();
    }
    // Called when a task object is successfully interacted with
    public void CompleteTask(InteractableObject.ObjectType type)
    {
        if (type == InteractableObject.ObjectType.Book)
            bookTaskDone = true;
        else if (type == InteractableObject.ObjectType.Lamp)
            lampTaskDone = true;

        UpdateUI();
        CheckForWin();
    }
    // Called when the player triggers a fail condition
    public void GameOver(InteractableObject.ObjectType typeOfFailedObject)
    {
        if (typeOfFailedObject == InteractableObject.ObjectType.Laptop)
            laptopTaskFailed = true;

        UpdateUI();
        StartCoroutine(EndGameAfterDelay(false));
    }

    void UpdateUI()
    {
        if (taskBookText != null) taskBookText.text = (bookTaskDone ? "(DONE) " : "") + "Knock down the books.";
        if (taskLampText != null) taskLampText.text = (lampTaskDone ? "(DONE) " : "") + "Knock down the lamp.";
        if (taskLaptopText != null) taskLaptopText.text = (laptopTaskFailed ? "(FAILED) " : "") + "Don't touch the laptop.";
    }
    // Checks if all win conditions are met
    void CheckForWin()
    {
        if (bookTaskDone && lampTaskDone && !laptopTaskFailed)
            StartCoroutine(EndGameAfterDelay(true));
    }
    // Handles win or lose sequences with a short delay
    IEnumerator EndGameAfterDelay(bool didWin)
    {
        yield return new WaitForSeconds(0.1f);

        if (didWin)
        {
            if (levelCompleteScreen != null) levelCompleteScreen.SetActive(true);
            StartCoroutine(LoadNextLevelAfterDelay());
        }
        else
        {
            if (gameOverScreen != null) gameOverScreen.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSecondsRealtime(nextLevelDelay);
        SceneManager.LoadScene("Level 2");
    }
}
