using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

     public float movementSpeed = 2.4f;
    public float jumpPower = 10f;
    public float secondJumpPower = 10f;
    public float radius = 0.3f;
    private Button jumpButton;
    public Transform groundCheckPosition;

    public LayerMask layerGround;

    private bool isGrounded;
    private bool playerJumped = false;
    private bool canDoubleJump = false;
    private bool gameStarted = false;

    private PlayerAnimation playerAnim;

    public GameObject smokePosition;

    private BGScroller bg_scroller;


    private PlayerHealthDamageShoot playerShoot;
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        gameStarted = true;
        smokePosition.SetActive(true);
        playerShoot.canShoot = true;
        GameplayController.instance.canCountScore = true;
        bg_scroller.canScroll = true;
        playerAnim.playerRun();
    }

    private void Start()
    {
        smokePosition.SetActive(false);
        StartCoroutine(StartGame());
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
        bg_scroller = GameObject.Find(Tags.BACKGROUND_OBJ).GetComponent<BGScroller>();
        playerShoot = GetComponent<PlayerHealthDamageShoot>();
        jumpButton = GameObject.Find("Jump").GetComponent<Button>();
        jumpButton.onClick.AddListener( () => Jump());
    }

    void FixedUpdate()
    {
        //fizik kullanýyosan fixedupdatede çaðýrmak daha mantýklý

        if (gameStarted)
        {
            PlayerMove();
            playerGrounded();
            //playerJump();
        }
    }

    void PlayerMove()
    {
        rb.velocity = new Vector3(movementSpeed, rb.velocity.y, 0f);  // jump ekleyince bunu koycaz
    }

    void playerGrounded()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length >0; //groundcheckposition'ýn radius kadar çevresinde bir bölge oluþturup layerground'a uzaklýðýna bakar.
        if(isGrounded && playerJumped)
        {
            playerJumped = false;
            playerAnim.didLand();
        }
    }
    public void Jump()
    {
        if (!isGrounded && canDoubleJump)
        {
            canDoubleJump = false;
            rb.AddForce(new Vector3(0, secondJumpPower, 0));
            Debug.Log("2 kere zýpladýk");
        }

        else if (isGrounded)
        {
            Debug.Log("zýplýoz");
            playerAnim.didJump();
            rb.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canDoubleJump = true;
        }
    }
    //void playerJump()
    //{


    //    if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump)
    //    {
    //        canDoubleJump = false;
    //        rb.AddForce(new Vector3(0, secondJumpPower, 0));
    //        Debug.Log("2 kere zýpladýk");
    //    }

    //    else if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
    //    {
    //        Debug.Log("zýplýoz");
    //        playerAnim.didJump();
    //        rb.AddForce(new Vector3(0, jumpPower, 0));
    //        playerJumped = true;
    //        canDoubleJump = true;
    //    } 
    //}

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == Tags.PLATFORM_TAG)
        {
            Debug.Log("platformdayýk");
        }
    }


}//class
