using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //unitValue�� ���� ��ȯ�Ǵ� unit
    [SerializeField] private GameObject[] unit;
    [SerializeField] private Ply_Controller player;
    private LeaderState leaderState;
    [SerializeField]private GameObject targetLeader;

    //������ġ 3��
    public Transform[] SpawnPoint = new Transform[3];
    //������ġ�� 0~2 ����ġ ���ʴ�� ��ȯ�ϱ����� �ε���
    private int SpawnIndex = 0;
    //��ȯ�Ǵ� ����
    private float Spawninterval = 0.4f;
    private int myLayer;
    private bool isAI;
    // ���� ��� ���̾�
    private LayerMask TeamLayer;
    private void Awake()
    {
        myLayer = gameObject.layer;
        TeamLayer = LayerMask.NameToLayer("Team");
        myLayer = transform.parent.gameObject.layer;
        targetLeader = null;
        

        for (int i = 0; i < 3; i++)
        {
            SpawnPoint[i] = transform.GetChild(i); // �� �ڽ� ��ü�� �迭�� ����
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Ply_Controller>();
    }
    private void Update()
    {
    
        //��������Ʈ ���̾ ����� ���̾�� �ٸ��� ��߷��̾�� ������Ʈ.
        if (myLayer != transform.parent.gameObject.layer)
        {
            //��߷��̾�� ����
            gameObject.layer  = transform.parent.gameObject.layer;

            //�߸�����̶�� �׳� ����
            if (gameObject.layer == 0)
            {
                return;
            }
            //������̶�� Ÿ���� �÷��̾�
            else if (gameObject.layer == TeamLayer)
            {
                targetLeader = player.gameObject;
            }
            //������̶�� ���̾�°� Ÿ�� ����.
            else
            {
                try
                {
                    targetLeader = SetLeader();
                }
                catch
                {
                    Debug.Log("Ÿ��ã������");
                }
            }



        }

        //�߸��� �ƴҶ�
        if (targetLeader != null)
        {
            //Ÿ���� ���̾ƴ϶�� ��ȯ�ϴ� Ÿ���� AI�̴�
            if (targetLeader.gameObject.layer == TeamLayer)
            {
                isAI = false;

            }
            else
            {
                isAI = true;

            }
        }


        if (isAI)
        {
            AIspawn();

        }






    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAI)
        {
            if (other.CompareTag("Leader") && other.gameObject.layer == gameObject.layer && leaderState.canSpawn)
            {

                InvokeRepeating("UnitSpawn", 0f, Spawninterval);


            }
            else if (!leaderState.canSpawn)
            {

                CancelInvoke("UnitSpawn");
            }
        }
        else
        {

            if (other.CompareTag("Player"))
            {
                GameManager.instance.inRange = true;
                Ply_Controller ply = other.GetComponent<Ply_Controller>();
                ply.spawnPoint = gameObject.GetComponent<EnemySpawn>();
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isAI)
        {
            if (other.CompareTag("Leader") && other.gameObject.layer == gameObject.layer)
            {

                CancelInvoke("UnitSpawn");
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.inRange = false;
            }
        }
    }
    private void UnitSpawn()
    {
        if (leaderState.currentUnitCount > 19)
        {
            return;
        }
        Unit_Information currentUnit = GameManager.instance.units[leaderState.unitValue];
        GameObject newUnit = Instantiate(currentUnit.unitObject, SpawnPoint[SpawnIndex].position, Quaternion.identity);
        SetLayerRecursively(newUnit, leaderState.gameObject.layer);
        switch(targetLeader.gameObject.layer)
        {
            case 7:
                ColorManager.instance.RecursiveSearchAndSetUnit(newUnit.transform, GameManager.instance.T1_Color);
                break;
            case 8:
                ColorManager.instance.RecursiveSearchAndSetUnit(newUnit.transform, GameManager.instance.T2_Color);
                break;
            case 9:
                ColorManager.instance.RecursiveSearchAndSetUnit(newUnit.transform, GameManager.instance.T3_Color);
                break;

        }

        UnitAttack2 unitAttack2 = newUnit.GetComponent<UnitAttack2>();
        unitAttack2.data = currentUnit;
        unitAttack2.Setunit();
        leaderState.UnitList.Add(newUnit);
        leaderState.Gold -= currentUnit.cost;
        SpawnIndex++;

        leaderState.currentUnitCount++;
        //������ġ�� ���ʴ�� �������ϱ����� �޼ҵ� 
        if (SpawnIndex > 2)
        {
            SpawnIndex = 0;
        }

    }
    private LeaderState FindLeader()
    {
        GameObject[] objectsWithSameLayer = GameObject.FindGameObjectsWithTag("Leader"); // YourTag���� LeaderState ������Ʈ�� �ִ� ������Ʈ�� �±׸� �ֽ��ϴ�.

        // ã�� ������Ʈ �߿��� LeaderState ������Ʈ�� ���� ù ��° ������Ʈ�� ã���ϴ�.


        foreach (var obj in objectsWithSameLayer)
        {
            if (obj.gameObject.layer == gameObject.layer)
            {
                leaderState = obj.GetComponent<LeaderState>();

                if (leaderState != null)
                {
                    return leaderState;
                    // LeaderState�� ã���� ������ �����մϴ�.
                }
            }
        }

        if (leaderState == null)
        {
            Debug.LogWarning("LeaderState ������Ʈ�� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }
        return null;
    }
    public void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer; // ���� ������Ʈ�� ���̾� ����

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer); // ���� ������Ʈ�� ���� ��� ȣ��
        }


    }
    private GameObject SetLeader()
    {
     
        if (myLayer != TeamLayer)
        {

            leaderState = FindLeader();
            if (leaderState != null)
            {
                targetLeader = leaderState.gameObject;

            }
        }
        else 
        {

            targetLeader = player.gameObject;


        }
        return targetLeader;
    }
    private void AIspawn()
    {
        if (leaderState.isDead)
        {
            leaderState.canSpawn = false;

            return;
        }
        //����ī��Ʈ�� �ƽ��� �ưų� , ���ֺ�뺸�� ���� ��尡 ������ false;
        if (leaderState.maxUnitCount <= leaderState.currentUnitCount || leaderState.Gold <= leaderState.unitCost)
        {
            leaderState.canSpawn = false;
        }
        else
        {
            leaderState.canSpawn = true;
        }
    }
}