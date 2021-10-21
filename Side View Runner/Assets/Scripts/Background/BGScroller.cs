using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [HideInInspector] public bool canScroll;
    public float offsetSpeed = -0.001f;

    private Renderer myRenderer;

     void Awake()
    {
        myRenderer = GetComponent<MeshRenderer>();

    }

     void Update()
    {
        if (canScroll)
        {
            myRenderer.material.mainTextureOffset -= new Vector2(offsetSpeed, 0);
        }
    }






}
