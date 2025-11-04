using UnityEngine;
using AK.Wwise;

public class ChildDoorSoundController : MonoBehaviour
{
    public string doorCloseEventName = "Play_Door_3_close";

    public void PlayDoorCloseSound()
    {
        AkSoundEngine.PostEvent(doorCloseEventName, gameObject);
        AudioDebugUI.Report(doorCloseEventName);
    }
}
