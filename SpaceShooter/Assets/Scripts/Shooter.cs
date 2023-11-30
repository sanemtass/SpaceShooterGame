using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firePoint; // Ateş etme noktası
    public float bulletSpeed = 10f; // Mermi hızı
    public float fireRate = 3f; // Ateş etme hızı
    private float nextFireTime = 0f; // Bir sonraki ateş zamanı

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // nextFireTime'ı güncelle
        }
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPooler.Instance.GetPoolObject(7);

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = bullet.transform.up * bulletSpeed;

            StartCoroutine(ReturnBulletToPool(bullet));
        }
    }

    private IEnumerator ReturnBulletToPool(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        ObjectPooler.Instance.SetPoolObject(bullet, 7);
    }
}
