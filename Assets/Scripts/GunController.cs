using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public GameObject bulletPrefab; // Bullet prefab to shoot
    public Transform firePoint;     // Point where bullets are spawned
    public int bulletPoolSize = 5;  // Pool size for bullets

    [Header("Enemy Tracking")]
    public List<Transform> enemies; // List of enemy transforms to target

    private Queue<GameObject> bulletPool; // Object pool for bullets

    void Start()
    {
        // Initialize object pool
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    void Update()
    {
        Transform target = GetClosestEnemy();
        if (target != null)
        {
            RotateGunTowards(target);
            ShootAtTarget(target);
        }
    }

    // Rotate gun's Z-axis to aim its Y-axis towards the target
    private void RotateGunTowards(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Offset for gun's Y-axis
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    // Shoot bullets towards the target using object pooling
    private void ShootAtTarget(Transform target)
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = firePoint.position;

            BulletController bulletScript = bullet.GetComponent<BulletController>();
            bulletScript.Initialize(target, () => ReturnBulletToPool(bullet));
        }
    }

    // Return bullets to the pool after they hit or miss
    private void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    // Get the closest enemy to the gun
    private Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
