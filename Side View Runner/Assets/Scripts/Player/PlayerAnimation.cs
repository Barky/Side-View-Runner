using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animation anim;


    private void Awake()
    {
        anim = GetComponent<Animation>();

    }

    public void didJump()
    {
        anim.Play(Tags.ANIMATION_JUMP);
        anim.PlayQueued(Tags.ANIMATION_JUMPFALL);

    }

    public void didLand()
    {
        anim.Stop(Tags.ANIMATION_JUMPFALL);
        anim.Stop(Tags.ANIMATION_JUMPLAND);
        anim.Blend(Tags.ANIMATION_JUMPLAND, 0);
        anim.CrossFade(Tags.ANIMATION_RUN);
    }

    public void playerRun()
    {
        anim.Play(Tags.ANIMATION_RUN);
    }
}
