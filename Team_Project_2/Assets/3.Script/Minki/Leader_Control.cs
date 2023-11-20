using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Leader_Control : MonoBehaviour
{
    public enum AIState
    {
        NonTarget,
        Target_Gate,
        Target_Flag,
        Target_Base
    }

    public AIState state;
    private LeaderAI ai;
    private AIPath aiPath;
    public AIDestinationSetter Main_target;
    public Targetsetting targetsetting;
    public Transform Target;
    public Transform NextTarget;

    public bool NearGate = false;
    public bool NearFlag = false;
    public bool NearBase = true;
    public bool isStart = false;

    public bool isOccu = false;


    private float scanRange = 2f;


    private void Start()
    {
        ai = GetComponent<LeaderAI>();
        aiPath = GetComponent<AIPath>();
        Main_target = GetComponent<AIDestinationSetter>();
    }

    private void Update()
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }

        if(Main_target == null && Target != null)
        {
            Debug.Log("???");
            ToTarget(Target);
        }

        Scan_targetPos();

        switch(state)
        {
            case AIState.NonTarget:
                Search_Target();
                break;
            case AIState.Target_Gate:
                break;
        }

        #region �ӽ�
        /*switch(ai.bat_State)
        {
            case LeaderState.BattleState.Wait:
                if(!isStart)
                {
                    return;
                }

               
                if (ai.currentUnitCount < 15)
                {
                    Wait_Soldier();
                }
                Change_State();
                break;
            case LeaderState.BattleState.Move:
                *//*
                    1. �÷��װ� ����� �� 
                      > �� �÷������� �߸� �÷������� ��� �÷������� Ȯ��
                      > �� �÷��׸� ���� ���� ���� Ȯ���ϰ� ������ ������ ������ ����� ��߷� �̵�(����� ����� ���̽���� ����Ʈ)
                      > �߸� �÷��׶�� Wait ���·� ��ȯ
                      > ��� �÷��׶�� Wait ���·� ��ȯ
                    2. ����Ʈ�� ����� ��
                      > ���� ���� ���� ���ٸ� ����� ��߷� �̵�
                *//*
                if(!NearGate && NearFlag && !NearBase)
                {
                    if (ai.currentUnitCount >= 15 && !isStart)
                    {
                        targetsetting = GetComponent<TargetFlag>(); 
                        NextTarget = targetsetting.Target(transform);
                        Move_Gate(transform, ref Target);
                        isStart = true;
                    }
                    else if(ai.currentUnitCount >= 15 && Target.gameObject.layer.Equals(gameObject.layer))
                    {
                        Debug.Log("��������");
                        Move_Flag(transform, ref Target);
                    }
                    else if(ai.currentUnitCount >= 15 && !Target.gameObject.layer.Equals(gameObject.layer))
                    {
                        Debug.Log("������");
                        ai.bat_State = LeaderState.BattleState.Wait;
                    }
                }
                else if(NearGate && !NearFlag && !NearBase)
                {
                    if(ai.currentUnitCount >= 15)
                    {
                        Target = NextTarget;
                        ToTarget(Target);
                    }
                }
                break;
        }*/
        #endregion
    }

    public void Search_Target()
    {
        // ai Ÿ���� ���� ��
        switch(ai.bat_State)
        {
            case LeaderState.BattleState.Attack:
                ai.bat_State = LeaderState.BattleState.Move;
                break;
            case LeaderState.BattleState.Detect:
                ai.bat_State = LeaderState.BattleState.Move;
                break;
            case LeaderState.BattleState.Move:
                /*
                    1. ���� ������ ���� ���� üũ�ؼ� ���� ���ڰ� ������ ����� ��������Ʈ�� �̵��ؼ� ���� ä��
                    2. ���� ���ڰ� 15�� �̻��̶�� ���� ����� ��߷� �̵��ؼ� ���� �غ�
                    3. ��߿� �����ߴٸ� Wait ���·� ��ȯ�ؼ� ������ ���������� ���
                */     
                if(!Check_Soldier())
                {
                    Return_SpawnPoint(transform, ref Target);
                }
                else if(Check_Soldier())
                {
                    if(!NearFlag)
                    {
                        if(NearBase)
                        {

                        }
                        else
                        {
                            Move_Flag(transform, ref Target);
                        }
                    }
                    else if(NearFlag && isStart)
                    {
                        ai.bat_State = LeaderState.BattleState.Wait;
                    }
                }
                break;
            case LeaderState.BattleState.Wait:
                ai.bat_State = LeaderState.BattleState.Move;
                break;
        }
        
    }

    public bool Check_Soldier()
    {
        if(ai.currentUnitCount >= 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Return_SpawnPoint(Transform StartPos, ref Transform Target)
    {
        aiPath.canMove = true;
        aiPath.canSearch = true;
        targetsetting = GetComponent<TargetMyBase>();
        Target = targetsetting.Target(StartPos);
        ToTarget(Target);
    }

    private void Wait_Soldier()
    {
        /*
            ���� 15������ �Ǳ� ������ �ش� �ڸ����� ���
            ai�н��� canmove, cansearch����
        */

        aiPath.canMove = false;
        aiPath.canSearch = false;
    }

    private void Move_Flag(Transform StartPos, ref Transform Target)
    {
        aiPath.canMove = true;
        aiPath.canSearch = true;
        targetsetting = GetComponent<TargetFlag>();
        Target = targetsetting.Target(StartPos);
        ToTarget(Target);
    }

    private void Move_Base()
    {
        aiPath.canMove = true;
        aiPath.canSearch = true;
    }

    private void Move_Gate(Transform StartPos, ref Transform Target)
    {
        aiPath.canMove = true;
        aiPath.canSearch = true;
        targetsetting = GetComponent<TargetGate>();
        Target = targetsetting.Target(StartPos);
        ToTarget(Target);
    }

    private void ToTarget(Transform Target)
    {
        Main_target.target = Target;
    }

    public bool isArrive(Transform EndPos)
    {
        return (Vector3.Magnitude(transform.position - EndPos.position) < 5) ? true : false;
    }

    private void Change_State()
    {
        if(ai.currentUnitCount >= 15)
        {
            ai.bat_State = LeaderState.BattleState.Move;
        }
    }

    private void Scan_targetPos()
    {
        int layerMask = ~LayerMask.GetMask("IgnoreCollision");

        RaycastHit[] allHits = Physics.SphereCastAll(transform.position, scanRange, Vector3.forward, 0, layerMask);

        List<GameObject> validObject = new List<GameObject>();

        NearGate = false;
        NearFlag = false;
        NearBase = false;

        for(int i = 0; i < allHits.Length; i++)
        {
            GameObject hitObject = allHits[i].transform.gameObject;

            if (hitObject.CompareTag("SpawnPoint"))
            {
                // "SpawnPoint" �±��� ��쿡�� ó������ �ʽ��ϴ�.
                continue;
            }

            validObject.Add(hitObject);
        }

        foreach (GameObject hit in validObject)
        {
            if(hit.transform.CompareTag("SpawnPoint"))
            {
                continue;
            }

            if (hit.transform.CompareTag("Gate"))
            {
                NearGate = isArrive(hit.transform);
            }
            else if (hit.transform.CompareTag("Flag"))
            {
                NearFlag = isArrive(hit.transform);
            }
            else if (hit.transform.CompareTag("Base"))
            {
                NearBase = isArrive(hit.transform);
            }

        }
    }
       
}
