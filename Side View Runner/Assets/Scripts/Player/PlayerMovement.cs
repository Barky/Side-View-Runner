using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //fizik kullanıyosan fixedupdatede çağırmak daha mantıklı
        PlayerMove();
    }

    void PlayerMove()
    {
        rb.velocity = new Vector3(movementSpeed, rb.velocity.y, 0f);  // jump ekleyince bunu koycaz
    }


}//class
