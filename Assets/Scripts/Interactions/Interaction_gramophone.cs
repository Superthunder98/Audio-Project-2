using UnityEngine;
using TMPro;
using AK.Wwise;

public class Interaction_gramophone : MonoBehaviour
{
    public string startEventName = "Play_Gramophone";
    public string stopEventName = "Stop_Gramophone";
    public string playTextToDisplay = "E = Play Gramophone";
    public string stopTextToDisplay = "E = Stop Gramophone";
    public GameObject interactUI;
    public TextMeshProUGUI interactText;

    private bool playerInRange = false;
    private bool isMusicPlaying = false;

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
                ToggleMusic();
                HideInteractUI();
                UpdateInteractText();
            }
        }
    }

    private void ToggleMusic()
    {
        if (isMusicPlaying)
        {
            AkSoundEngine.PostEvent(stopEventName, gameObject);
            AudioDebugUI.Report(stopEventName);
            isMusicPlaying = false;
        }
        else
        {
            AkSoundEngine.PostEvent(startEventName, gameObject);
            AudioDebugUI.Report(startEventName);
            isMusicPlaying = true;
        }
    }

    private void UpdateInteractText()
    {
        if (interactText != null)
        {
            interactText.text = isMusicPlaying ? stopTextToDisplay : playTextToDisplay;
        }
    }

    private void HideInteractUI()
    {
        if (interactUI != null)
        {
            interactUI.SetActive(false);
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
                UpdateInteractText();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideInteractUI();
        }
    }
}