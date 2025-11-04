using UnityEngine;
using TMPro;
using AK.Wwise;

public class PianoInteraction : MonoBehaviour
{
    public GameObject interactUI;
    public string pianoEventName = "Play_Piano";
    public float interactionDistance = 3f;

    private TextMeshProUGUI interactText;
    private GameObject player;

    private void Start()
    {
        if (interactUI != null)
        {
            interactUI.SetActive(false);
            interactText = interactUI.GetComponentInChildren<TextMeshProUGUI>();
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= interactionDistance)
            {
                if (interactUI != null)
                {
                    interactUI.SetActive(true);

                    if (interactText != null)
                    {
                        interactText.text = "E = Play piano";
                    }
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    AkSoundEngine.PostEvent(pianoEventName, gameObject);
                    AudioDebugUI.Report(pianoEventName);
                }
            }
            else
            {
                if (interactUI != null)
                {
                    interactUI.SetActive(false);
                }
            }
        }
    }
}