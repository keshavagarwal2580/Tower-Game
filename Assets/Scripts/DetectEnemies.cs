//using UnityEngine;

//public class EnemyShootTrigger : MonoBehaviour
//{
//    public GameObject[] bulletPrefab; // Reference to the bullet prefabs
//    public Transform bulletSpawnPoint; // Point where the bullet will be instantiated
//    public float bulletSpeed = 10f; // Speed of the bullet

//    private Transform currentTarget; // Current enemy target
//    private bool canShoot = true; // Shooting cooldown flag
//    public float shootCooldown = 2f; // Time between shots


//    [SerializeField] private Transform[] rotation;

//    [SerializeField] private ImageClickHandler imageClickHandler; // Image handler for bullet selection

//    private void OnTriggerEnter(Collider other)
//    {
//        // Check if the object entering the collider is an enemy
//        if (other.CompareTag("Enemy") && canShoot)
//        {
//            currentTarget = other.transform; // Assign the current enemy as target
//            ShootBullet(imageClickHandler.selectedTowerIndex);
//            StartCoroutine(ShootCooldown());
//        }
//    }

//    private void ShootBullet(int index)
//    {
//        if (currentTarget == null) return;

//        // Instantiate the bullet at the spawn position
//        GameObject bullet = Instantiate(bulletPrefab[index], bulletSpawnPoint.position, rotation[index].rotation);

//        // Pass the target and speed to the bullet script
//        HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();
//        if (homingBullet != null)
//        {
//            homingBullet.SetTarget(currentTarget, bulletSpeed);
//        }
//    }

//    private System.Collections.IEnumerator ShootCooldown()
//    {
//        // Prevent shooting for the cooldown duration
//        canShoot = false;
//        yield return new WaitForSeconds(shootCooldown);
//        canShoot = true;
//    }
//}
