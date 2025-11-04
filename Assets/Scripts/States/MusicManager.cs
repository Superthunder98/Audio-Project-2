using UnityEngine;
using AK.Wwise;

public class MusicManager : MonoBehaviour
{
    public string initialStateGroup = "Game_State";
    public string initialStateName;

    private void Start()
    {
        // Set the initial music state
        AkSoundEngine.SetState(initialStateGroup, initialStateName);
    }
}