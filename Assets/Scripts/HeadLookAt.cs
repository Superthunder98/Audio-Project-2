using UnityEngine;

public class HeadLookAt : MonoBehaviour
{
    public Transform playerTransform;
    public Transform headBone;
    public float lookSpeed = 250f;
    public float maxAngle = 60f;
    public float weightSmoothing = 1f;
    [Range(0, 1)]
    public float lookAtWeight = 0.5f;

    private Quaternion initialRotation;
    private float currentWeight;

    void Start()
    {
        if (headBone == null)
        {
            Debug.LogError("Head bone not assigned!");
            enabled = false;
            return;
        }

        initialRotation = headBone.localRotation;

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("Player transform not assigned and Player tag not found in scene!");
                enabled = false;
                return;
            }
        }
    }

    void LateUpdate()
    {
        if (playerTransform == null) return;

        // Calculate the direction to the player
        Vector3 targetDirection = playerTransform.position - headBone.position;

        // Calculate the angle between the head's forward direction and the target direction
        float angle = Vector3.Angle(headBone.forward, targetDirection);

        // Calculate the dot product to check if the player is behind the character
        float dotProduct = Vector3.Dot(headBone.forward, targetDirection.normalized);

        // If the angle exceeds the maxAngle or the player is behind the character, stop checking for the player's location
        if (angle > maxAngle || dotProduct < 0)
        {
            // Smoothly reduce the weight to zero
            currentWeight = Mathf.MoveTowards(currentWeight, 0, Time.deltaTime * weightSmoothing);
        }
        else
        {
            // Smoothly adjust the weight towards the lookAtWeight
            currentWeight = Mathf.MoveTowards(currentWeight, lookAtWeight, Time.deltaTime * weightSmoothing);
        }

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Blend between the current (animated) rotation and the target rotation
        Quaternion blendedRotation = Quaternion.Slerp(headBone.rotation, targetRotation, currentWeight);

        // Apply the blended rotation
        headBone.rotation = Quaternion.Slerp(headBone.rotation, blendedRotation, Time.deltaTime * lookSpeed);
    }
}
