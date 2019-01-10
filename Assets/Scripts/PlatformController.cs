using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RaycastController
{

    public Vector3 move;
    public LayerMask passengerMask;             //the player layer

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRaycastOrigins();


        Vector3 velocity = move * Time.deltaTime;
        MovePassengers(velocity);
        transform.Translate(velocity);

    }

    void MovePassengers(Vector3 velocity)
    {
        HashSet<Transform> movedPassengers = new HashSet<Transform>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);
        //Vertically
        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;
            for (int i = 0; i < verticalraycount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft; //if going down then rayOrigin is bottom left, else it's top left
                rayOrigin += Vector2.right * (verticalrayspacing * i);                          // sets current ray origin for each ray according to the direction the object is going
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask); //vertical raycats from ray origin in direction we are going, of raylength
                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = (directionY == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

                        hit.transform.Translate(new Vector3(pushX, pushY));

                    }
                }

            }
        }

        //Horizontally
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;
            for (int i = 0; i < horizontalraycount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight; //if going down then rayOrigin is bottom left, else it's top left
                rayOrigin += Vector2.up * (horizontalrayspacing * i);                          // sets current ray origin for each ray according to the direction the object is going

                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask); //vertical raycats from ray origin in direction we are going, of raylength
                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                        float pushY = 0;

                        hit.transform.Translate(new Vector3(pushX, pushY));

                    }
                }
            }

        }



        // Passenger on top of horizontally or downward moving platform
        if(directionY==-1 || velocity.y==0 && velocity.x !=0)
        {
            float rayLength = skinWidth * 2;
            for (int i = 0; i < verticalraycount; i++)
            {
                Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalrayspacing * i);                          // sets current ray origin for each ray according to the direction the object is going

                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask); //vertical raycats from ray origin in direction we are going, of raylength
                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x;
                        float pushY = velocity.y;

                        hit.transform.Translate(new Vector3(pushX, pushY));

                    }
                }
            }
        }

    }
}