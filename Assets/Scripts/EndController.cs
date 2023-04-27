using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    public AudioSource phoneRing;

    public AudioClip elevatorDing;
    public AudioClip doorClose;
    public AudioClip phonePickup;
    public AudioClip hello;

    public GameObject blackscreen;
    public GameObject titleCard;
    public GameObject credits;
    public GameObject playAgain;

    private bool ringing = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndStart());
    }

    IEnumerator EndStart()
    {

        phoneRing.PlayOneShot(elevatorDing);
        yield return new WaitForSeconds(elevatorDing.length);
        phoneRing.PlayOneShot(doorClose);
        yield return new WaitForSeconds(15f);

        phoneRing.Play();
        ringing = true;
    }

    private void Update()
    {
        if (ringing && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(EndSequence());
        }
    }

    IEnumerator EndSequence()
    {
        ringing = false;
        phoneRing.Stop();
        blackscreen.SetActive(true);
        phoneRing.PlayOneShot(phonePickup);
        yield return new WaitForSeconds(1f);
        phoneRing.PlayOneShot(hello, 3f);
        yield return new WaitForSeconds(hello.length);
        titleCard.SetActive(true);
        yield return new WaitForSeconds(5f);
        credits.SetActive(true);
        yield return new WaitForSeconds(5f);
        playAgain.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
