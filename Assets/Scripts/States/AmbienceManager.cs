using UnityEngine;
using AK.Wwise;

public class AmbienceManager : MonoBehaviour
{
    public string initialStateGroup = "Ambience_State";
    public string initialStateName;

    private void Start()
    {
        // Set the initial ambience state
        AkSoundEngine.SetState(initialStateGroup, initialStateName);
    }
}