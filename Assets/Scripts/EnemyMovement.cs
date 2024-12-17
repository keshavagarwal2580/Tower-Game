//using UnityEngine;
//using UnityEngine.AI;

//public class EnemyMovement : MonoBehaviour
//{
//    private NavMeshAgent navMeshAgent;
//    private Vector3 originalPosition;
//    public Transform target;

//    void Start()
//    {
//        navMeshAgent = GetComponent<NavMeshAgent>();
//        originalPosition = transform.position;

//        if (navMeshAgent == null)
//        {
//            Debug.LogError("NavMeshAgent component is missing from this GameObject.");
//        }
//    }

//    public void MoveToTarget()
//    {
//        if (target != null)
//        {
//            navMeshAgent = GetComponent<NavMeshAgent>();
//            navMeshAgent.SetDestination(target.position);
//            print("traget Position"+target.position);
//        }
//    }

//    // This will be called to return the enemy to its original position
//    public void ReturnToOriginalPosition()
//    {
//        navMeshAgent.SetDestination(originalPosition);
//    }
//}


using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 originalPosition;
    public Transform target;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        


        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing from this GameObject.");
        }
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(target.position);
            navMeshAgent.speed = Random.Range(2, 3);
        }
    }

    public void ResetPosition()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-5f, -7f), -0.3f);
        transform.position = originalPosition;
        //navMeshAgent.ResetPath(); // Clear any existing path
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            // Deactivate the enemy on reaching the target
            gameObject.SetActive(false);
            

            // Optionally, you can add logic for any effects or points here
        }
    }
}

