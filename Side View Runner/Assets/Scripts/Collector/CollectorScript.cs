using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour
{


    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == Tags.PLATFORM_TAG || target.tag == Tags.MONSTER || target.tag == Tags.HEALTH)
        {
            target.gameObject.SetActive(false);
        }
    }



}
