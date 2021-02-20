using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("The force applied to the player to move it on the ground.")]
    private float normalMovementForce = 60;
    [SerializeField, Tooltip ("The force applied to the player to move it on the ground while boosted.")]
    private float boostMovementForce = 100;
    [SerializeField, Tooltip("This influences the maximum speed of the player while they're on the ground")]
    private float runningDifficulty = 1;
    [SerializeField, Tooltip("The force applied to the player to move it in the air")]
    private float airtimeMovementForce = 5;
    [SerializeField, Tooltip("The impulse force applied to the player to make it jump")]
    private float jumpForce = 3;
    [SerializeField, Tooltip("The maximum number of jumps the player is able to make before they land again")]
    private int maxJumpsRemaining = 2;
    [SerializeField, Tooltip("The amount of time in seconds that the jetPack can be used for at a time.")]
    private float jetPackFuelMax = 3;
    [SerializeField, Tooltip("The Upwards force applied to the player while the jetpack is being used")]
    private float jetPackForce = 20;
    [SerializeField, Tooltip("The wearable jetpack prefab that the player will wear while the jetpack is active")]
    private GameObject jetPack;
    

    //how many jumps the player is able to make right now before they land back on the ground
    private int jumpsRemaining = 1;

    private Rigidbody2D rb;
    private Collider2D chCollider;
    private bool grounded = false;
    private float timeSinceLeftGround = 0;
    //groundNormal is the normal of the ground at the point of collision with the player.
    private Vector2 groundNormal;
    private bool jumpHeldDown;
    private float boostTimer;
    private float movementForce;
    private float jetPackFuel;
    private bool jetPackEnabled;
    private GameObject jetPackInstance;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        chCollider = gameObject.GetComponent<Collider2D>();
        movementForce = normalMovementForce;
        jetPackInstance = Instantiate(jetPack, gameObject.transform);
        jetPackInstance.SetActive(false);
    }

    //picking up powerups
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Pizza")
        {
            boostTimer += 10;
            Destroy(collider.gameObject);
        }
        if (collider.tag == "JetPack")
        {
            jetPackFuel = jetPackFuelMax;
            Destroy(collider.gameObject);
            jetPackInstance.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).collider.tag == "WallOfDeath")
            {
                SceneManager.LoadScene(0);//if the wall touches the player, reload the scene
            }
        }
        grounded = true;
        jetPackEnabled = false;

        //this recharges the jumps when you land on the a surface that isn't a roof
        if (jumpsRemaining != maxJumpsRemaining)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (collision.GetContact(i).normal.y >= -0.1f)
                {
                    jumpsRemaining = maxJumpsRemaining;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        grounded = true;
        timeSinceLeftGround = 0;

        Vector2 contactNormalSum = Vector2.zero;
        int noOfContacts = collision.contactCount;
        for (int i = 0; i < noOfContacts; i++)
        {
            contactNormalSum += collision.GetContact(i).normal;
        }
        //ground normal is the average normal of all points of contact
        groundNormal = contactNormalSum / noOfContacts;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        groundNormal = Vector2.up;
    }

    private bool JumpPressedThisFrame()
    {
        if (Input.GetAxis("Jump") <= 0 && jumpHeldDown == true)
        {
            jumpHeldDown = false;
            return false;
        }
        else if (Input.GetAxis("Jump") > 0 && jumpHeldDown == false)
        {
            jumpHeldDown = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    void FixedUpdate()
    {
        //using PizzaBoost
        if (boostTimer <= 0)
        {
            movementForce = normalMovementForce;
        }
        else if (boostTimer > 0)
        {
            movementForce = boostMovementForce;
            boostTimer -= Time.deltaTime;
        }

        if (jetPackFuel <= 0)
        {
            jetPackInstance.SetActive(false);
        }

        //using Jetpack
        if (Input.GetAxis("Jump") > 0 && jetPackFuel > 0 && timeSinceLeftGround > 0.25f)
        {
            rb.AddForce(Vector3.up * jetPackForce, ForceMode2D.Force);
            jetPackFuel -= Time.deltaTime;
            Debug.Log(jetPackFuel);
        }
        else if(JumpPressedThisFrame() && jumpsRemaining > 0)//jumping and doublejumping
        {
            if (timeSinceLeftGround < 0.25f && jumpsRemaining == maxJumpsRemaining)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                rb.AddForce(groundNormal.normalized * jumpForce, ForceMode2D.Impulse);
            }
            else if (jumpsRemaining < maxJumpsRemaining)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            jumpsRemaining -= 1;
        }

        if (timeSinceLeftGround < 0.3)
        {
            timeSinceLeftGround += Time.fixedDeltaTime;
        }

        if (grounded)
        {
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * movementForce, 0));
            rb.AddForce(new Vector2(-rb.velocity.x * runningDifficulty, 0));
        }
        else
        {
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * airtimeMovementForce, 0));
        }
    }
}
