using UnityEngine;

public class DisableColliderOnTrigger : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}