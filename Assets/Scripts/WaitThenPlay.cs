using System.Collections;
using UnityEngine;
using AK.Wwise; // Make sure to include the Wwise namespace

public class WaitThenPlay : MonoBehaviour
{
    [SerializeField] private float delayInSeconds = 5f; // Default delay of 5 seconds
    private AkAmbient akAmbient;

    [SerializeField] private AK.Wwise.Event soundEvent; // Reference to the Wwise event

    private void Awake()
    {
        // Get the AkAmbient component attached to this GameObject
        akAmbient = GetComponent<AkAmbient>();
        if (akAmbient == null)
        {
            Debug.LogError("No AkAmbient component found on this GameObject!");
        }
    }

    private void Start()
    {
        // Start the coroutine to wait and then play
        StartCoroutine(WaitAndPlay());
    }

    private IEnumerator WaitAndPlay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        // Play the Wwise event
        if (akAmbient != null && soundEvent.IsValid())
        {
            soundEvent.Post(gameObject);
            AudioDebugUI.Report(soundEvent);
            Debug.Log($"Wwise event played after {delayInSeconds} seconds");
        }
        else
        {
            Debug.LogError("Unable to play sound: AkAmbient component or Wwise event is not set correctly.");
        }
    }
}
