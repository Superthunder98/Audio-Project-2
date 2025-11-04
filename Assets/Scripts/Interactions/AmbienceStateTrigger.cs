using UnityEngine;
using AK.Wwise;

public class AmbienceStateTrigger : MonoBehaviour
{
    public string stateGroup = "Ambience_State";
    public string stateName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.SetState(stateGroup, stateName);
        }
    }
}