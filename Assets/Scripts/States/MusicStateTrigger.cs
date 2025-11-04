using UnityEngine;
using AK.Wwise;

public class MusicStateTrigger : MonoBehaviour
{
    public string stateGroup = "Game_State";
    public string stateName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.SetState(stateGroup, stateName);
        }
    }
}