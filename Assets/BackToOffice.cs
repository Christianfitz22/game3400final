using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToOffice : MonoBehaviour
{
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (!this.activated && other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().TransitionBackToReal();
        }
    }
}
