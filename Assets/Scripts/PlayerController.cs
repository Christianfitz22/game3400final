using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    public float jumpHeight = 1.0f;
    [SerializeField]
    private float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool grounded;

    public AudioSource audioSource;
    public AudioMixer masterMixer;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioSource jumpSource;

    public Transform cameraPoint;
    private bool cutscene = false;
    public Transform cameraTarget;
    private GameObject camera;

    public Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        grounded = true;
        camera = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cutscene)
        {
            Movement();
        }
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y + 0.1f, -1, QueryTriggerInteraction.Ignore);
        //Vector3 boxCenter = transform.position - new Vector3(0f, transform.localScale.y / 2f, 0f);
        //return Physics.CheckBox(boxCenter, new Vector3(0.1f, 0.8f, 0.1f));
    }

    private void Movement()
    {

        bool oldGrounded = grounded;
        grounded = isGrounded();

        if (!oldGrounded && grounded && this.velocity.y < -3f)
        {
            jumpSource.PlayOneShot(landSound);
        }

        if (grounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        var movement = transform.right * xMove + transform.forward * zMove;
        if (movement != Vector3.zero && grounded)
        {
            if (!this.audioSource.isPlaying)
            {
                this.audioSource.Play();
            }
        }
        else
        {
            this.audioSource.Stop();
        }
        controller.Move(movement * walkSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded && jumpHeight != 0f)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * -3f * gravity));
            jumpSource.PlayOneShot(jumpSound);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    public void Respawn()
    {
        controller.enabled = false;
        gameObject.transform.position = respawnPoint.position;
        gameObject.transform.rotation = respawnPoint.rotation;
        velocity.y = 0;
        controller.enabled = true;
    }

    public void AddVelocity(Vector3 add)
    {
        velocity += add;
    }

    public void TransitionToSurreal()
    {
        cutscene = true;
        camera.GetComponent<CameraController>().enabled = false;
        transform.DetachChildren();
        camera.transform.SetPositionAndRotation(this.cameraPoint.position, this.cameraPoint.rotation);
        StartCoroutine(FadeOut());
    }

    public void TransitionBackToReal()
    {
        SceneManager.LoadScene("ReturnToOffice");
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(13.0f);

        float curVolume = 1f;
        while (curVolume > 0f)
        {
            camera.transform.position = Vector3.Lerp(cameraPoint.position, cameraTarget.position, 1 - curVolume);
            masterMixer.SetFloat("Volume", Mathf.Log10(curVolume) * 20f);
            curVolume -= Time.deltaTime / 10f;
            yield return null;
        }
        SceneManager.LoadScene("Floating Islands");
    }
}