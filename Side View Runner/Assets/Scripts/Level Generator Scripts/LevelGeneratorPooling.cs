using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorPooling : MonoBehaviour
{
    [SerializeField] private Transform platform, platform_parent;

    [SerializeField] private Transform monster, monster_parent;

    [SerializeField] private Transform health, health_parent;

    [SerializeField] private int levelLength = 100;

    [SerializeField] private float distance_between_platforms = 15f;

    [SerializeField] private float min_position_y=0f, max_position_y=7f;

    [SerializeField] private float chance_for_monster=0.25f, chance_for_health=0.1f;

    [SerializeField] private float health_min_y=1f, health_max_y=3f;

    private float platform_last_pos_x;

    private Transform[] platformArray; //b?t?n platformlar? bu list tutacak

    private void Start()
    {
        CreatePlatforms();
    }

    void CreatePlatforms()
    {
        platformArray = new Transform[levelLength];
        Debug.Log("ilki ?al???o");
        for (int i =0; i < platformArray.Length; i++)
        {
            Transform newPlatform = (Transform)Instantiate(platform, Vector3.zero, Quaternion.identity);
            platformArray[i] = newPlatform;

            float platformPositionY = Random.Range(min_position_y, max_position_y);
            Vector3 platformPosition;

            if (i < 2)
            {//player?n ba?lad??? ilk 2si onunla ayn? yde olsun die
                platformPositionY = 0f;
            }

            platformPosition = new Vector3(distance_between_platforms * i, platformPositionY, 0);
            platform_last_pos_x = platformPosition.x;

            platformArray[i].position = platformPosition;
            platformArray[i].parent = platform_parent;

            //spawn monsters and healths scripts later
            SpawnHealthandMonster(platformPosition, i, true);
        }


        }

    public void poolingPlatforms()
    {
        Debug.Log("ikincisi ?al???o");
        for (int i = 0; i < platformArray.Length; i++)
        {
            if (!platformArray[i].gameObject.activeInHierarchy)
            {
                platformArray[i].gameObject.SetActive(true);
                float platformPositionY = Random.Range(min_position_y, max_position_y);
                Vector3 platformPosition = new Vector3(distance_between_platforms + platform_last_pos_x, platformPositionY, 0);

                platformArray[i].position = platformPosition;
                platform_last_pos_x = platformPosition.x;

                //spawn monsters and healths scripts later
                SpawnHealthandMonster(platformPosition, i, false);
            }
        }
    }

    void SpawnHealthandMonster(Vector3 platformposition, int i, bool gameStarted)
    {
        if (i > 2)
        {
            if(Random.Range(0,1f) < chance_for_monster)
            {
                if (gameStarted)
                {
                    platformposition = new Vector3(distance_between_platforms * i, platformposition.y + 0.1f, 0);
                }
                else
                {
                    platformposition = new Vector3(distance_between_platforms + platform_last_pos_x, platformposition.y + 0.1f, 0);
                }
                Transform create_monster = (Transform)Instantiate(monster, platformposition, Quaternion.Euler(0, -90, 0));
                create_monster.parent = monster_parent;
            }

            if (Random.Range(0, 1f) < chance_for_health)
            {

                if (gameStarted)
                {
                    float health_y = Random.Range(health_min_y, health_max_y);
                    platformposition = new Vector3(distance_between_platforms * i, platformposition.y + health_y, 0);
                }
                else
                {
                    float health_y = Random.Range(health_min_y, health_max_y);
                    platformposition = new Vector3(distance_between_platforms + platform_last_pos_x, platformposition.y + health_y, 0);
                }
                Transform create_health = (Transform)Instantiate(health, platformposition, Quaternion.identity);
                create_health.parent = health_parent;
            }
        }
    }
}
