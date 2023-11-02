using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Vector3[] spawnPositions; // Taşların oluşturulacağı pozisyonlar
    public float spawnInterval = 2.0f; // Taşların oluşturulma aralığı
    public int objectType = 0; // Object Pooler'daki taşın object type'ı

    private void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                GameObject asteroid = ObjectPooler.Instance.GetPoolObject(objectType);

                if (asteroid != null)
                {
                    asteroid.transform.position = spawnPositions[i];
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
