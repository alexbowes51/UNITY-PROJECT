using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public GameObject leftBarrel; // Left barrel GameObject
    public GameObject rightBarrel; // Right barrel GameObject
    public GameObject projectilePrefab; // Projectile prefab to instantiate
    public float fireRate = 1f; // Fire rate in seconds
    public float projectileSpeed = 10f; // Speed of the projectile

    private float fireTimer = 0f; // Timer to track fire rate
    private bool isLeftBarrelNext = true; // Toggle between barrels

    void Update()
    {
        // Ensure player exists
        if (player != null)
        {
            // Get direction to the player
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0; // Zero out the Y-axis to constrain rotation to horizontal

            // Rotate to face the player
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;

            // Handle firing
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Fire();
                fireTimer = 0f;
            }
        }
    }

    void Fire()
    {
        // Determine the firing barrel
        GameObject firePoint = isLeftBarrelNext ? leftBarrel : rightBarrel;

        // Create the projectile at the barrel's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);

        // Set the projectile's velocity
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.transform.forward * projectileSpeed;
        }

        // Toggle barrel for the next shot
        isLeftBarrelNext = !isLeftBarrelNext;
    }
}
