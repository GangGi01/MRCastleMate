using System;
using UnityEngine;

public class Boss : MonoBehaviour, IDamage
{
    [SerializeField]
    public BossData bossData;
    private float turnSpeed = 20.0f;

    private Transform target;
    private int wavepointIndex = 0;

    public Transform enemy;
    private bool isDead = false;

    [NonSerialized] public float hp;
    [NonSerialized] public float speed;

    private void Start()
    {
        hp = bossData.hp;
        speed = bossData.speed;

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
