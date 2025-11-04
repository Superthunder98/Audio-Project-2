using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventDoorSound : MonoBehaviour
{
    private ChildDoorSoundController childDoorSoundController;

    private void Awake()
    {
        // Find the ChildDoorSoundController script on the child GameObject
        childDoorSoundController = GetComponentInChildren<ChildDoorSoundController>();
    }

    public void PlayDoorCloseSound()
    {
        if (childDoorSoundController != null)
        {
            childDoorSoundController.PlayDoorCloseSound();
        }
        else
        {
            Debug.LogError("ChildDoorSoundController script not found on a child GameObject.");
        }
    }
}
