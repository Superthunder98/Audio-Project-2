using UnityEngine;
using TMPro;
using AK.Wwise;

public class Interaction : MonoBehaviour
{
    public string eventName;
    public string textToDisplay;
    public GameObject interactUI;
    public TextMeshProUGUI interactText;
    private bool playerInRange = false;

    private void Start()
    {
        if (interactUI != null)
        {
            interactUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AkSoundEngine.PostEvent(eventName, gameObject);
                AudioDebugUI.Report(eventName);
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactUI != null)
            {
                interactUI.SetActive(true);
                if (interactText != null)
                {
                    interactText.text = textToDisplay;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactUI != null)
            {
                interactUI.SetActive(false);
            }
        }
    }

    
}