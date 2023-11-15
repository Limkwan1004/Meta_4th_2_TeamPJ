using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeaderController : MonoBehaviour
{
    // �� ����ּ��� ����

    public Targetsetting Targetset;

    public Transform Target;
    public Transform NextTarget;

    private ObjPosInfo MapData;

    public LeaderState leaderstate;

    private bool isStart = false;

    public Transform CurrentPos;    
    



    // ��.. ��..

    private void Start()
    {
        MapData = FindObjectOfType<ObjPosInfo>();
        leaderstate = GetComponent<LeaderState>();
    }

    private void Update()
    {

        if (!GameManager.instance.isLive) return;

        if (Input.GetKeyDown(KeyCode.F2))
        {
            leaderstate.currentUnitCount++;
            Debug.Log(leaderstate.currentUnitCount);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            leaderstate.currentUnitCount--;
            Debug.Log(leaderstate.currentUnitCount);
        }

        if (isStart)
        {
            #region Base�� ��
            // ���� ��ġ�� ���̽��� ��
            if (Target.gameObject.CompareTag("Base") && isArrive(Target))
            {
                // ���� ��ġ�� ���� ������ ��
                if (Target.gameObject.layer.Equals(gameObject.layer))
                {
                    // ���� ���� ���� 15�� �̻��� ��
                    if (leaderstate.currentUnitCount >= 15)
                    {
                        Targetset = GetComponent<TargetFlag>();
                        NextTarget = Targetset.Target(transform);

                        // ���� �߾� ���� ����� ���� �� �԰����� ��
                        // �׷� �� ���� ������ ��ٸ���..
                        if (NextTarget.Equals(null))  
                        {

                            if (GameManager.instance.currentTime < 900)
                            {
                                if (leaderstate.currentUnitCount >= 18)
                                {
                                    Targetset = GetComponent<TargetEnemyBase>();
                                    NextTarget = Targetset.Target(transform);
                                }
                            }
                            else
                            {
                                if (leaderstate.currentUnitCount >= 23)
                                {
                                    Targetset = GetComponent<TargetEnemyBase>();
                                    NextTarget = Targetset.Target(transform);
                                }

                            }
                            return;
                        }
                        GoGate(transform, ref Target);

                    }
                    else
                    {
                        return;
                    }
                }
                // ���� ��ġ�� ��� ������ ��
                else
                {
                    // �������
                    // ������ ��� �����ϸ� ���̽��� ���� �������� �ٲ� �� if������ �̵�
                    if (!Target.GetComponentInChildren<Flag>().transform.parent.gameObject.layer.Equals(gameObject.layer))
                    {
                        Target = Target.gameObject.GetComponentInChildren<Flag>().transform.parent.transform;
                        Targetset.ToTarget(Target);
                    }  



                }
            }
            #endregion

            #region Flag�� ��
            if (Target.CompareTag("Flag") && isArrive(Target))
            {
                // �� ����� �ƴ� ��
                // �����Ϸ��� ������ �־����..
                if(!Target.gameObject.layer.Equals(gameObject.layer)) 
                {
                    return;
                }
                // �� ����� ��
                else
                {
                    if (leaderstate.currentUnitCount > 13)
                    {
                        GoFlag(transform, ref Target);
                    }
                    else
                    {
                        Targetset = GetComponent<TargetMyBase>();
                        NextTarget = Targetset.Target(transform);
                        GoMyBase(transform, ref Target);
                    }
                }
            }
            #endregion


            #region Gate�� ��
            // ����Ʈ�� ������ ��
            if (Target.gameObject.CompareTag("Gate") && isArrive(Target))
            {
                if (NextTarget.Equals(null))
                {
                    return;

                }
                else
                {
                    Target = NextTarget;
                    Targetset.ToTarget(Target);
                    NextTarget = null;
                }


            }
            #endregion











        }







        // --------------------------------------------------------------------------------------------
        else  // ó�� �� ���� ����
        {
            GoMyBase(transform, ref Target);
            isStart = true;
        }


    }


    private bool isArrive(Transform EndPos)
    {
        return (Vector3.Magnitude(transform.position - EndPos.position) < 5) ? true : false;
    }

    private void GoFlag(Transform StartPos, ref Transform Target)
    {
        Targetset = GetComponent<TargetFlag>();
        Target = Targetset.Target(StartPos);
        Targetset.ToTarget(Target);

    }
    private void GoMyBase(Transform StartPos, ref Transform Target)
    {     
        Targetset = GetComponent<TargetMyBase>();
        Target = Targetset.Target(StartPos);
        Targetset.ToTarget(Target);

    }
    private void GoEnemyBase(Transform StartPos, ref Transform Target)
    {    
        Targetset = GetComponent<TargetEnemyBase>();
        Target = Targetset.Target(StartPos);
        Targetset.ToTarget(Target);

    }
    private void GoGate(Transform StartPos, ref Transform Target)
    {
        Targetset = GetComponent<TargetGate>();
        Target = Targetset.Target(StartPos);
        Targetset.ToTarget(Target);
   
    }
}


