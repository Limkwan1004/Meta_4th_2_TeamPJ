using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Team
{
    neutrality = 0, // �߸�
    Team1,   
    Team2,
    Team3,
    Team4
}

public class GameManager : MonoBehaviour
{
    /*
        ���� �Ŵ������� �����ؾ� �� ���� ���
        1. ���
        2. �÷��̾� ü��
        3. ������ (���� ����)
    */
    public static GameManager instance = null;

    [SerializeField] private GameObject Option;

    [Header("���� �÷���")]
    public float currentTime = 0f;  // ������ �����ϰ� ���� �ð�
    public float EndTime = 1800f;   // ���� �ð��� 30��
    public int Occupied_Area = 1;   // ������ ���� Default�� 1
    public int Color_Index;         // �÷��̾� �÷� �ε���

    [Header("��� ����")]
    public float Gold = 1000;       // ��差
    private float Magnifi = 2f;     // �⺻ ��� ���� (������Ʈ�� ������ 60 x 2f�� �⺻ ȹ�� ��差�� �д� 120)
    
    [Header("�÷��̾� ����")]
    public bool isLive = false;
    public float Current_HP = 150f;
    public float Max_Hp = 150f;
    public bool isDead;
    public float Damage = 20f; 
    public float Regeneration = 0.5f;
    public float respawnTime = 10f;
    //�����α� 
    public int Max_MinionCount = 19;
    public int Current_MinionCount;
    //���� ���׷��̵�
    public bool isPossible_Upgrade_1 = false;
    public bool isPossible_Upgrade_2 = false;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // ���� ��� ��·�
    // ���� ��庥Ƽ��
    // ��� ��·� ���׷��̵�
    
    private void Update()
    {
        if(!isLive)
        {
            return;
        }
        
        currentTime += Time.deltaTime;

        Gold += Time.deltaTime * Magnifi * Occupied_Area; // ������ = �д� 120 * ������ ���� ����
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

    public int T1_Color;
    public int T2_Color;
    public int T3_Color;
}
