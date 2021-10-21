using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    public float movementSpeed = 5f;
    public float jumpPower = 10f;
    public float secondJumpPower = 10f;
    public float radius = 0.3f;

    public Transform groundCheckPosition;

    public LayerMask layerGround;

    private bool isGrounded;
    private bool playerJumped = false;
    private bool canDoubleJump = false;
    private bool gameStarted = false;

    public PlayerAnimation playerAnim;

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("�al��t� be oh sonunda amk");
        gameStarted = true;
        playerAnim.playerRun();
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    void FixedUpdate()
    {
        //fizik kullan�yosan fixedupdatede �a��rmak daha mant�kl�

        if (gameStarted)
        {
            PlayerMove();
            playerGrounded();
            playerJump();
        }
    }

    void PlayerMove()
    {
        rb.velocity = new Vector3(movementSpeed, rb.velocity.y, 0f);  // jump ekleyince bunu koycaz
    }

    void playerGrounded()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length >0; //groundcheckposition'�n radius kadar �evresinde bir b�lge olu�turup layerground'a uzakl���na bakar.
        if(isGrounded && playerJumped)
        {
            playerJumped = false;
            playerAnim.didLand();
        }
    }

    void playerJump()
    {


        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump)
        {
            canDoubleJump = false;
            rb.AddForce(new Vector3(0, secondJumpPower, 0));
        }

        else if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            playerAnim.didJump();
            rb.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canDoubleJump = true;
        } 
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == Tags.PLATFORM_TAG)
        {
            Debug.Log("platformday�k");
        }
    }


}//class
