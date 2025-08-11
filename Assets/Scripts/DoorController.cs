using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject openedDoorSprite;

    public void OpenDoor()
    {
        Debug.Log("The door is opening!");
        gameObject.SetActive(false); // Turn off the original door
        if (openedDoorSprite != null)
        {
            openedDoorSprite.SetActive(true); // Turn on door opened status
        }
    }

}
