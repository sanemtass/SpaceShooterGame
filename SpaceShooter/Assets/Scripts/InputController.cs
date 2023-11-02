using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Gemiyi hareket ettirme hızı

    private void Update()
    {
        // Mouse'un pozisyonunu al
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Mouse sol tuşuna tıklandıysa
        if (Input.GetMouseButton(0))
        {
            // Mouse pozisyonu geminin pozisyonuna göre soldaysa sola, sağdaysa sağa hareket et
            if (mousePos.x > transform.position.x)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
        }
    }
}

