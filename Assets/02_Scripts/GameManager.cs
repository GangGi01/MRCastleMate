using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public BossData[] bossData;
    public EnemyData[] enemyData;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;

    public static GameManager instance;

    private int waveIndex = 1;
    public static int enemiesAlive = 0;
    public static int bossAlive = 0;
    private bool isWaving = false;

    private int baseEnemies = 10;
    private int groupSize = 10;

    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {

    }



    public void OnButtonPressd(SelectEnterEventArgs args)
    {

        if (!isWaving)
        {
            StartWave();
        }
    }
    private void Update()
    {

    }







    public void StartWave()
    {
        if (enemiesAlive > 0.0f || bossAlive > 0.0f)
        {
            Debug.Log("웨이브에 적이 남아있습니다.");
            return;
        }

        isWaving = true;

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        int groupIndex = (waveIndex - 1) / groupSize;

        //Mathf.Max(a, b) a와 b중 더 큰 수를 반환
        //Mathf.min(a, b) a와 b중 더 작은 수를 반환
        int enemyTypeIndex = Mathf.Min(groupIndex, enemyData.Length - 1);



        // 10의 배수 라운드에서는 보스 몬스터 소환
        if (waveIndex % 10 == 0)
        {
            int bossTypeIndex = Mathf.Min(groupIndex, bossData.Length - 1);
            SpawnBoss(bossData[bossTypeIndex]);
        }
        else
        {
            int enemiesToSpwan = baseEnemies + (waveIndex - 1) % groupSize;
            // 일반 적 스폰
            for (int i = 0; i < enemiesToSpwan; i++)
            {
                SpawnEnemy(enemyData[enemyTypeIndex]);
                yield return new WaitForSeconds(0.5f);

            }
        }



        waveIndex++;
        isWaving = false;

    }


    private void SpawnEnemy(EnemyData enemy)
    {
        Instantiate(enemy.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemiesAlive++;
    }

    private void SpawnBoss(BossData boss)
    {
        Instantiate(boss.bossPrefab, spawnPoint.position, spawnPoint.rotation);
        bossAlive++;
    }

}
