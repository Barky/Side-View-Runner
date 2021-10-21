using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDamageShoot : MonoBehaviour
{

    private float distanceBeforeNewPlatforms = 120f;
    private LevelGenerator level_generator;

    private void Awake()
    {
        level_generator = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGenerator>();
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == Tags.MORE_PLATFORMS)
        {
            Vector3 temp = target.transform.position;
            temp.x += distanceBeforeNewPlatforms;
            target.transform.position = temp;

            level_generator.GenerateLevel(false);
        }
    }









}
