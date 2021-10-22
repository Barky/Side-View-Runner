using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float lifeTime = 5f;
    private float startY;

    private void Start()
    {
        startY = transform.position.y;
    }

    private void Update()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }

    IEnumerator turnoffBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == Tags.MONSTER || target.tag == Tags.PLAYER_TAG || target.tag == Tags.MONSTER_BULLET || target.tag ==Tags.PLAYER_BULLET )
        {
            gameObject.SetActive(false);
        }
    }


}
