using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private float lifeTime = 6f;
    private float startY;

    private Transform player;


    private void Awake()
    {
        //startY = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        startY = transform.position.y;
        StartCoroutine(turnoffBullet());
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        if (player)
        {
            if (gameObject.tag == "PlayerBullet")
            {
                if (gameObject.transform.position.x - player.transform.position.x > 19f)
                {
                    Destroy(gameObject);
                }
            }
            else if (gameObject.tag == "MonsterBullet")
            {
                if (gameObject.transform.position.x - player.transform.position.x < -6.3f)
                {
                    Destroy(gameObject); //MissingReferenceException: The object of type 'Transform' has been destroyed but you are still trying to access it.
                                         //player ölünce veriyor bu hatayý.


                }
            }

        }
    }

    IEnumerator turnoffBullet()
    {

        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.MONSTER || target.tag == Tags.PLAYER_TAG || target.tag == Tags.MONSTER_BULLET || target.tag == Tags.PLAYER_BULLET)
        {
            Destroy(gameObject);
        }
    }


}
