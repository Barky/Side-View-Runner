using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTarget;

    private float offsetZ = -8.5f; //playerdan ne kdr uzak olacaðý
    private float offsetX = 8f;
    private float constantY = 3.52f;
    private float cameraLerpTime = 0.05f;



    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Update()
    {
        if (playerTarget)
        {
            Vector3 TargetPosition = new Vector3(playerTarget.position.x + offsetX, constantY, playerTarget.position.z + offsetZ);
            transform.position = Vector3.Lerp(transform.position, TargetPosition, cameraLerpTime); // þu anki pozisyon, target pozisyon, oraya geçme zamaný
        }
    }

}
