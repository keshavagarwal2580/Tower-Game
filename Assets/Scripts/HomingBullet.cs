using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private Transform target; // Reference to the enemy
    private float speed = 10f; // Bullet speed

    public void SetTarget(Transform enemyTarget, float bulletSpeed)
    {
        target = enemyTarget;
        speed = bulletSpeed;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy the bullet if the target no longer exists
            return;
        }

        // Calculate direction to the target and move towards it
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Optionally rotate the bullet to face the target
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Logic for hitting the enemy
            Destroy(gameObject); // Destroy the bullet on impact
        }
    }
}
