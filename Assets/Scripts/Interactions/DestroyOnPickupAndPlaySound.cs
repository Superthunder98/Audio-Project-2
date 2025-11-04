using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPickupAndPlaySound : MonoBehaviour
{
    public AK.Wwise.Event pickupSoundEvent;
    public MeshRenderer meshRenderer;
    public BoxCollider boxCollider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (pickupSoundEvent != null)
        {
            pickupSoundEvent.Post(gameObject);
            AudioDebugUI.Report(pickupSoundEvent);
        }

        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }
}