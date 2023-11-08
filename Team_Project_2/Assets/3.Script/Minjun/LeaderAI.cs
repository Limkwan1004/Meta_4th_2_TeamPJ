using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class LeaderAI : Unit
{
    private float scanRange = 10f;
    private LayerMask targetLayer;
    private int invertedLayerMask;
    private NavMeshAgent navMesh;
    private Animator ani;
    private GameObject[] flag;
    [SerializeField] private GameObject targetFlag;

    public bool isEnermyChecked = false;

    //public Transform nearestTarget;
    private float AttackRange = 10f;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        combinedMask = TargetLayers();
        navMesh = GetComponent<NavMeshAgent>();
        flag = GameObject.FindGameObjectsWithTag("Flag");
        targetFlag = TargetFlag();
        bat_State = BattleState.Move;
    }
    private void Update()
    {

        // �׻� �ֺ��� �����ִ��� Ž��
        EnemyDitect();
  
        switch (bat_State)
        {
  
            case BattleState.Attack:
                break;
            case BattleState.Search:
                
                targetFlag = TargetFlag();
                if(targetFlag != null) {
                    bat_State = BattleState.Move;
                }
               
             
                break;
            case BattleState.Move:
                if (targetFlag.transform.position != null)
                {
                        ani.SetBool("Move", true);
                        Debug.Log("����̵�");
                        navMesh.SetDestination(targetFlag.transform.position);
                }
                else
                {
                    bat_State = BattleState.Search;
                }
                break;
            case BattleState.Defense:
                break;
            case BattleState.Detect:
                //ani.SetBool("Move", true);
                //navMesh.SetDestination(NearestTarget.position);
                break;

        }


    }

    private void EnemyDitect()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, scanRange, Vector3.forward, 0, combinedMask);
        NearestTarget = GetNearestTarget(hits);

        if (NearestTarget != null)
        {
            float attackDistance = Vector3.Distance(transform.position, NearestTarget.position);
            bat_State = BattleState.Detect;

            //DItect �����϶� ���и� ��� õõ�� ����
            if (attackDistance <= AttackRange)
            {
                bat_State = BattleState.Attack;
            }

            isEnermyChecked = true;
        }
        else
        {
            if (bat_State == BattleState.Attack)
            {
                if (targetFlag == null)
                {
                    bat_State = BattleState.Search;
                }
                else
                {
                    bat_State = BattleState.Move;
                }


            }
            bat_State = BattleState.Move;
            isEnermyChecked = false;
        }


        //�������� ���ݶ��̴��� ������ Ditect ���·� ����
    }

    private GameObject TargetFlag()
    {
        GameObject[] defaultFlags = flag.Where(_flag => _flag.layer != gameObject.layer).ToArray();
        if (defaultFlags.Length > 0)
        {
            int randomIndex = Random.Range(0, defaultFlags.Length);
            GameObject selected1Flag = defaultFlags[randomIndex];

            return selected1Flag;
            // ���õ� ��ü(selectedFlag)�� ����ϼ���.
        }

        GameObject selectedFlag = null;
        int minTouchingCount = int.MaxValue;
        int layerMask = (1 << LayerMask.NameToLayer("Team")) | (1 << LayerMask.NameToLayer("Enemy1")) | (1 << LayerMask.NameToLayer("Enemy2")) | (1 << LayerMask.NameToLayer("Enemy3"));
        int radius = 10;
        foreach (GameObject _flag in flag)
        {
            // _flag �ֺ����� trigger�� ��� �ִ� ��ü ����
            Collider[] colliders = Physics.OverlapSphere(_flag.transform.position, radius, layerMask, QueryTriggerInteraction.Collide);

            // �ּ� ī��Ʈ ����
            if (colliders.Length < minTouchingCount)
            {
                minTouchingCount = colliders.Length;
                selectedFlag = _flag;
            }
        }

        if (selectedFlag != null)
        {
            return selectedFlag;
        }
        else
        {
            Debug.Log("��߸�ã��");
            return null;

        }
       

    }
}