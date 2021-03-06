using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

     public float movementSpeed = 2.4f;
    public float jumpPower = 7.5f;
    public float radius = 0.3f;
    private Button jumpButton;
    public Transform groundCheckPosition;

    public LayerMask layerGround;

    private bool isGrounded;
    private bool playerJumped = false;
    private bool canDoubleJump = false;
    [HideInInspector] public bool gameStarted = false;

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
        //fizik kullanıyosan fixedupdatede çağırmak daha mantıklı

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
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length >0; //groundcheckposition'ın radius kadar çevresinde bir bölge oluşturup layerground'a uzaklığına bakar.
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
            rb.velocity = new Vector3(0f, jumpPower, 0);
           // rb.AddForce(new Vector3(0, secondJumpPower, 0));
        }

        else if (isGrounded)
        {
            playerAnim.didJump();
            rb.velocity = new Vector3(0f, jumpPower, 0);
            //rb.AddForce(new Vector3(0, jumpPower, 0));
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
    //        Debug.Log("2 kere zıpladık");
    //    }

    //    else if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
    //    {
    //        Debug.Log("zıplıoz");
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

        }
    }


}//class
