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
    public bool isFinalLevel = true;
    public float nextLevelDelay = 3f;

    private bool blenderTaskDone = false;
    private bool coffeeMachineTaskDone = false;
    private bool stoveTaskFailed = false;

    void Start()
    {
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (levelCompleteScreen != null) levelCompleteScreen.SetActive(false);
        if (finalMessageText != null) finalMessageText.text = "";
        Time.timeScale = 1f;
        UpdateUI();
    }

    public void CompleteTask(InteractableObject.ObjectType type)
    {
        if (type == InteractableObject.ObjectType.Blender)
            blenderTaskDone = true;
        else if (type == InteractableObject.ObjectType.CoffeeMachine)
            coffeeMachineTaskDone = true;

        UpdateUI();
        CheckForWin();
    }

    public void GameOver(InteractableObject.ObjectType typeOfFailedObject)
    {
        if (typeOfFailedObject == InteractableObject.ObjectType.Stove)
            stoveTaskFailed = true;

        UpdateUI();
        StartCoroutine(EndGameAfterDelay(false));
    }

    void UpdateUI()
    {
        if (taskBlenderText != null) taskBlenderText.text = (blenderTaskDone ? "(DONE) " : "") + "Knock down the blender.";
        if (taskCoffeeMachineText != null) taskCoffeeMachineText.text = (coffeeMachineTaskDone ? "(DONE) " : "") + "Knock down the coffee machine.";
        if (taskStoveText != null) taskStoveText.text = (stoveTaskFailed ? "(FAILED) " : "") + "Don't touch the stove.";
    }

    void CheckForWin()
    {
        if (blenderTaskDone && coffeeMachineTaskDone && !stoveTaskFailed)
            StartCoroutine(EndGameAfterDelay(true));
    }

    IEnumerator EndGameAfterDelay(bool didWin)
    {
        if (didWin)
        {
            if (levelCompleteScreen != null) levelCompleteScreen.SetActive(true);

            if (isFinalLevel && finalMessageText != null)
                finalMessageText.text = "Congratulations! You've completed the game!";
        }
        else
        {
            if (gameOverScreen != null) gameOverScreen.SetActive(true);
        }

        // Ovo koristi realno vreme tako da pauza ne spreči prikaz
        yield return new WaitForSecondsRealtime(0.1f);

        Time.timeScale = 0f;
    }
}
