using UnityEngine;
using TMPro;
using AK.Wwise;

public class LightSwitchInteraction : MonoBehaviour
{
    public string eventName;
    public string textToDisplay;
    public GameObject interactUI;
    public TextMeshProUGUI interactText;
    public GameObject lightsObject;
    public GameObject lightSwitch;
    public float switchOffAngle = -15f;
    public bool countLights = false;

    [Header("Emission Control")]
    public bool useEmissionControl = false;
    public GameObject[] emissionObjects;
    public string emissionColorProperty = "_EmissionColor";
    public Color emissionOnColor = Color.white;

    private bool lightsEnabled = false;
    private bool playerInRange = false;
    private Material[] emissionMaterials;

    private void Start()
    {
        if (interactUI != null)
        {
            interactUI.SetActive(false);
        }
        if (lightsObject != null)
        {
            lightsObject.SetActive(false);
        }

        if (useEmissionControl)
        {
            InitializeEmissionMaterials();
        }
    }

    private void InitializeEmissionMaterials()
    {
        emissionMaterials = new Material[emissionObjects.Length];
        for (int i = 0; i < emissionObjects.Length; i++)
        {
            Renderer renderer = emissionObjects[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                emissionMaterials[i] = renderer.material;
                SetEmission(emissionMaterials[i], false);
            }
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
                ToggleLights();

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

    private void ToggleLights()
    {
        lightsEnabled = !lightsEnabled;

        // Toggle emission if enabled
        if (useEmissionControl)
        {
            ToggleEmission();
        }

        // Toggle lightsObject regardless of emission control
        if (lightsObject != null)
        {
            lightsObject.SetActive(lightsEnabled);
        }

        // Set light switch rotation
        if (lightSwitch != null)
        {
            lightSwitch.transform.localEulerAngles = new Vector3(lightsEnabled ? 0f : switchOffAngle, 0f, 0f);
        }

        // Adjust the score based on the lights' state
        if (countLights)
        {
            ScoringSystem.theScore += lightsEnabled ? 1 : -1;
        }
    }

    private void ToggleEmission()
    {
        foreach (Material material in emissionMaterials)
        {
            if (material != null)
            {
                SetEmission(material, lightsEnabled);
            }
        }
    }

    private void SetEmission(Material material, bool enabled)
    {
        if (enabled)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor(emissionColorProperty, emissionOnColor);
        }
        else
        {
            material.DisableKeyword("_EMISSION");
            material.SetColor(emissionColorProperty, Color.black);
        }
    }
}
