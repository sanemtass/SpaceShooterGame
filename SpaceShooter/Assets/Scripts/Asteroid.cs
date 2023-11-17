// Asteroid.cs
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int damage = 10;  // Asteroidin verdiği hasar miktarı

    private void OnTriggerEnter2D(Collider2D other)
    {
        Spacecraft spacecraft = other.GetComponent<Spacecraft>();

        if (spacecraft != null)
        {
            spacecraft.TakeDamage(damage);
        }
    }
}
