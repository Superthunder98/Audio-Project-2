using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhenTriggered : MonoBehaviour
{

     public bool disableAfterOneTrigger = false;
     BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (disableAfterOneTrigger == true)
        {
            boxCollider.enabled = false;
        }
    }
}