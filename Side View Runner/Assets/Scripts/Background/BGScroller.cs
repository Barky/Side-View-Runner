using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [HideInInspector] public bool canScroll = false;
    public float offsetSpeed = -0006f;

    private Renderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<MeshRenderer>();

    }

    private void Update()
    {
        if (canScroll)
        {
            myRenderer.material.mainTextureOffset = new Vector2(offsetSpeed, 0);
        }
    }






}
