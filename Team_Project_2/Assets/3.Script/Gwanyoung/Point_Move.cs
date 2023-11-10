using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Point_Move : MonoBehaviour
{
    // ����Ʈ�������� �̵�

    public GameObject[] FlagPoint; // �÷�������Ʈ
    private NavMeshAgent navMesh;
    private Vector3 currentPos;
    private int Flag_Num;

    private bool isMove = false;  // �̵� ������

    private void Start()
    {
        FlagPoint = GameObject.FindGameObjectsWithTag("Flag");
        navMesh = GetComponent<NavMeshAgent>();
        
    }
    private void Update()
    {

        if(!GameManager.instance.isLive)
        {
            return;
        }

        if (!isMove)
        {
            Flag_Num = Random.Range(0, FlagPoint.Length);

            if (FlagPoint[Flag_Num].layer != gameObject.layer) 
            {
                isMove = true;
                navMesh.SetDestination(FlagPoint[Flag_Num].transform.position);   // �������� Flag�� ���� �̵�
            }
        }
        else
        {
            currentPos = FlagPoint[Flag_Num].transform.position - transform.position;  // �� �������� ����
            // Flag ��ǥ�� ���� ��ǥ�� 1.5�� ���̳� ��
            if (Mathf.Abs(currentPos.x) <= 1.5f && Mathf.Abs(currentPos.z) <= 1.5f && FlagPoint[Flag_Num].layer == gameObject.layer) 
            {
                isMove = false;               
            }
        }

    }
}
