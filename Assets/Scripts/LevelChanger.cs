using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public List<GameObject> objects;
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (!this.activated && other.tag == "Player")
        {
            foreach (GameObject target in this.objects)
            {
                target.SetActive(!target.activeInHierarchy);
            }
            this.activated = true;
        }
    }
}
