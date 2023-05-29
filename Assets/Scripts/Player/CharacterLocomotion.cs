using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    private Animator anim;
    private Vector2 input;

    private Vector2 moveVec;

    private Vector2 velocity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveUpdate();
    }

    private void MoveUpdate()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        moveVec = Vector2.SmoothDamp(moveVec, input, ref velocity, 0.05f);

        anim.SetFloat("InputX", moveVec.x);
        anim.SetFloat("InputY", moveVec.y);
    }
}