using UnityEngine;
using UnityEngine.UI;

public class TriggerBoxVisibilityManager : MonoBehaviour
{
    private GameObject[] interactables;
    private bool isVisible = false;

    [SerializeField] private Button toggleButton;
    [SerializeField] private Image toggleIcon;
    [SerializeField] private Sprite tickSprite;
    [SerializeField] private Sprite crossSprite;

    void Start()
    {
        // Collect all objects with the tag "Interactable"
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        UpdateButtonAppearance();
    }

    void Update()
    {
        // Keep the key press functionality as an alternative
        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleInteractableVisibility();
        }
    }

    public void ToggleInteractableVisibility()
    {
        isVisible = !isVisible;
        foreach (GameObject interactable in interactables)
        {
            MeshRenderer renderer = interactable.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = isVisible;
            }
        }

        // Play the visibility toggle sound
        SoundManager.instance.PlayVisibilityToggleSound(isVisible);

        // Update button appearance
        UpdateButtonAppearance();
    }

    private void UpdateButtonAppearance()
    {
        if (toggleIcon != null)
        {
            toggleIcon.sprite = isVisible ? tickSprite : crossSprite;
            toggleIcon.color = isVisible ? Color.white : new Color(1, 1, 1, 0.5f); // Optional: dim the icon when off
           // Debug.Log($"Button state updated. Visible: {isVisible}. Sprite assigned: {toggleIcon.sprite.name}");
        }
        else
        {
            Debug.LogError("Toggle Icon is not assigned in the inspector!");
        }
    }
}