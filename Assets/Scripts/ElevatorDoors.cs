using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Start,
    Opening,
    Open,
    Closing,
    Closed
}

public class ElevatorDoors : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public Transform leftTarget;
    public Transform rightTarget;
    private Vector3 leftStart;
    private Vector3 rightStart;
    private Vector3 leftEnd;
    private Vector3 rightEnd;

    public State state = State.Start;
    private float time = 0;
    public float startDelay;
    public float moveSpeed;

    public AudioSource audioSource;
    public AudioClip ding;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    void Start()
    {
        this.leftStart = this.leftDoor.transform.position;
        this.rightStart = this.rightDoor.transform.position;
        this.leftEnd = this.leftTarget.position;
        this.rightEnd = this.rightTarget.position;
        this.audioSource.PlayOneShot(this.ding);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.state == State.Start)
        {
            this.time += Time.deltaTime;
            if (time >= startDelay)
            {
                this.time = 0;
                this.state = State.Opening;
                this.audioSource.PlayOneShot(this.doorOpenSound);
            }
        }
        else if (this.state == State.Opening)
        {
            if (this.rightDoor.transform.position != this.rightEnd)
            {
                this.time += Time.deltaTime * this.moveSpeed;
                this.time = Mathf.Clamp(this.time, 0, Mathf.PI);
                float t = 0.5f + Mathf.Sin(this.time - Mathf.PI / 2f) * 0.5f;
                this.rightDoor.transform.position = Vector3.Lerp(this.rightStart, this.rightEnd, t);
                this.leftDoor.transform.position = Vector3.Lerp(this.leftStart, this.leftEnd, t);
            }
            else
            {
                this.time = 0;
                this.state = State.Open;
            }
        }
        else if (this.state == State.Closing)
        {
            Debug.Log("closing start");
            if (this.rightDoor.transform.position != this.rightStart)
            {
                Debug.Log("door is traveling");
                this.time += Time.deltaTime * this.moveSpeed;
                this.time = Mathf.Clamp(this.time, 0, Mathf.PI);
                float t = 0.5f + Mathf.Sin(this.time - Mathf.PI / 2f) * 0.5f;
                this.rightDoor.transform.position = Vector3.Lerp(this.rightEnd, this.rightStart, t);
                this.leftDoor.transform.position = Vector3.Lerp(this.leftEnd, this.leftStart, t);
            }
            else
            {
                Debug.Log("door at rest");
                this.time = 0;
                this.state = State.Closed;
            }
        }
    }

    public void Close()
    {
        this.state = State.Closing;
        this.audioSource.PlayOneShot(this.doorCloseSound);
    }
}
