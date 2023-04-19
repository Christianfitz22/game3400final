using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCloseTrigger : MonoBehaviour
{
    public ElevatorDoors doors;
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (!activated && other.tag == "Player")
        {
            doors.Close();
            activated = true;
        }
    }
}
