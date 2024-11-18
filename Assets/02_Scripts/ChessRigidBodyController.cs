using System;
using UnityEngine;

public class ChessRigidBodyController : MonoBehaviour
{
    public static ChessRigidBodyController instance;

    private Rigidbody towerRigidbody;
    [NonSerialized] public bool chessBool;

    private void Awake()
    {
        instance = this;

        towerRigidbody = GetComponent<Rigidbody>();
        if (towerRigidbody != null)
        {
            // 리지드바디 초기 설정
            towerRigidbody.isKinematic = true; // 물리 작동 중지
            towerRigidbody.useGravity = false; // 중력 비활성화
        }
    }

    // 손으로 집을 때 호출할 메서드
    public void ActivateRigidbody()
    {
        if (towerRigidbody != null)
        {
            towerRigidbody.isKinematic = false; // 물리 작동 활성화
            towerRigidbody.useGravity = true; // 중력 활성화
        }
        chessBool = false;
    }
}
