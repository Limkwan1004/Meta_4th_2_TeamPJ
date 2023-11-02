using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaderAI : LeaderState
{
    private float scanRange = 10f;
    private LayerMask targetLayer;
    private int invertedLayerMask;
    private NavMeshAgent navMesh;
    public Transform nearestTarget;
    private float AttackRange = 5f;
    private void Awake()
    {
        targetLayer = 1 << gameObject.layer;
        Debug.Log(targetLayer); 
        invertedLayerMask = ~targetLayer.value; // targetLayer�� ������ ������ ���̾���� ����
     
        navMesh = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        // �׻� �ֺ��� �����ִ��� Ž��
        EnemyDitect();



        switch (jud_State)
        {
            case JudgmentState.Ready:
                ani_State = AniState.Idle;
                break;
            case JudgmentState.wait:
                break;
            case JudgmentState.Ditect:
                //�ִϸ��̼� ���е��
                ani_State = AniState.shild;

                //õõ�� ������ ����


                break;
        }

    }

    private void EnemyDitect()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, scanRange, Vector3.forward, 0, invertedLayerMask);
        nearestTarget = GetNearestTarget(hits);

        float attackDistance = Vector3.Distance(transform.position, nearestTarget.position);
        if (attackDistance <= AttackRange)
        {
            bat_State = BattleState.Attack;
        }
       
        //�������� ���ݶ��̴��� ������ Ditect ���·� ����
        if (hits.Length > 0)
        {
            jud_State = JudgmentState.Ditect;
            navMesh.SetDestination(nearestTarget.position);
            //DItect �����϶� ���и� ��� õõ�� ����
            if (attackDistance <= AttackRange)
            {
                bat_State = BattleState.Attack;
            }
        }
        else
        {
            bat_State = BattleState.Attack;
        }
    }
    Transform GetNearestTarget(RaycastHit[] hits)
    {
        Transform nearest = null;
        float closestDistance = float.MaxValue;

        foreach (RaycastHit hit in hits)
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearest = hit.transform;
            }
        }

        return nearest;
    }
}