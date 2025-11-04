using UnityEngine;

public class MasterLightToggle : MonoBehaviour
{
    public GameObject[] lights;
    public KeyCode toggleKey = KeyCode.L; 
    private bool areLightsOn = false;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleAllLights();
        }
    }

    private void ToggleAllLights()
    {
        areLightsOn = !areLightsOn;
        foreach (var lightObject in lights)
        {
            if (lightObject != null)
            {
                // Toggle each light's active state
                lightObject.SetActive(areLightsOn);
            }
        }

        // Update the score based on the lights' state
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (areLightsOn)
        {
            ScoringSystem.theScore += lights.Length;
        }
        else
        {
            ScoringSystem.theScore = Mathf.Max(ScoringSystem.theScore - lights.Length, 0);
        }
    }
}
