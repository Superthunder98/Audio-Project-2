using UnityEngine;
// Ensure you're using the right namespace for Wwise integration
using AK.Wwise;

public class PlayAkAmbientSound : MonoBehaviour
{
    public string eventName = "YourEventNameHere"; 

    // Update is called once per frame
    void Update()
    {
        // Example: Play the Wwise event when the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Post the event to the Wwise sound engine
            AkSoundEngine.PostEvent(eventName, gameObject);
            AudioDebugUI.Report(eventName);
        }
    }
}
