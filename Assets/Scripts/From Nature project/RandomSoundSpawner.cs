using UnityEngine;
using System.Collections;
using AK.Wwise; // Add Wwise namespace

public class RandomSoundSpawner : MonoBehaviour
{
    [Tooltip("Maximum distance on the X-axis that the sound can spawn from the initial position")]
    [SerializeField] private float maxDistanceX = 10f;

    [Tooltip("Maximum distance on the Z-axis that the sound can spawn from the initial position")]
    [SerializeField] private float maxDistanceZ = 10f;

    [Tooltip("Maximum elevation on the Y-axis that the sound can spawn above the initial position")]
    [SerializeField] private float maxElevationY = 5f;

    [Tooltip("Minimum time interval between sound spawns")]
    [SerializeField] private float minTimeInterval = 5f;

    [Tooltip("Maximum time interval between sound spawns")]
    [SerializeField] private float maxTimeInterval = 15f;

    [Tooltip("Array of Wwise events to be played randomly")]
    [SerializeField] private AK.Wwise.Event[] wwiseEvents; // Replace AudioClip[] with Wwise Event[]

    [Tooltip("If true, waits for the estimated duration of the event before starting the next interval")]
    [SerializeField] private bool waitForEventCompletion = true;

    [Tooltip("Maximum pitch variation applied to each sound (0 = no variation, 1 = maximum variation)")]
    [SerializeField] private float audioPitchVariation = 0.2f;

    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Start()
    {
        StartCoroutine(PlaySoundRoutine());
    }

    private IEnumerator PlaySoundRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minTimeInterval, maxTimeInterval));

        while (true)
        {
            RepositionAndPlaySound();
            if (waitForEventCompletion)
            {
                // Estimate event duration (you may need to adjust this)
                float estimatedDuration = 5f; // Default to 5 seconds
                yield return new WaitForSeconds(estimatedDuration);
            }
            yield return new WaitForSeconds(Random.Range(minTimeInterval, maxTimeInterval));
        }
    }

    private void RepositionAndPlaySound()
    {
        // Randomly reposition
        float randomX = Random.Range(-maxDistanceX, maxDistanceX);
        float randomZ = Random.Range(-maxDistanceZ, maxDistanceZ);
        float randomY = Random.Range(0, maxElevationY);
        transform.position = initialPosition + new Vector3(randomX, randomY, randomZ);

        // Play random Wwise event
        if (wwiseEvents.Length > 0)
        {
            AK.Wwise.Event wwiseEvent = wwiseEvents[Random.Range(0, wwiseEvents.Length)];
            
            // Play the Wwise event
            // The position will be automatically set based on the GameObject's position
            wwiseEvent.Post(gameObject);
            AudioDebugUI.Report(wwiseEvent);

            // Set pitch variation if supported by your Wwise project
            // You may need to use RTPC (Real-Time Parameter Control) for this
            // Example (adjust 'PitchParameter' to match your Wwise project setup):
            // AkSoundEngine.SetRTPCValue("PitchParameter", 1f + Random.Range(-audioPitchVariation, audioPitchVariation), gameObject);
        }
    }

    private void OnValidate()
    {
        // Ensure minInterval is always less than or equal to maxInterval
        if (minTimeInterval > maxTimeInterval)
        {
            minTimeInterval = maxTimeInterval;
        }

        // Ensure AudioPitchVariation is within a reasonable range
        audioPitchVariation = Mathf.Clamp(audioPitchVariation, 0f, 1f);
    }
}