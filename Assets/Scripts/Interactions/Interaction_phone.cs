using UnityEngine;
using TMPro;
using AK.Wwise;

public class Interaction_phone : MonoBehaviour
{
    public string eventName;
    public string textToDisplay;
    public GameObject interactUI;
    public GameObject phoneHandSet;
    public GameObject phoneRinging;
    //public GameObject phoneDialTone;
    public TextMeshProUGUI interactText;
    private bool playerInRange = false;
    private bool phoneHandSetEnabled = false;
    public int transitionDuration = 0;

    

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
                StopAudioFile();
                AkSoundEngine.PostEvent(eventName, gameObject);
                AudioDebugUI.Report(eventName);
                
                //TogglePhoneHandset();
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

    private void StopAudioFile()
    {
        if (phoneRinging == null)
        {
            Debug.LogError("AkAmbient game object reference is missing!");
            return;
        }

        // Only attempt to stop if the GameObject is active (i.e., registered with Wwise)
        if (!phoneRinging.activeInHierarchy)
        {
            // Nothing to stop because the object is disabled and not registered in Wwise
            return;
        }

        AkAmbient akAmbient = phoneRinging.GetComponent<AkAmbient>();
        if (akAmbient != null)
        {
            akAmbient.Stop(transitionDuration);
        }
        else
        {
            Debug.LogError("AkAmbient component not found on the game object!");
        }
    }

    
    //private void TogglePhoneHandset()
    //{
    //    if (phoneHandSet != null)
    //    {
    //        phoneHandSetEnabled = !phoneHandSetEnabled;
    //        phoneHandSet.SetActive(phoneHandSetEnabled);
    //        phoneDialTone.SetActive(true);
    //    }
    //}


}