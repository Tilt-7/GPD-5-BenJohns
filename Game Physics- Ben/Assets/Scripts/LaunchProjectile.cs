using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float launchVelocity = 700f;
    private Vector3 launcher;

    // Update is called once per frame
    void Update()
    {
        // Check if the "Fire1" button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate a new projectile at the current position and rotation of the launcher
            GameObject launchedProjectile = Instantiate(projectile, transform.position, transform.rotation);

            // Get the Rigidbody component and apply the launch velocity
            Rigidbody rb = launchedProjectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.up * launchVelocity); // Applying force in the upward direction
            }
        }

        // Assign the launcher variable to the current position of the launcher GameObject
        launcher = transform.localPosition;

        // Update the launcher position based on player input (Horizontal and Vertical)
        launcher.x += Input.GetAxis("Horizontal") * Time.deltaTime * 10;
        launcher.y += Input.GetAxis("Vertical") * Time.deltaTime * 10;

        // Apply the updated position to the launcher
        transform.localPosition = launcher;
    }
}