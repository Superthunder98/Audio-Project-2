using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerActivateGameObject : MonoBehaviour
{

    public GameObject objectToActivate;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
            Destroy(gameObject);
        }
    }
}
