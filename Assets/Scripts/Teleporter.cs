using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;
    public GameObject sphere4;
    public GameObject sphere5;


    private CharacterController characterController;

    void Start()
    {
        // Try to get the CharacterController component if it exists
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TeleportTo(sphere1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TeleportTo(sphere2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TeleportTo(sphere3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TeleportTo(sphere4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TeleportTo(sphere5);
        }
    }

    void TeleportTo(GameObject sphere)
    {
        if (sphere != null)
        {
            // Disable the CharacterController if it exists
            if (characterController != null)
            {
                characterController.enabled = false;
            }

            // Set the player's position to the sphere's position
            transform.position = sphere.transform.position;
            //transform.rotation = sphere.transform.rotation;

            // Re-enable the CharacterController if it exists
            if (characterController != null)
            {
                characterController.enabled = true;
            }
        }
    }
}
