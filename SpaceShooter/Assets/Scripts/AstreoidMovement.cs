using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstreoidMovement : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        // Nesnenin hareketi
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
