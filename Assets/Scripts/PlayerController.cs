using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y + 0.1f, -1, QueryTriggerInteraction.Ignore);
        //Vector3 boxCenter = transform.position - new Vector3(0f, transform.localScale.y / 2f, 0f);
        //return Physics.CheckBox(boxCenter, new Vector3(0.1f, 0.8f, 0.1f));
    }

    private void Movement()
    {
        grounded = isGrounded();
        if (grounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        var movement = transform.right * xMove + transform.forward * zMove;
        controller.Move(movement * walkSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * -3f * gravity));
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
        /*
        transform.SetParent(null);
        controller.enabled = false;
        gameObject.transform.position = respawnPoint.position;
        gameObject.transform.rotation = respawnPoint.rotation;
        velocity.y = 0;
        controller.enabled = true;
        */
    }

    public void AddVelocity(Vector3 add)
    {
        velocity += add;
    }
}