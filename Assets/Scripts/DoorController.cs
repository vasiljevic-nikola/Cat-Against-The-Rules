using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject openedDoorSprite;

    public void OpenDoor()
    {
        Debug.Log("The door is opening!");
        gameObject.SetActive(false); // Ugasi originalna vrata
        if (openedDoorSprite != null)
        {
            openedDoorSprite.SetActive(true); // Upali crnu rupu
        }
    }

}

