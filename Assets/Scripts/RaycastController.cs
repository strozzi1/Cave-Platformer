using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour {

    public LayerMask collisionMask;

    public const float skinWidth = .015f;
    public int horizontalraycount = 4;                                     //rays shot from the sides
    public int verticalraycount = 4;                                        // rays shot from top and bottom

    [HideInInspector]
    public float horizontalrayspacing;                                             //space between rays 
    [HideInInspector]
    public float verticalrayspacing;

    [HideInInspector]
    public BoxCollider2D collider;
    public RaycastOrigins raycastOrigins;

    public virtual void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;                                         //get bounds of our collider
        bounds.Expand(skinWidth * -2);                                           //give our bounds a little buffer room

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);    //set our bounds
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {                                                 //simply calculates space between rays based on bounds
        Bounds bounds = collider.bounds;                                        //get bounds of our collider
        bounds.Expand(skinWidth * -2);                                           //skin buffer width
                                                                                 //(2 is minimum number of rays -  for the corners, no upper limit)
        horizontalraycount = Mathf.Clamp(horizontalraycount, 2, int.MaxValue);  //Makes sure raycount never goes below 2 (the corners)
        verticalraycount = Mathf.Clamp(verticalraycount, 2, int.MaxValue);

        horizontalrayspacing = bounds.size.y / (horizontalraycount - 1);          //get distance between rays y size divided by number of rays-1
        verticalrayspacing = bounds.size.x / (verticalraycount - 1);
    }


    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;               //our bounds
        public Vector2 bottomLeft, bottomRight;
    }
}
