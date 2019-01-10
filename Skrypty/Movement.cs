using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;

	// Update is called once per frame
	void Update () {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jumping",true);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
    }

    private void FixedUpdate()
    {
        // poruszanie
        controller.Move(horizontalMove * Time.fixedDeltaTime,false ,jump);
        jump = false;
    }
}
