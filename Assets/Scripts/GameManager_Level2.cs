using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager_Level2 : MonoBehaviour
{
    [Header("Task UI")]
    public TMP_Text taskKeyText; // UI text for the key task
    public TMP_Text taskDoorText; // UI text for the key task
    public TMP_Text taskArmchairText; // UI text for the armchair task (fail condition)

    [Header("Level Objects")]
    public DoorController doorwayOpened; // Reference to the door that will be opened

    [Header("End Screen UI")]
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;

    [Header("Level Transition")]
    public string nextLevelName = "Level3";
    public float nextLevelDelay = 2f;
    public bool isFinalLevel = false;

    // Task progress flags
    private bool keyTaskDone = false;
    private bool doorTaskDone = false;
    private bool armchairTaskFailed = false;

    void Start()
    {
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (levelCompleteScreen != null) levelCompleteScreen.SetActive(false);
        Time.timeScale = 1f;
        UpdateUI();
    }
    // Called when the player completes the key task
    public void CompleteKeyTask()
    {
        keyTaskDone = true;
        UpdateUI();
        // Open the door if reference exists
        if (doorwayOpened != null)
            doorwayOpened.OpenDoor();
        else
            Debug.LogWarning("DoorwayOpened reference is not set in GameManager_Level2!");

        CheckForWin();
    }
    // Called when the player goes through the door
    public void PlayerWentThroughDoor()
    {
        doorTaskDone = true;
        UpdateUI();
        CheckForWin();
    }
    // Called when the player fails (e.g., touches the armchair)
    public void GameOver()
    {
        armchairTaskFailed = true;
        UpdateUI();
        StartCoroutine(EndGameAfterDelay(false));
    }
    // Updates the on-screen task descriptions
    void UpdateUI()
    {
        if (taskKeyText != null) taskKeyText.text = (keyTaskDone ? "(DONE) " : "") + "Place the key on the pillow.";
        if (taskDoorText != null) taskDoorText.text = (doorTaskDone ? "(DONE) " : "") + "Go through the open door.";
        if (taskArmchairText != null) taskArmchairText.text = (armchairTaskFailed ? "(FAILED) " : "") + "Don't touch the armchair.";
    }

    void CheckForWin()
    {
        if (keyTaskDone && doorTaskDone && !armchairTaskFailed)
            StartCoroutine(EndGameAfterDelay(true));
    }
    // Handles win or lose events with a short delay
    IEnumerator EndGameAfterDelay(bool didWin)
    {
        yield return new WaitForSeconds(0.1f);

        if (didWin)
        {
            if (levelCompleteScreen != null) levelCompleteScreen.SetActive(true);
            StartCoroutine(LoadNextLevelAfterDelay());// Proceed to the next level
        }
        else
        {
            if (gameOverScreen != null) gameOverScreen.SetActive(true);
        }

        Time.timeScale = 0f;
    }
    // Loads the next level after a delay
    IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSecondsRealtime(nextLevelDelay);
        SceneManager.LoadScene("Level 3");
    }
}
