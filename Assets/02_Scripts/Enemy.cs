using UnityEngine;


public class Enemy : MonoBehaviour
{

    [SerializeField]
    public EnemyData enemyData;
    private float turnSpeed = 20.0f;

    private Transform target;
    private int wavepointIndex = 0;

    public Transform enemy;
    public float speed = 2.0f;


    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            GameManager.enemiesAlive--;
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    private void Start()
    {

    }
}
