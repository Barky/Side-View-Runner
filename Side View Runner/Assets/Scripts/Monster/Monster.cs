using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject monsterDiedEffect;
    public Transform Bullet;
    private float distancefromPlayertoStartMove = 19f;
    private float moveSpeedMinimum = 0.5f;
    private float moveSpeedMaximum = 1f;

    //private bool moveRight = false;
    private float movementSpeed;
    private bool isPlayerInRegion = false;

    private Transform playerTransform;
    [SerializeField] public bool canShootandMove = false;
    PlayerMovement movements;

    private void Awake()
    {

        playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
        movements = GameObject.Find("Player").GetComponent<PlayerMovement>();

        movementSpeed = Random.Range(moveSpeedMinimum, moveSpeedMaximum);

        

    }
    private void Start()
    {
        StartCoroutine(constantshoot());

    }
    private void Update()
    {
        canShootandMove  = movements.gameStarted;
        
        

        if (playerTransform)
        {
            float distanceFromPlayer = (playerTransform.position - transform.position).magnitude;

            if (distanceFromPlayer < distancefromPlayertoStartMove)
            {
                isPlayerInRegion = true;
            }
            else
            {
                isPlayerInRegion = false;
            }

            if (isPlayerInRegion && !canShootandMove)
            {
                Destroy(gameObject);
            }


            if (isPlayerInRegion && canShootandMove)
            {
                
                    //monsters running only to player(left)
                        transform.position = new Vector3(transform.position.x - Time.deltaTime * movementSpeed, transform.position.y, transform.position.z);

            }
            else
            {

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            }
        }
    }
    IEnumerator constantshoot()
    {
        while (true)
        {
            if (playerTransform)
            {
                if (isPlayerInRegion && canShootandMove)
                {
                    Vector3 bulletPosition = transform.position;
                    bulletPosition.y += 1f;
                    bulletPosition.x -= 1f;
                    Transform newBullet = (Transform)Instantiate(Bullet, bulletPosition, Quaternion.identity);

                    newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 300f);
                    newBullet.parent = transform;

                    yield return new WaitForSeconds(2.5f);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                //without this, while looping empty forever and game will crash.
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.PLAYER_BULLET)
        {
            GameplayController.instance.IncrementScore(200);
            MonsterDied();
        }
    }

    private void OnCollisionEnter(Collision target)
    {


        if(target.gameObject.tag == Tags.PLAYER_TAG)
        {
            MonsterDied();
        }
    }

    void MonsterDied()
    {
        Vector3 effectposition = transform.position;
        effectposition.y += 2f;
        Instantiate(monsterDiedEffect, effectposition, Quaternion.identity);
        Destroy(gameObject);
    }

}
