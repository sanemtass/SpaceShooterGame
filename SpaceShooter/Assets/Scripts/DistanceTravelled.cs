using UnityEngine;

public class DistanceTravelled : MonoBehaviour
{
    private Vector3 lastPosition;
    private float totalDistance;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        float deltaTime = Time.deltaTime;
        float distance = Vector3.Distance(lastPosition, currentPosition);

        totalDistance += distance;
        lastPosition = currentPosition;

        Debug.Log("Total Distance: " + totalDistance);
    }
}
