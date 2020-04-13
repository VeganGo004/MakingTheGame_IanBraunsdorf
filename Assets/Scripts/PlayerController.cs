using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private Animator playerAnim;
    public float forceMultiplier;
    public float gravityMultiplier;
    public bool isonGround = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isonGround && !gameOver)
        {
            playerRigidBody.AddForce(Vector3.up * forceMultiplier, ForceMode.Impulse);
            isonGround = false;
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // game over if the player hits an obstacle
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
        // set on ground state to true if we hit the ground
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isonGround = true;
        }
    }
}
