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
        Follow = 1,      //���� ������~ (�÷��̾ ���߸� �뿭���߱�)
        Attack,    //�����϶�~
        Stop    //�����~~
    }

    public Mode CurrentMode;

    //�ٸ� Ŭ���� ��ü=====================
    private Minion_Controller[] minionController;

    private Following following;

    //�÷��̾� ����========================
    public int Max_MinionCount = 19;
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


    public LayerMask TargetLayer;

    private UnityEngine.AI.NavMeshAgent[] agents;

    private List<GameObject> nearestMinion_List = new List<GameObject>();


    //�����߰� �̼���

    public bool isOperateStop = false;

    public bool isOperateFollow = true;


    public bool isDead { get; private set; }



    //�߰� �̼���
    [SerializeField]
    private Animator animator;

    public bool isPlay_AttackOrder = false;
    public bool isPlay_FollowOrder = false;
    public bool isPlay_StopOrder = false;

    private void Awake()
    {
        following = GetComponent<Following>();
        CurrentMode = Mode.Follow;

    }

    private void Update()
    {
        Spawn_Solider();
        //���� ����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("���� ������~~");

            if (!isPlay_FollowOrder)
            {
                animator.SetTrigger("FollowOrder");
                isPlay_FollowOrder = true;
            }
            CurrentMode = Mode.Follow;
            isOperateFollow = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("�����϶�~~");
            if (!isPlay_AttackOrder)
            {
                animator.SetTrigger("AttackOrder");
                isPlay_AttackOrder = true;
            }
            CurrentMode = Mode.Attack;
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("�����~~");

            if (!isPlay_StopOrder)
            {
                animator.SetTrigger("StopOrder");
                isPlay_StopOrder = true;
            }


            CurrentMode = Mode.Stop;
            isOperateStop = true;

            isOperateFollow = false;



        }





    }

    private void Spawn_Solider()
    {
        if (Current_MinionCount < Max_MinionCount)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
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

                if (selectedNumber != -1)
                {

                    switch (selectedNumber)
                    {
                        case 1:
                            Debug.Log("1����");
                            Human_num = 0;
                            if (GameManager.instance.Gold > 15)
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


                    StartCoroutine(following.Mode_Follow_co());
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

        Minion_Controller minionController = Minion.GetComponent<Minion_Controller>();
        Minion.layer = 7;

        GameManager.instance.Gold -= (15 + (Human_num * 5));
        //Minion.transform.SetParent(transform);
        Minions_List.Add(Minion);
        Current_MinionCount++;
    }
}
