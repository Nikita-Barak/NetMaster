using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    [SerializeField] float radius = 3.0f;
    [SerializeField] LayerMask lineOfSightLayers;
    private GameObject player;
    private AnimationController enemyAnimator;
    private bool hasLineOfSight = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAnimator = GetComponent<AnimationController>();
    }

    void Update()
    {
        if (player != null && DistanceFromPlayer() <= radius && hasLineOfSight)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            // Calculate the angle and determine animation direction
            Vector2 direction = DirectionTowardsPlayer().normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            DetermineAnimation(angle);
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = DirectionTowardsPlayer();
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, DistanceFromPlayer(), lineOfSightLayers);

        if (ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag("Player");
            if (hasLineOfSight)
            {
                Debug.DrawRay(transform.position, direction, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, direction, Color.red);
            }
        }
    }

    float DistanceFromPlayer()
    {
        if (player == null) return float.MaxValue; // Avoid null reference
        return Vector2.Distance(transform.position, player.transform.position);
    }

    Vector2 DirectionTowardsPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.z = 0; // Ensure movement is limited to 2D
        return direction;
    }

    // This method sets values for the animation controller in order to determine which animation to play based on the direction of movement towards the player.
    // The direction vector's degree could be found in one of 8 "pizza slices", and the one slice that the degree is at determines the animation that needs to be played.
    // For the sake of controlling the animations, assigning the values -1/0/1 and passing them to the HandleMovement() method suffices.
    void DetermineAnimation(float angle)
    {
        // Normalize angle to 0-360 range
        if (angle < 0) angle += 360f;

        float horizontal = 0f;
        float vertical = 0f;

        // Determine the direction based on the angle
        if (angle >= 22.5f && angle < 67.5f) // Top-Right
        {
            horizontal = 1f;
            vertical = 1f;
        }
        else if (angle >= 67.5f && angle < 112.5f) // Up
        {
            horizontal = 0f;
            vertical = 1f;
        }
        else if (angle >= 112.5f && angle < 157.5f) // Top-Left
        {
            horizontal = -1f;
            vertical = 1f;
        }
        else if (angle >= 157.5f && angle < 202.5f) // Left
        {
            horizontal = -1f;
            vertical = 0f;
        }
        else if (angle >= 202.5f && angle < 247.5f) // Bottom-Left
        {
            horizontal = -1f;
            vertical = -1f;
        }
        else if (angle >= 247.5f && angle < 292.5f) // Down
        {
            horizontal = 0f;
            vertical = -1f;
        }
        else if (angle >= 292.5f && angle < 337.5f) // Bottom-Right
        {
            horizontal = 1f;
            vertical = -1f;
        }
        else // Right (angle >= 337.5f || angle < 22.5f)
        {
            horizontal = 1f;
            vertical = 0f;
        }

        // Send the values to the animator
        enemyAnimator.HandleMovement(horizontal, vertical, false);
    }
}
