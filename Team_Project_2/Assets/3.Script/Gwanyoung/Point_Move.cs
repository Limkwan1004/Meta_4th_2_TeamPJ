using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Point_Move : MonoBehaviour
{
    // ����Ʈ�������� �̵�

    public GameObject FlagPoint; // �÷�������Ʈ
    public NavMeshAgent navMesh;
    private Vector3 currentPos;
    private int Flag_Num;

    private bool isMove = false;  // �̵� ������

    private void Start()
    {

        navMesh = GetComponent<NavMeshAgent>();
       


    }
    private void Update()
    {

        navMesh.SetDestination(FlagPoint.transform.position);
    }
}
