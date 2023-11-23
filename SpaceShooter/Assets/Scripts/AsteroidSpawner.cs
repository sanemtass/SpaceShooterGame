using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public bool gameStarted = false;
    public float minSpawnInterval = .2f;
    public float maxSpawnInterval = .5f;
    public float asteroidLifetime = 1.5f;
    public int maxAsteroidCount = 14;
    public float spawnHeight = 10f; // Yukarıdan gelme yüksekliği

    private List<Vector2> asteroidPositions = new List<Vector2>();

    private void Start()
    {
        StartCoroutine(SpawnAsteroids());
        
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            if (gameStarted)
            {
                int asteroidCount = Random.Range(5, maxAsteroidCount + 1);

                // Asteroidler arasındaki boşluğu düzenli bir desene göre azalt
                float patternWidth = 5f; // Desen genişliği
                float patternHeight = 6f; // Desen yüksekliği

                for (int i = 0; i < asteroidCount; i++)
                {
                    GameObject asteroid = GetRandomAsteroid();

                    // Desen içinde rastgele bir konum belirle
                    float patternX = Random.Range(-patternWidth / 2f, patternWidth / 2f);
                    float patternY = Random.Range(-patternHeight / 2f, patternHeight / 2f);

                    asteroid.transform.position = new Vector2(patternX, spawnHeight + patternY); // Yukarıdan gelme yüksekliği ekleniyor
                    asteroid.SetActive(true);
                }

                yield return new WaitForSeconds(asteroidLifetime);

                ReturnAsteroids(asteroidCount);

                float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void ReturnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroid = GetAsteroidByObjectType(Random.Range(0, 7)); // Rastgele bir asteroid tipi al

            if (asteroid != null && asteroid.activeSelf)
            {
                ObjectPooler.Instance.SetPoolObject(asteroid, asteroid.GetComponent<ObjectTypeScript>().objectType);
            }

            asteroid.SetActive(false);
            asteroidPositions.Remove(asteroid.transform.position);
        }
    }

    private void SetRandomPosition(GameObject obj)
    {
        Vector2 newPosition;
        do
        {
            float x = Random.Range(-2f, 2f);
            float y = Random.Range(12f, 15f);
            newPosition = new Vector2(x, y);
        } while (IsPositionOccupied(newPosition)); // Eğer yeni konum mevcut asteroid konumları ile çakışıyorsa tekrar dene

        obj.transform.position = newPosition;
    }

    private bool IsPositionOccupied(Vector2 position)
    {
        // Listede aynı konumda başka bir asteroid var mı kontrol et
        return asteroidPositions.Contains(position);
    }

    private GameObject GetRandomAsteroid()
    {
        int randomType = Random.Range(0, 7); // 0'dan 6'ya kadar olan objectType değerlerini kullanarak objeleri oluştur
        GameObject asteroid = GetAsteroidByObjectType(randomType);
        if (asteroid.activeSelf)
        {
            ObjectPooler.Instance.SetPoolObject(asteroid, randomType);
        }
        return asteroid;
    }

    private GameObject GetAsteroidByObjectType(int objectType)
    {
        foreach (var pool in ObjectPooler.Instance.pools)
        {
            ObjectTypeScript objectTypeScript = pool.objectPrefab.GetComponent<ObjectTypeScript>();
            if (objectTypeScript != null && objectTypeScript.objectType == objectType)
            {
                return ObjectPooler.Instance.GetPoolObject(objectType);
            }
        }
        return null;
    }
}
