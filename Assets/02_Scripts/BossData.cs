using UnityEngine;

[CreateAssetMenu(fileName = "NewBoss", menuName = "Boss Data")]
public class BossData : ScriptableObject
{
    public string bossName;
    public float hp;
    public float speed;
    public Transform bossPrefab;
}
