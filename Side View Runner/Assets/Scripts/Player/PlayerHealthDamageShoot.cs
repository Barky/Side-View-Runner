using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDamageShoot : MonoBehaviour
{
    [SerializeField] private Transform playerBullet;
    private float distanceBeforeNewPlatforms = 120f;
    private LevelGenerator level_generator;
    [HideInInspector] public bool canShoot;

    private Button ShootButton;
    private LevelGeneratorPooling level_generator_pooling;
    void Awake()
    {
        level_generator = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGenerator>();
        level_generator_pooling = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGeneratorPooling>();

        ShootButton = GameObject.Find("Shoot").GetComponent<Button>();

        ShootButton.onClick.AddListener( () => Shoot() );
    }

    void FixedUpdate()
    {
        //Fire();
    }

    public void Shoot()
    {
        if (canShoot)
        {

            Vector3 bulletPos = transform.position;
            bulletPos.y += 1.5f;
            bulletPos.x += 1f;
            Transform newbullet = (Transform)Instantiate(playerBullet, bulletPos, Quaternion.identity);
            newbullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            newbullet.parent = transform;
        }
    }
//}
//    void Fire()
//    {
//        if (Input.GetKeyDown(KeyCode.K))
//        {
//            if (canShoot)
//            {

//                Vector3 bulletPos = transform.position;
//                bulletPos.y += 1.5f;
//                bulletPos.x += 1f;
//                Transform newbullet = (Transform)Instantiate(playerBullet, bulletPos, Quaternion.identity);
//                newbullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
//                newbullet.parent = transform;
//            }
//        }
//    }

    void OnTriggerEnter(Collider target)
    {
        // kurþun bize çarparsa ölecez
        // oyunun bittiðini game controller a söyleme scripti yazýlacak.
        if(target.tag == Tags.MONSTER_BULLET || target.tag == Tags.BOUNDS)
        {

           GameplayController.instance.TakeDamage();
        }

        if(target.tag == "Health")
        {
            Debug.Log("health e çarpmýþ olmalýyýz.");
            GameplayController.instance.IncrementHealth();
            target.gameObject.SetActive(false);
        }

        //more platform çizgisinden geçince yenilerini üret
        if(target.tag == Tags.MORE_PLATFORMS)
        {
            Vector3 temp = target.transform.position;
            temp.x += distanceBeforeNewPlatforms;
            target.transform.position = temp;

            level_generator.GenerateLevel(false);
            //level_generator_pooling.poolingPlatforms();  burda bi hata var onu çözeceyiz.
        }
    }


    void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == Tags.MONSTER)
        {

           GameplayController.instance.TakeDamage();
            Debug.Log("monster a  çarpmýþ olmalýyýz.");
            target.gameObject.SetActive(false);
        }
    }






}
