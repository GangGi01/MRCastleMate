using UnityEngine;

public class ChessSpawner : MonoBehaviour
{
    public GameObject chessPrefab;
    public Transform handTransform;


    private GameObject spawnedChess;
    private bool m_chessBool;


    private void Start()
    {
        m_chessBool = ChessRigidBodyController.instance.chessBool;
    }


    public void SpawnChess()
    {
        if (m_chessBool == false)
        {
            spawnedChess = Instantiate(chessPrefab, handTransform.position, handTransform.rotation);
            spawnedChess.transform.SetParent(handTransform);

        }
        else
        {
            Debug.Log("타워가 이미 소환되어있습니다");
        }
    }

}
