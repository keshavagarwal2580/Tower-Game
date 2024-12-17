using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;

    private Transform target;
    private Action onHitCallback;

    public void Initialize(Transform targetEnemy, Action callback)
    {
        target = targetEnemy;
        onHitCallback = callback;
    }

    void Update()
    {
        if (target == null)
        {
            onHitCallback?.Invoke();
            return;
        }

        // Move bullet towards the target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if bullet reached close enough to the target
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            OnHit();
        }
    }

    private void OnHit()
    {
        // Perform any hit effects (like damage)
        Debug.Log("Bullet hit the enemy!");

        // Return bullet to the pool
        onHitCallback?.Invoke();
    }
}
