using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager_Level3 : MonoBehaviour
{
    [Header("Task UI")]
    public TMP_Text taskBlenderText;
    public TMP_Text taskCoffeeMachineText;
    public TMP_Text taskStoveText;

    [Header("End Screen UI")]
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;
    public TMP_Text finalMessageText;

    [Header("Level Transition")]
    public bool isFinalLevel = true; // Indicates if this is the last level in the game
    public float nextLevelDelay = 3f; // Delay before transitioning to the next level

    // Task state tracking
    private bool blenderTaskDone = false;
    private bool coffeeMachineTaskDone = false;
    private bool stoveTaskFailed = false;

    void Start()
    {
        // Hide end screens at the start of the level
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (levelCompleteScreen != null) levelCompleteScreen.SetActive(false);
        // Clear final message
        if (finalMessageText != null) finalMessageText.text = "";
        // Ensure game runs at normal speed
        Time.timeScale = 1f;
        // Update the task display at the start
        UpdateUI();
    }
    // Called when a task is completed successfully
    public void CompleteTask(InteractableObject.ObjectType type)
    {
        if (type == InteractableObject.ObjectType.Blender)
            blenderTaskDone = true;
        else if (type == InteractableObject.ObjectType.CoffeeMachine)
            coffeeMachineTaskDone = true;

        UpdateUI();
        CheckForWin();
    }
    // Called when the player fails (e.g., touches the stove)
    public void GameOver(InteractableObject.ObjectType typeOfFailedObject)
    {
        if (typeOfFailedObject == InteractableObject.ObjectType.Stove)
            stoveTaskFailed = true;

        UpdateUI();
        StartCoroutine(EndGameAfterDelay(false)); // Trigger loss sequence
    }
    // Refreshes the UI text for each task
    void UpdateUI()
    {
        if (taskBlenderText != null) taskBlenderText.text = (blenderTaskDone ? "(DONE) " : "") + "Knock down the blender.";
        if (taskCoffeeMachineText != null) taskCoffeeMachineText.text = (coffeeMachineTaskDone ? "(DONE) " : "") + "Knock down the coffee machine.";
        if (taskStoveText != null) taskStoveText.text = (stoveTaskFailed ? "(FAILED) " : "") + "Don't touch the stove.";
    }
    // Checks if all win conditions are met
    void CheckForWin()
    {
        if (blenderTaskDone && coffeeMachineTaskDone && !stoveTaskFailed)
            StartCoroutine(EndGameAfterDelay(true));
    }
    // Handles ending the game (win or lose) after a short delay
    IEnumerator EndGameAfterDelay(bool didWin)
    {
        if (didWin)
        {
            if (levelCompleteScreen != null) levelCompleteScreen.SetActive(true);
            // Show a special message if this is the final level
            if (isFinalLevel && finalMessageText != null)
                finalMessageText.text = "Congratulations! You've completed the game!";
        }
        else
        {
            if (gameOverScreen != null) gameOverScreen.SetActive(true);
        }

        // Small delay before freezing the game
        yield return new WaitForSecondsRealtime(0.1f);
        // Pause the game
        Time.timeScale = 0f;
    }
}
