using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AK.Wwise;

public class AudioDebugUI : MonoBehaviour
{
    public static AudioDebugUI Instance { get; private set; }

    public TextMeshProUGUI debugText;
    [SerializeField] private int maxLines = 8;
    [SerializeField] private float lineLifetimeSeconds = 4f;

    private readonly List<(string line, float expiresAt)> activeLines = new List<(string, float)>();
    private string lastRendered = string.Empty;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (debugText == null)
        {
            debugText = GetComponent<TextMeshProUGUI>();
        }

        Clear();
        // Ensure overlay starts hidden but can be enabled later without being shut off again
        gameObject.SetActive(false);
    }


    public static void Report(string eventName)
    {
        if (Instance == null)
        {
            return;
        }

        Instance.AddLine(eventName);
    }

    public static void Report(AK.Wwise.Event wwiseEvent)
    {
        if (Instance == null)
        {
            return;
        }

        string name = wwiseEvent != null ? wwiseEvent.Name : "<null event>";
        Instance.AddLine(name);
    }

    private void AddLine(string eventName)
    {
        // Ignore excessively noisy events like footsteps
        if (!string.IsNullOrEmpty(eventName))
        {
            string lower = eventName.ToLower();
            if (lower.Contains("footstep"))
            {
                return;
            }
        }

        // Display only the event name, without any timestamp
        string line = eventName;

        activeLines.Add((line, Time.time + Mathf.Max(0.01f, lineLifetimeSeconds)));

        // Enforce max lines by trimming oldest entries
        while (activeLines.Count > maxLines)
        {
            activeLines.RemoveAt(0);
        }

        Render();
    }

    public void Clear()
    {
        activeLines.Clear();
        if (debugText != null)
        {
            debugText.text = string.Empty;
        }
        lastRendered = string.Empty;
    }

    public static void SetVisible(bool visible)
    {
        if (Instance == null)
        {
            return;
        }
        Instance.gameObject.SetActive(visible);
    }

    public static bool IsVisible()
    {
        return Instance != null && Instance.gameObject.activeSelf;
    }

    private void Update()
    {
        bool removedAny = false;
        float now = Time.time;
        for (int i = activeLines.Count - 1; i >= 0; i--)
        {
            if (activeLines[i].expiresAt <= now)
            {
                activeLines.RemoveAt(i);
                removedAny = true;
            }
        }

        if (removedAny)
        {
            Render();
        }
    }

    private void Render()
    {
        if (debugText == null)
        {
            return;
        }

        string composed = string.Join("\n", activeLines.ConvertAll(l => l.line));
        if (composed != lastRendered)
        {
            debugText.text = composed;
            lastRendered = composed;
        }
    }
}

