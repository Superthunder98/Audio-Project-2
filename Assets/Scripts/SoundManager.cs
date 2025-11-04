using UnityEngine;
using AK.Wwise;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AK.Wwise.Event openUIEvent;
    public AK.Wwise.Event closeUIEvent;
    public AK.Wwise.Event toggleVisibilityOnEvent;
    public AK.Wwise.Event toggleVisibilityOffEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayPauseSound()
    {
        if (openUIEvent != null)
        {
            openUIEvent.Post(gameObject);
            AudioDebugUI.Report(openUIEvent);
        }
        else
        {
            Debug.LogError("openUIEvent is null.");
        }
    }

    public void PlayResumeSound()
    {
        if (closeUIEvent != null)
        {
            closeUIEvent.Post(gameObject);
            AudioDebugUI.Report(closeUIEvent);
        }
        else
        {
            Debug.LogError("closeUIEvent is null.");
        }
    }

    public void PlayVisibilityToggleSound(bool isVisible)
    {
        if (isVisible)
        {
            if (toggleVisibilityOnEvent != null)
            {
                toggleVisibilityOnEvent.Post(gameObject);
                AudioDebugUI.Report(toggleVisibilityOnEvent);
                Debug.Log("Playing visibility ON sound");
            }
            else
            {
                Debug.LogError("toggleVisibilityOnEvent is null.");
            }
        }
        else
        {
            if (toggleVisibilityOffEvent != null)
            {
                toggleVisibilityOffEvent.Post(gameObject);
                AudioDebugUI.Report(toggleVisibilityOffEvent);
                Debug.Log("Playing visibility OFF sound");
            }
            else
            {
                Debug.LogError("toggleVisibilityOffEvent is null.");
            }
        }
    }
}