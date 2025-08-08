using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager_Level2 : MonoBehaviour
{
    [Header("Task UI")]
    public TMP_Text taskKeyText;
    public TMP_Text taskDoorText;
    public TMP_Text taskArmchairText;

    [Header("Level Objects")]
    public DoorController door;

    [Header("End Screen UI")]
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;

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

    // NOVA FUNKCIJA KOJU POZIVA KLJUČ
    public void CompleteKeyTask()
    {
        keyTaskDone = true;
        if (door != null)
        {
            door.OpenDoor();
        }
        UpdateUI();
        CheckForWin();
    }

    public void PlayerWentThroughDoor()
    {
        doorTaskDone = true;
        UpdateUI();
        CheckForWin();
    }

    public void GameOver()
    {
        armchairTaskFailed = true;
        UpdateUI();
        StartCoroutine(EndGameAfterDelay(false));
    }

    void UpdateUI()
    {
        if (taskKeyText != null) taskKeyText.text = (keyTaskDone ? "(DONE) " : "") + "Place the key on the pillow to open the door.";
        if (taskDoorText != null) taskDoorText.text = (doorTaskDone ? "(DONE) " : "") + "Go through the open door.";
        if (taskArmchairText != null) taskArmchairText.text = (armchairTaskFailed ? "(FAILED) " : "") + "Don't touch the armchair.";
    }

    void CheckForWin()
    {
        if (keyTaskDone && doorTaskDone && !armchairTaskFailed)
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
