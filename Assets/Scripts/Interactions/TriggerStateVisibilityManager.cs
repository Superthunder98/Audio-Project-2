using UnityEngine;
using UnityEngine.UI;

public class TriggerStateVisibilityManager : MonoBehaviour
{
    private GameObject[] triggerBoxes;
    private bool isVisible = false;

    [SerializeField] private Button toggleButton;
    [SerializeField] private Image toggleIcon;
    [SerializeField] private Sprite tickSprite;
    [SerializeField] private Sprite crossSprite;

    [Header("Optional: Use this instance to toggle the Debug overlay instead")]
    [SerializeField] private bool useForDebugOverlay = false;
    [SerializeField] private GameObject debugOverlayRoot; // Assign your 'Debugging' object (with TextMeshPro)

    void Start()
    {
        triggerBoxes = GameObject.FindGameObjectsWithTag("LocationStateTrigger");

        if (useForDebugOverlay && debugOverlayRoot == null)
        {
            // Fallback: try to find by common name
            debugOverlayRoot = GameObject.Find("Debugging");
        }

        if (useForDebugOverlay)
        {
            // Force debug overlay OFF at startup and reflect that in the button state
            AudioDebugUI.SetVisible(false);
            isVisible = false;
        }

        UpdateButtonAppearance();
    }

    void Update()
    {
        if (!useForDebugOverlay)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleTriggerVisibility();
            }
        }
    }

    public void ToggleTriggerVisibility()
    {
        if (useForDebugOverlay)
        {
            ToggleDebugOverlayVisibility();
            return;
        }

        isVisible = !isVisible;
        foreach (GameObject triggerBox in triggerBoxes)
        {
            MeshRenderer renderer = triggerBox.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = isVisible;
            }
        }

        Debug.Log($"Trigger visibility toggled to {isVisible}");
        SoundManager.instance.PlayVisibilityToggleSound(isVisible);

        UpdateButtonAppearance();
    }

    public void ToggleButtonAction()
    {
        if (useForDebugOverlay)
        {
            ToggleDebugOverlayVisibility();
        }
        else
        {
            ToggleTriggerVisibility();
        }
    }

    private void ToggleDebugOverlayVisibility()
    {
        bool newVisible = !AudioDebugUI.IsVisible();
        AudioDebugUI.SetVisible(newVisible);

        // Keep shared UI indicator state in sync
        isVisible = newVisible;
        UpdateButtonAppearance();
    }

    private void UpdateButtonAppearance()
    {
        if (toggleIcon != null)
        {
            toggleIcon.sprite = isVisible ? tickSprite : crossSprite;
            toggleIcon.color = isVisible ? Color.white : new Color(1, 1, 1, 0.5f);
            //Debug.Log($"Button appearance updated. Visible: {isVisible}. Sprite: {toggleIcon.sprite.name}");
        }
        else
        {
            Debug.LogError("Toggle Icon is not assigned in the inspector!");
        }
    }
}