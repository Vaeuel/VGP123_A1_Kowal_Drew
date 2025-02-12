using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_check : MonoBehaviour
{
    [SerializeField] private Transform groundCheck; //SerializeField allows private references to show in inspector
    [SerializeField, Range(0.01f, 0.1f)] private float groundCheckRadius = .1f; 
    [SerializeField] private LayerMask isGroundLayer;

    // Start is called before the first frame update
    void Start()
    {
        if (!groundCheck)
        {
            Debug.Log("No ground check set. creating one assing pivot is bottom center");

            // ground check initialization
            GameObject newGameObject = new GameObject(); // creates new game object in scene and names it 
            newGameObject.transform.SetParent(transform); // childs the new game object under what ever uses this script
            newGameObject.transform.localPosition = Vector3.zero; // Zeros the new object location local to its' parent
            newGameObject.name = "GroundCheck"; // renames the game object in for the hierarchy
            groundCheck = newGameObject.transform; // Sets and returns the ground check objects trans values to global variable.

        }

    }
    public bool IsGrounded()
    {

        if (!groundCheck) return false;

        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer); // Should set the global bool value based on overlap and layer mask.;
    }

}

