using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAndHealthKill : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    
    private void OnTriggerEnter(Collider other)


    {
        if (other.CompareTag("target"))
        {
            gameManager.DeductHealth(10);
        }

        if (other.CompareTag("Bullet"))
        {
            gameManager.IncreaseGold(2);
            gameObject.SetActive(false);
           // other.gameObject.SetActive(false);
            gameObject.GetComponent<EnemyMovement>().ResetPosition();
        }
    }
}
