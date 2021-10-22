using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject monsterDiedEffect;
    public Transform Bullet;
    public float distancefromPlayertoStartMove = 20f;
    public float moveSpeedMinimum = 1f;
    public float moveSpeedMaximum = 2f;

    private bool moveRight = false;
    private float movementSpeed;
    private bool isPlayerInRegion = false;

    private Transform playerTransform;
    public bool canShoot;

   // private string FUNCTION_TO_INVOKE = "startShooting";   kafam karýþmasýn diye yazmadým þimdi. ama normalde yap bunu.

    private void Start()
    {
        if(Random.Range(0.0f, 1.0f) > 0.5f)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
        movementSpeed = Random.Range(moveSpeedMinimum, moveSpeedMaximum);
        playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;

    }
    private void Update()
    {
        if (playerTransform)
        {
            float distanceFromPlayer = (playerTransform.position - transform.position).magnitude;
            if(distanceFromPlayer < distancefromPlayertoStartMove)
            {
                if (moveRight)
                {
                    transform.position = new Vector3(transform.position.x + Time.deltaTime * movementSpeed, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x - Time.deltaTime * movementSpeed, transform.position.y, transform.position.z);

                }
                if (!isPlayerInRegion)
                {
                    if (canShoot)
                    {
                        InvokeRepeating("startShooting", 0.5f, 1.5f); // 1.5 saniyede bir, 0.5 saniyelik startshooting fonksiyonunu döndürecek
                    }
                    isPlayerInRegion = true;
                }

            }
            else
            {
                CancelInvoke("startShooting");
            }
        }
    }

    void startShooting()
    {
        if (playerTransform)
        {
            Vector3 bulletPosition = transform.position;
            bulletPosition.y += 1.5f;
            bulletPosition.x -= 1f;
            Transform newBullet = (Transform)Instantiate(Bullet, bulletPosition, Quaternion.identity);

            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            newBullet.parent = transform;
        }
    }
}
