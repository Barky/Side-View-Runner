using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTarget;

    public float offsetZ = -8.5f; //playerdan ne kdr uzak olacaðý
    public float offsetX = 0f;
    public float constantY = 2.25f;
    public float cameraLerpTime = 0.05f;
    public float radius;



    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Update()
    {
        if (playerTarget)
        {
            Vector3 TargetPosition = new Vector3(playerTarget.position.x + offsetX, constantY, playerTarget.position.y + offsetZ);
            transform.position = Vector3.Lerp(transform.position, TargetPosition, cameraLerpTime); // þu anki pozisyon, target pozisyon, oraya geçme zamaný
        }
    }

}
