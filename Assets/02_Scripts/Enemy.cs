using System;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamage
{

    [SerializeField]
    public EnemyData enemyData;
    private float turnSpeed = 20.0f;

    private Transform target;
    private int wavepointIndex = 0;

    private bool isDead = false;

    [NonSerialized] public float hp;
    [NonSerialized] public float speed;


    private void Start()
    {
        hp = enemyData.hp;
        speed = enemyData.speed;

        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

        if (Vector3.Distance(transform.position, target.position) <= 0.01f)
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

    public void OnDamage(float damage)
    {
        if (isDead) return;

        hp -= damage;

        if (hp < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        GameManager.enemiesAlive--;
        Destroy(gameObject);
    }


}
