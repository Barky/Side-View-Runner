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

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //fizik kullanýyosan fixedupdatede çaðýrmak daha mantýklý
        PlayerMove();
        playerGrounded();
        playerJump();
    }

    void PlayerMove()
    {
        rb.velocity = new Vector3(movementSpeed, rb.velocity.y, 0f);  // jump ekleyince bunu koycaz
    }

    void playerGrounded()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length >0; //groundcheckposition'ýn radius kadar çevresinde bir bölge oluþturup layerground'a uzaklýðýna bakar.
        
    }

    void playerJump()
    {


        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump)
        {
            Debug.Log("ikinci jump");
            canDoubleJump = false;
            rb.AddForce(new Vector3(0, secondJumpPower, 0));
        }

        else if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {

            Debug.Log("ilk jump");
            rb.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canDoubleJump = true;
        } 
    }

}//class
