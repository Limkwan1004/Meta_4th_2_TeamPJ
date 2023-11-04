using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaderAI : Unit
{
    private float scanRange = 10f;
    private LayerMask targetLayer;
    private int invertedLayerMask;
    private NavMeshAgent navMesh;
    private Animator ani;

    //public Transform nearestTarget;
    private float AttackRange = 5f;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        combinedMask = TargetLayers();
        navMesh = GetComponent<NavMeshAgent>();
        bat_State = BattleState.Follow;
    }
    private void Update()
    {
        // �׻� �ֺ��� �����ִ��� Ž��
        EnemyDitect();
        switch (bat_State)
        {
            case BattleState.Follow:
                Debug.Log("AI Follow����");
                //navMesh.isStopped = true;
                break;
            case BattleState.Attack:
                Debug.Log("AI Attack����");

                break;
            case BattleState.Detect:
                Debug.Log("AI �������� ������");
                //�ִϸ��̼� ���е��
                ani_State = AniState.shild;
                //õõ�� ������ ����
                //Debug.Log()
                ani.SetBool("Move", true);
                navMesh.SetDestination(NearestTarget.position);
                break;
        }

        switch (jud_State)
        {
            case JudgmentState.Ready:
                ani_State = AniState.Idle;
                break;
            case JudgmentState.wait:
                break;


                //float originalSpeed = navMeshAgent.speed; // ���� �ӵ� ����
                //navMeshAgent.speed = originalSpeed / 4; // 1/4�� ���� �ӵ� ����



        }



    }

    private void EnemyDitect()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, scanRange, Vector3.forward, 0, combinedMask);
        NearestTarget = GetNearestTarget(hits);
        if(NearestTarget != null) { 
        float attackDistance = Vector3.Distance(transform.position, NearestTarget.position);
            bat_State = BattleState.Detect;
           
            //DItect �����϶� ���и� ��� õõ�� ����
            if (attackDistance <= AttackRange)
            {
                bat_State = BattleState.Attack;
            }
        }
        else
        {
            jud_State = JudgmentState.Ready;
            bat_State = BattleState.Follow;
        }

       
        //�������� ���ݶ��̴��� ������ Ditect ���·� ����
    }
  

}   