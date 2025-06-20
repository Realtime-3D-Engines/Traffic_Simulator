using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 5f;
    public float stopDistance = 1.5f;
    public bool isNSLane = true;

    private int currentWaypointIndex = 0;

    public TrafficLightController assignedLight; // Assign a traffic light to this car


    void Update()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        // Check for car ahead using raycast to avoid collisions
        RaycastHit hit;
        float rayDistance = 2f;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Car"))
            {
                // Car ahead detected, stop moving
                return;
            }
        }

        Transform target = waypoints[currentWaypointIndex];
        Vector3 direction = target.position - transform.position;

        // Stop if red light is ahead within stopDistance

        if (assignedLight != null && assignedLight.IsRedLightActive && direction.magnitude < stopDistance)
        {
            // Red light is active and car is close to the waypoint, stop moving
            return;
        }


        // Move toward next waypoint
        transform.position += direction.normalized * speed * Time.deltaTime;

        // Rotate smoothly toward waypoint
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
        }

        // Switch to next waypoint if close
        if (direction.magnitude < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }
}
