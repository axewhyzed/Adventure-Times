using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A Prefab is basically a template for an object

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    private Animator animator;
    [SerializeField] float movementSpeed = 6f; //SerializeField will allow us
                    //to edit these values directly as a field in PlayerMovement Script section in unity
                    // public keyword also has the same effect but then other scripts can also access 
    [SerializeField] float jumpForce = 5f;
    [SerializeField] AudioSource enemyKill;
    
    // Start is called before the first frame update

    void Start()
    {
	animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        //if (Input.GetButtonDown("Jump") && isGrounded())
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            // Instead of 0, we use rb.velocity.(axis name) so we don't stop moving as soon as we jump
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
		    animator.SetBool("isWalking", true);
	    }
	    else{
		    animator.SetBool("isWalking", false);
	    }

        if((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && Input.GetKey("left ctrl"))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        //rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);


        // For GetAxisRaw, it will directly go to zero suddenly thats why used in 2D, not 3D

    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy Head"))
        {
            Debug.Log("Enemy Killed!");
            Destroy(collision.transform.parent.gameObject);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce * 2, rb.velocity.z); // Make a bigger jump when enemy killed
            enemyKill.Play();
        }
    }

    /*bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }*/

}
