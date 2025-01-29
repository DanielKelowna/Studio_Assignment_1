using System;
using JetBrains.Annotations;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Rigidbody sphereRigidBody;
    public float ballSpeed = 2f;
    public Boolean isCollidingWithPlane;
    // Violates SRP but the application is so small that it's unesseccary to modularize

    void Start()
    {
        Debug.Log("Calling the Start method");
       
}

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        Debug.Log("Calling the Update method");

        // Modify input vector with key presses
        if (Input.GetKey(KeyCode.W)) inputVector += Vector2.up;
        if (Input.GetKey(KeyCode.S)) inputVector += Vector2.down;
        if (Input.GetKey(KeyCode.A)) inputVector += Vector2.left;
        if (Input.GetKey(KeyCode.D)) inputVector += Vector2.right;

        // Normalize vector to magnitude 1 (1.41 if two keys pressed)
        if (inputVector.magnitude > 1)
            inputVector = inputVector.normalized;

        // Convert 2D input into a 3D movement vector
        Vector3 inputXZPlane = new Vector3(inputVector.x, 0, inputVector.y);

        // Apply continuous force for movement
        sphereRigidBody.AddForce(inputXZPlane);

        // Apply jump impulse only if colliding with the plane and Spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isCollidingWithPlane)
        {
            sphereRigidBody.AddForce(Vector3.up * 5f, ForceMode.Impulse); // Jump instantly
        }

        Debug.Log("Resultant Vector: " + inputVector);
        Debug.Log("Resultant 3D Vector: " + inputXZPlane);
    }

    // Bugs: Colliding with wall causes upward motion, which when stacked with the jump causes ball to exit confined area
    // Fixable, but outside the scope of this assignment
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            isCollidingWithPlane = true;
            Debug.Log("Sphere started colliding with the Plane.");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            isCollidingWithPlane = false;
            Debug.Log("Sphere stopped colliding with the Plane.");
        }
    }
}
