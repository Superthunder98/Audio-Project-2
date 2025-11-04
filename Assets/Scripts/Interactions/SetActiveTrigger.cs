using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveTrigger : MonoBehaviour
{

    public GameObject objectToTarget;
    public bool setToActive = false;


    public void OnTriggerEnter(Collider other)
    {
        if (setToActive && other.CompareTag("Player"))
        {
            objectToTarget.SetActive(true);
        }
        else
        {
            objectToTarget.SetActive(false);
        }
        Destroy(gameObject);
    }


}
