using UnityEngine;
using AK.Wwise;

public class PlayerLineOfSightDemonTrigger : MonoBehaviour
{
    public AK.Wwise.Event soundEvent; // Wwise event to trigger
    public float checkInterval = 0.5f; // How often to check visibility
    public float maxViewDistance = 20f; // Maximum distance for the player to see the demon
    public LayerMask obstacleLayer; // Layers to be considered as obstacles
    public Transform playerTransform; // Assign the player's transform in the inspector

    private bool hasPlayed = false;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            CheckPlayerLineOfSightAndPlaySound();
        }
    }

    void CheckPlayerLineOfSightAndPlaySound()
    {
        if (!hasPlayed && IsVisibleToPlayer())
        {
            soundEvent.Post(gameObject);
            AudioDebugUI.Report(soundEvent != null ? soundEvent.Name : "<null event>");
            hasPlayed = true;
            Debug.Log("Player sees the demon. Playing sound event.");
        }
    }

    bool IsVisibleToPlayer()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player transform not assigned!");
            return false;


            
        }

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= maxViewDistance)
        {
            // Check if there's a clear line of sight
            Ray ray = new Ray(transform.position, directionToPlayer);
            if (!Physics.Raycast(ray, distanceToPlayer, obstacleLayer))
            {
                // Check if the demon is in the player's field of view
                float dotProduct = Vector3.Dot(playerTransform.forward, -directionToPlayer.normalized);
                float fieldOfViewThreshold = Mathf.Cos(60f * Mathf.Deg2Rad); // 60 degree field of view

                if (dotProduct > fieldOfViewThreshold)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Optional: Method to reset the trigger, allowing the sound to play again
    public void ResetTrigger()
    {
        hasPlayed = false;
    }
}