using System.Collections;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float range = 2.0f;
    public float damage = 2.0f;
    public float turnSpeed = 200.0f;
    public float attackCooldown = 1.0f;
    public string enemyTage = "ENEMY";
    public string bossTage = "BOSS";
    public Transform partToRotate;
    public float attackMoveDistance = 0.5f; // 적 방향으로 이동하는 거리
    public float moveDuration = 0.2f; // 이동 및 복귀 시간

    private Transform target;
    private Enemy currentTarget;
    private Boss currentBoss;
    private float lastAttackTime;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position; // 초기 위치 저장
        StartCoroutine(UpdateTarget());
    }

    IEnumerator UpdateTarget()
    {
        while (true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTage);
            GameObject[] bosses = GameObject.FindGameObjectsWithTag(bossTage);

            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            GameObject nearestBoss = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy <= range && distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
            foreach (GameObject boss in bosses)
            {
                float distanceToBoss = Vector3.Distance(transform.position, boss.transform.position);
                if (distanceToBoss <= range && distanceToBoss < shortestDistance)
                {
                    shortestDistance = distanceToBoss;
                    nearestBoss = boss;
                }
            }
            if (nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.transform.position) <= range)
            {
                target = nearestEnemy.transform; // 적을 타겟으로 설정
                currentTarget = nearestEnemy.GetComponent<Enemy>();
                currentBoss = null; // 보스는 null로 설정
            }
            else if (nearestBoss != null && Vector3.Distance(transform.position, nearestBoss.transform.position) <= range)
            {
                target = nearestBoss.transform; // 보스를 타겟으로 설정
                currentBoss = nearestBoss.GetComponent<Boss>();
                currentTarget = null; // 적은 null로 설정
            }
            else
            {
                target = null; // 모두 null로 설정
                currentTarget = null;
                currentBoss = null;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - partToRotate.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed);

        if (currentBoss != null)
        {
            Attack(currentBoss);
        }
        else if (currentTarget != null)
        {
            Attack(currentTarget);
        }
    }

    private void Attack(Enemy target)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            StartCoroutine(PerformAttack(target.transform.position));
            target.OnDamage(damage); // 적에게 피해 주기
            lastAttackTime = Time.time;
        }
    }


    private void Attack(Boss target)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            StartCoroutine(PerformAttack(target.transform.position));
            target.OnDamage(damage); // 보스에게 피해 주기
            lastAttackTime = Time.time;
        }
    }

    private IEnumerator PerformAttack(Vector3 targetPosition)
    {
        Vector3 attackPosition = transform.position + (targetPosition - transform.position).normalized * moveDuration;

        // 이동
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, attackPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 복귀
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(attackPosition, originalPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition; // 위치 보정
    }

    private void OnDrawGizmosSelected() //기즈모를 그려주는 유니티 함수
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range); //내 위치를 기준으로 range를 반지름을 구를 그려줌
    }

}
