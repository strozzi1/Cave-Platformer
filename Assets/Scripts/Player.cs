using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//end of video 2, ready to start video 3
[RequireComponent (typeof(Controller2D)) ]
public class Player : MonoBehaviour {

    float jumpHeight = 2.5f;
    float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;

    float movespeed = 6f;
    float gravity = -20;
    float jumpVelocity = 8;

    float velocityXSmoothing;

    Vector3 velocity;

	Controller2D controller;

	void Start () {
		controller = GetComponent<Controller2D>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("gravity/velocity: ");
	}
	
    void Update()
    {

        if(controller.collisions.above|| controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));          //save both x and y input as a vector

        if(Input.GetButtonDown("Jump") && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX  = input.x * movespeed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);                 //call controller scripts move function
    }

}
