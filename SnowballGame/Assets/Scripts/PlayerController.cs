using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rg;

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, walking;
    public string currentState;
    public float speed;
    public float movement;
    public string currentAnimation;

    void Start()
    {
        currentState = "Idle";
        SetCharacterState(currentState);
        rg = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    //set animation
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if(animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    //checks character state and sets the animation accordingly
    public void SetCharacterState(string state)
    {
        if(state.Equals("Idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        else if(state.Equals("walk"))
        {
            SetAnimation(walking, true, 1f);
        }
    }
    public void Move()
    {
        movement = Input.GetAxis("Horizontal");
        rg.velocity = new Vector2(movement * speed, rg.velocity.y);
        if(movement != 0)
        {
            SetCharacterState("walk");
            if(movement>0)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
            else
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
        }
        else
        {
            SetCharacterState("Idle");
        }
    }
}
