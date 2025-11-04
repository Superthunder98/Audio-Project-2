using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;


public class PlayEventOnKeyPress : MonoBehaviour
{

    public KeyCode keyToPress;
    public string eventToPlayName;


    private void Update()
    {
            if (Input.GetKeyDown(keyToPress))
            {
                AkSoundEngine.PostEvent(eventToPlayName, gameObject);
            AudioDebugUI.Report(eventToPlayName);

            }
    }
}
