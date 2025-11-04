using UnityEngine;
using AK.Wwise;

public class LocationStateTrigger : MonoBehaviour
{
    public string stateGroup = "Location";
    public string stateName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.SetState(stateGroup, stateName);
        }
    }
}