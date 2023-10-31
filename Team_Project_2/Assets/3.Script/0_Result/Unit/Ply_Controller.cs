using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ply_Controller : MonoBehaviour
{
    /*
        1. ����Ű �Է����� ���� ��ȯ
        2. ����Ű �Է����� ����� ���� ��ġ
    */
    
    public enum Mode
    {
        Follow = 1,      //���� ������~
        Attack,    //�����϶�~
        Assemble    //���ڸ��� ���߱� & ���� �ٰ����� �����
    }

    public Mode CurrentMode;

    //�ٸ� Ŭ���� ��ü=====================
    private Minion_Controller[] minionController;

    //�÷��̾� ����========================
    public int Max_MinionCount = 25;
    public int Current_MinionCount;

    //�̴Ͼ� ������=========================
    [Header("�ε��� - 0 : ���� / 1 : ��� / 2 : �ü�")]
    [SerializeField] private GameObject[] Minion_Prefabs;

    public List<GameObject> Minions_List = new List<GameObject>();
    //public LinkedList<GameObject> Minions_List = new LinkedList<GameObject>();

    public int Human_num;

    [SerializeField]
    private Transform Spawner;

    //���� & �ü� ���� �� �ִ°� �Ǵ��ϴ� ���� -> ���Ŀ� ���׷��̵� ��ɰ� �Ҷ� ������ּ���
    private bool isPossible_HeavyInfantry = false;
    private bool isPossible_Archer = false;


    public bool isDead { get; private set; } = false;

    public LayerMask TargetLayer;

    private UnityEngine.AI.NavMeshAgent[] agents;

    private List<GameObject> nearestMinion_List = new List<GameObject>();


    private void Awake()
    {
        CurrentMode = Mode.Follow;
    }

    private void Update()
    {
        Spawn_Solider();
        //���� ����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("���� ������~~");
            StartCoroutine(Mode_Follow_co());
            CurrentMode = Mode.Follow;
        }
            
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("�����϶�~~");
            CurrentMode = Mode.Attack;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("�𿩶�~~");
            CurrentMode = Mode.Assemble;
        }
    }

    private void Spawn_Solider()
    {
        if(Current_MinionCount < Max_MinionCount)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                int selectedNumber = -1; // �⺻�� ����
                

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    selectedNumber = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    selectedNumber = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    selectedNumber = 3;
                }

                if(selectedNumber != -1)
                {
                    switch (selectedNumber)
                    {
                        case 1:
                            Debug.Log("1����");
                            Human_num = 0;
                            if(GameManager.instance.Gold > 15)
                            {
                                Init_Solider(Human_num);
                            }
                            else
                            {
                                // ���߿� ��尡 �����մϴ� ����� UI ���� �ؾ���
                                Debug.Log("1�� ���� ��尡 �����մϴ�.");
                            }
                            break;
                        case 2:
                            Debug.Log("2����");
                            Human_num = 1;
                            // ���߿� if���� �ص����Ʈ�� isPossible_HeavyInfantry ���� ���� Ȯ��
                            if (GameManager.instance.Gold > 20)
                            {
                                Init_Solider(Human_num);
                            }
                            else
                            {
                                // ���߿� ��尡 �����մϴ� ����� UI ���� �ؾ���
                                Debug.Log("2�� ���� ��尡 �����մϴ�.");
                            }
                            break;
                        case 3:
                            Debug.Log("3����");
                            Human_num = 2;
                            // ���߿� if���� �ص����Ʈ�� isPossible_Archer ���� ���� Ȯ��
                            if (GameManager.instance.Gold > 25)
                            {
                                Init_Solider(Human_num);
                            }
                            else
                            {
                                // ���߿� ��尡 �����մϴ� ����� UI ���� �ؾ���
                                Debug.Log("3�� ���� ��尡 �����մϴ�.");
                            }
                            break;
                    }
                }
            }
        }
    }

    private void Init_Solider(int Human_num)
    {
        // ���߿� ��Ʈ�� ������ �÷��� ��ũ��Ʈ���� �÷���ȣ �Ѱ� �����ŷ� ��ȯ�� �� �÷� ���� ���Ѿ���

        GameObject Minion = null;
        Minion = Instantiate(Minion_Prefabs[Human_num], Spawner.position, Quaternion.identity);
        //�̴Ͼ� ���� ��ġ�� ���߿� ������(Spawner)��ġ�� �ٲٱ� 

        Minion_Controller minionController = Minion.AddComponent<Minion_Controller>();

        GameManager.instance.Gold -= (15 + (Human_num * 5));
        Minion.transform.SetParent(transform);
        Minions_List.Add(Minion);
        Current_MinionCount++;
    }





    private bool isTarget
    {
        get
        {
            if (!isDead)
            {
                return true;
            }
            return false;
        }

    }



    public IEnumerator Mode_Follow_co()
    {

        minionController = GetComponentsInChildren<Minion_Controller>();
        agents = GetComponentsInChildren<UnityEngine.AI.NavMeshAgent>();


        if (isTarget)
        {
            for (int i = 0; i < Minions_List.Count; i++)
            {
                agents[i].isStopped = false;
            }
            //agent.isStopped = false;
            //������..�� ���ҳ���� �߰��� ���� �۵��ϵ��� ����..

            

            GameObject FollowObj = this.gameObject;

            for (int i = 0; i < Minions_List.Count; i++)
            {

                float ShortDis = Vector3.Distance(FollowObj.transform.position, Minions_List[0].transform.position);
                for (int j = 0; j < Minions_List.Count; j++)
                {
                    float Distance = Vector3.Distance(FollowObj.transform.position, Minions_List[j].transform.position);
                    if (ShortDis >= Distance)
                    {
                        nearestMinion_List.Add(Minions_List[j]);



                        //FollowObj = nearestMinion_List[j];
                        //ShortDis = Distance;
                    }

                    if (j == 0)
                    {
                        agents[j].SetDestination(this.transform.position + Vector3.back);
                    }
                    else
                    {
                        agents[j].SetDestination(nearestMinion_List[j - 1].transform.position + Vector3.back);
                    }
                }

            }
        }
        //else
        //{

        //    for (int i = 0; i < Minions_List.Count; i++)
        //    {
        //        agents[i].isStopped = true;
        //    }
        //    Collider[] cols = Physics.OverlapSphere(transform.position, 20f, TargetLayer);
        //    for (int i = 0; i < cols.Length; i++)
        //    {
        //        if (cols[i].TryGetComponent(out Ply_Controller p))
        //        {
        //            if (!p.isDead)
        //            {
        //                p = this.GetComponent<Ply_Controller>();
        //                break;
        //            }
        //        }

        //    }
        //}


        yield return null;


    }












}
