using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioOcclusion : MonoBehaviour
{
    public Transform listener; // Assign the GameObject with an AudioListener component
    public LayerMask occlusionLayers; // Assign layers which will be considered for occlusion
    public float maxDistance = 50f; // Max distance to check for occlusion
    public AudioLowPassFilter lowPassFilter; // The low pass filter component
    public float transitionSpeed = 1f; // Speed at which the cutoff frequency changes
    public float occludedVolume = 0.3f; // Volume level when occluded (between 0 and 1)
    public float occludedFrequency = 2000f;

    private AudioSource audioSource;
    private float targetCutoffFrequency = 20000f; // Target cutoff frequency (not occluded state)
    private float targetVolume = 1f; // Target volume (not occluded state)



    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Add a low pass filter if it's not added
        if (lowPassFilter == null)
        {
            lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
            lowPassFilter.enabled = true; // Initially enabled
            lowPassFilter.cutoffFrequency = 20000f; // Setting the LPF frequency to 20000Hz
        }

        // Set the initial target volume
        targetVolume = audioSource.volume;
    }

    void Update()
    {
        // Cast a ray from the audio source to the listener
        RaycastHit hit;
        Vector3 direction = listener.position - transform.position;
        bool isOccluded = Physics.Raycast(transform.position, direction, out hit, maxDistance, occlusionLayers);

        // If the ray hits an object tagged as occluder
        if (isOccluded && hit.collider.CompareTag("Occluder"))
        {
            targetCutoffFrequency = occludedFrequency; // Set target cutoff frequency to 1000Hz
            targetVolume = occludedVolume; // Set target volume to occludedVolume
        }
        else
        {
            targetCutoffFrequency = 20000f; // Set target cutoff frequency to 20000Hz
            targetVolume = 1f; // Set target volume to 1
        }

        // Gradually change the cutoff frequency towards the target
        lowPassFilter.cutoffFrequency = Mathf.Lerp(lowPassFilter.cutoffFrequency, targetCutoffFrequency, Time.deltaTime * transitionSpeed);

        // Gradually change the volume towards the target
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * transitionSpeed);
    }
}
