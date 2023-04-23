using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public List<GameObject> objects;
    private bool activated = false;

    public AudioSource audioSource;
    public List<AudioClip> changeSounds;

    void OnTriggerEnter(Collider other)
    {
        if (!this.activated && other.tag == "Player")
        {
            foreach (GameObject target in this.objects)
            {
                target.SetActive(!target.activeInHierarchy);
            }
            this.activated = true;
            StartCoroutine(PlayList());
        }
    }

    IEnumerator PlayList()
    {
        Debug.Log("player list called");
        foreach (AudioClip soundEffect in this.changeSounds)
        {
            Debug.Log("first sound played");
            audioSource.PlayOneShot(soundEffect);
            yield return new WaitForSeconds(soundEffect.length);
        }
    }
}
