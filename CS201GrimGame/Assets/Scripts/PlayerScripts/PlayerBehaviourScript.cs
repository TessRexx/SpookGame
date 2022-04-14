// THIS SCRIPT CONTAINS PLAYER MOVEMENT FROM USER INPUT

using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    // References
    Rigidbody2D playerRB;
    Animator playerAnimator;
    Collider2D playerCollider;

    // Variables
    float playerSpeed = 2500;
    float JumpSpeed = 18;

    // Start is called before the first frame update
    void Start()
    {
        // Component References
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool playerHorizontalMove = PlayerMovement();
        AnimationChange(playerHorizontalMove);
        PlayerJump();
    }

    // Player Walk Animation Method
    private void AnimationChange(bool playerHorizontalMove)
    {
        // Changing the animator controller variable to play different animations
        playerAnimator.SetBool("Walk", playerHorizontalMove);
    }

    // Player Movement Method
    private bool PlayerMovement()
    {
        // Flips player based on movement
        bool playerHorizontalMove = Mathf.Abs(playerRB.velocity.y) > 0;
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if ((Input.GetAxis("Horizontal") > 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Getting input and moving the character
        float controlThrow = Input.GetAxis("Horizontal") * Time.deltaTime;
        Vector2 playerVelocity = new Vector2(controlThrow * playerSpeed, playerRB.velocity.y); // Velocity is 2D vector
        playerRB.velocity = playerVelocity;

        return playerHorizontalMove;
    }

    // Player Jump Method
    private void PlayerJump()
    {
        // If user taps space bar
        if (Input.GetButtonDown("Jump"))
        {
            // Determine if player is touching ground, if true then can jump
            bool IsTouchingGround = playerCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"));
            if (IsTouchingGround)
            {
                Vector2 JumpVelocity = new Vector2(0, JumpSpeed);
                playerRB.velocity += JumpVelocity;
            }
            playerAnimator.SetTrigger("Jump");
        }
    }
}

