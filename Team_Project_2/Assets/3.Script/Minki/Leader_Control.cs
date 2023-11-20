using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Leader_Control : MonoBehaviour
{
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

        switch(ai.bat_State)
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
                /*
                    1. �÷��װ� ����� �� 
                      > �� �÷������� �߸� �÷������� ��� �÷������� Ȯ��
                      > �� �÷��׸� ���� ���� ���� Ȯ���ϰ� ������ ������ ������ ����� ��߷� �̵�(����� ����� ���̽���� ����Ʈ)
                      > �߸� �÷��׶�� Wait ���·� ��ȯ
                      > ��� �÷��׶�� Wait ���·� ��ȯ
                    2. ����Ʈ�� ����� ��
                      > ���� ���� ���� ���ٸ� ����� ��߷� �̵�
                */
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
        }
    }

    private void Wait_Flag()
    {
        aiPath.canMove = false;
        aiPath.canSearch = false;

        if(Target.CompareTag("Flag") && 
            NearFlag && 
            Target.gameObject.layer.Equals(gameObject.layer))
        {
            isOccu = true;
        }
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
