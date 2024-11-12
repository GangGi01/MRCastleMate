using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float hp;
    public float speed;
    public Transform enemyPrefab;
}
