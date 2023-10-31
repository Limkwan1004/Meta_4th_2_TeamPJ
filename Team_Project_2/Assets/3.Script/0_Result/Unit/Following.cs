using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Following : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> nearestMinion_List = new List<GameObject>();
    public LayerMask TargetLayer;

   // private UnityEngine.AI.NavMeshAgent[] agents;

    private Ply_Controller pc;
    private Ply_Movement pm;

    private Minion_Controller[] minionController;
    GameObject shortobj;
    GameObject FollowObj;
    private void Awake()
    {
        pc = FindObjectOfType<Ply_Controller>();
        pm = FindObjectOfType<Ply_Movement>();
    }

    private void Start()
    {
        StartCoroutine(Mode_Follow_co());
    }

    private void Update()
    {
        
            StartCoroutine(Mode_Follow_co());

        if (pm.isPlayerMove == false)
        {
            StopCoroutine(Mode_Follow_co());
            StartCoroutine(Mode_Stop_Follow_co());
        }
        else
        {

            StartCoroutine(Mode_Follow_co());
            StopCoroutine(Mode_Stop_Follow_co());
        }




    }
    private bool isTarget
    {
        get
        {
            if (!GameManager.instance.isLive)
            {
                return true;
            }
            return false;
        }

    }


  

    // �÷��̾� ������ �� : ���ٷ� �ټ���� �ڷ�ƾ
    public IEnumerator Mode_Follow_co()
    {
        nearestMinion_List.Clear();

        minionController = GetComponentsInChildren<Minion_Controller>();
      
        if (isTarget)
        {
            if(pm.isPlayerMove)
            {
                for (int i = 0; i < pc.Minions_List.Count; i++)
                {
                    pc.Minions_List[i].GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
                }
            }

            for (int i = 0; i < pc.Minions_List.Count; i++)
            {
                if (i == 0)
                {
                    FollowObj = pc.gameObject;
                }

                float ShortDis = Vector3.Distance(FollowObj.transform.position, pc.Minions_List[i].transform.position);
                for (int j = 0; j < pc.Minions_List.Count; j++)
                {
                    float Distance = Vector3.Distance(FollowObj.transform.position, pc.Minions_List[j].transform.position);

                    if (ShortDis >= Distance)
                    {
                        // nearestMinion_List.Add(pc.Minions_List[j]);


                        ShortDis = Distance;
                        shortobj = pc.Minions_List[j];
                    }
                    else
                    {
                        shortobj = pc.Minions_List[i];
                    }
                }

                nearestMinion_List.Add(shortobj);

                if (i == 0)
                {
                    nearestMinion_List[i].GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(pc.transform.position);  
                }
                else
                {

                    nearestMinion_List[i].GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(nearestMinion_List[i - 1].transform.position + Vector3.back);
                }

                FollowObj = nearestMinion_List[i];

            }
        }
        yield return null;
    }

    //�÷��̾� �ح��� �� : 5�� ����..?�� ���� �ڷ�ƾ
    public IEnumerator Mode_Stop_Follow_co()
    {
        minionController = GetComponentsInChildren<Minion_Controller>();
        for (int i = 0; i<nearestMinion_List.Count; i++)
        {
            if(i <= 4)
            {
                if (i % 2 == 0)
                {
                    if (i == 0)
                    {
                        nearestMinion_List[i].GetComponent<NavMeshAgent>().SetDestination(pm.CurrentPos + Vector3.left);
                        if (Vector3.Distance(nearestMinion_List[i].transform.position, pm.CurrentPos) <= 1f)
                        {
                            nearestMinion_List[i].GetComponent<Minion_Controller>().isClose = true;
                            //nearestMinion_List[i].GetComponent<NavMeshAgent>().isStopped = true;
                        }
                    }
                    else
                    {
                        nearestMinion_List[i].GetComponent<NavMeshAgent>().SetDestination(nearestMinion_List[i - 2].transform.position + Vector3.left);
                        if (Vector3.Distance(nearestMinion_List[i].transform.position, nearestMinion_List[i - 2].transform.position) <= 1f)
                        {
                            nearestMinion_List[i].GetComponent<Minion_Controller>().isClose = true;
                        }
                    }

                }
                else
                {
                    nearestMinion_List[i].GetComponent<NavMeshAgent>().SetDestination(pm.CurrentPos + Vector3.right);

                    if (i == 1)
                    {
                        nearestMinion_List[i].GetComponent<NavMeshAgent>().SetDestination(pm.CurrentPos + Vector3.right);
                        if (Vector3.Distance(nearestMinion_List[i].transform.position, pm.CurrentPos) <= 2f)
                        {
                           
                            nearestMinion_List[i].GetComponent<Minion_Controller>().isClose = true;
                        }

                    }
                    else
                    {
                        nearestMinion_List[i].GetComponent<NavMeshAgent>().SetDestination(nearestMinion_List[i - 2].transform.position + Vector3.right);
                        if (Vector3.Distance(nearestMinion_List[i].transform.position, nearestMinion_List[i - 2].transform.position) <= 2f)
                        {
                            nearestMinion_List[i].GetComponent<Minion_Controller>().isClose = true;
                        }
                    }
                }

            }
            else
            {
                //nearestMinion �� index 5��°����

                nearestMinion_List[i].GetComponent<NavMeshAgent>().SetDestination(nearestMinion_List[i - 5].transform.position + Vector3.back);
                if (Vector3.Distance(nearestMinion_List[i].transform.position, nearestMinion_List[i - 5].transform.position) <= 2f)
                {
                    nearestMinion_List[i].GetComponent<Minion_Controller>().isClose = true;
                }   
            }
        }
        yield return null;
    }



    public IEnumerator Mode_Stop_co()
    {
        //���߱� ���
        //���� �ȸ������� :)
        /*
         1.PlayerController���� ����� ����ϴ� ������ Player�� ��ġ�� �����ϴ� ���� ����� �� ��������
        2. �� ��ġ ���� 0���� �÷��̾� ��ġ
        3. nearestMinion ����Ʈ�� ������� 0���� ������ �����
         
         */
        yield return null;
    }

    public void Swap(ref float a ,ref float b)
    {
        float temp = a;
        a = b;
        b = temp;

    }
}
