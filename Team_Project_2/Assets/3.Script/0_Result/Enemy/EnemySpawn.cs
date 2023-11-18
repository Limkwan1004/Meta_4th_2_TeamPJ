using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //unitValue에 따라 소환되는 unit
    [SerializeField] private GameObject[] unit;
    [SerializeField] private Ply_Controller player;
    private LeaderState leaderState;
    [SerializeField] private GameObject targetLeader;

    //스폰위치 3개
    public Transform[] SpawnPoint = new Transform[3];
    //스폰위치를 0~2 번위치 차례대로 소환하기위한 인덱스
    private int SpawnIndex = 0;
    //소환되는 간격
    private float Spawninterval = 0.4f;
    private int myLayer;
    private bool isAI;
    private bool isRespawning;
    // 공격 대상 레이어
    private LayerMask TeamLayer;
    private void Awake()
    {
        myLayer = gameObject.layer;
        TeamLayer = LayerMask.NameToLayer("Team");
        myLayer = transform.parent.gameObject.layer;
        targetLeader = null;


        for (int i = 0; i < 3; i++)
        {
            SpawnPoint[i] = transform.GetChild(i); // 각 자식 객체를 배열에 저장
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Ply_Controller>();
    }
    private void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
       
        //스폰포인트 레이어가 깃발의 레이어랑 다르면 깃발레이어로 업데이트.
        if (myLayer != transform.parent.gameObject.layer)
        {
            //깃발레이어로 변경
            gameObject.layer = transform.parent.gameObject.layer;

            //중립깃발이라면 그냥 리턴
            if (gameObject.layer == 0)
            {
                return;
            }
            //팀깃발이라면 타겟은 플레이어
            else if (gameObject.layer == TeamLayer)
            {
                targetLeader = player.gameObject;
            }
            //적깃발이라면 레이어에맞게 타겟 세팅.
            else
            {
                try
                {
                    targetLeader = SetLeader();
                    targetLeader.TryGetComponent(out leaderState);
                }
                catch
                {
                    Debug.Log("타겟찾지못함");
                }
            }



        }
       
       


      
        if (targetLeader != null)
        {

            if (targetLeader.layer == LayerMask.NameToLayer("Die"))
            {
                if (leaderState.data.isDie && !isRespawning)
                {
                    StartCoroutine(RespawnAfterDelay(5f));
                }
            }
            if (targetLeader.gameObject.layer == TeamLayer)
            {
                isAI = false;

            }
            //타겟이 팀이아니라면 소환하는 타겟은 AI이다
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
        Soilder_Controller soilder_Con = newUnit.GetComponent<Soilder_Controller>();
        soilder_Con.infodata = currentUnit;
        soilder_Con.Setunit();
        switch (targetLeader.gameObject.layer)
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

        
        



        leaderState.UnitList.Add(newUnit);
        leaderState.Gold -= currentUnit.cost;
        SpawnIndex++;

        leaderAI.currentUnitCount++;
        //스폰위치를 차례대로 나오게하기위한 메소드 
        if (SpawnIndex > 2)
        {
            SpawnIndex = 0;
        }

    }
    private LeaderState FindLeader()
    {
        GameObject[] objectsWithSameLayer = GameObject.FindGameObjectsWithTag("Leader"); // YourTag에는 LeaderState 컴포넌트가 있는 오브젝트의 태그를 넣습니다.

        // 찾은 오브젝트 중에서 LeaderState 컴포넌트를 가진 첫 번째 오브젝트를 찾습니다.


        foreach (var obj in objectsWithSameLayer)
        {
            if (obj.gameObject.layer == gameObject.layer)
            {
                leaderState = obj.GetComponent<LeaderState>();

                if (leaderState != null)
                {
                    return leaderAI;
                    // LeaderState를 찾으면 루프를 종료합니다.
                }
            }
        }

        if (leaderState == null)
        {
            Debug.LogWarning("LeaderState 컴포넌트를 가진 오브젝트를 찾을 수 없습니다.");
        }
        return null;
    }
    public void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer; // 현재 오브젝트의 레이어 변경

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer); // 하위 오브젝트에 대해 재귀 호출
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
        if (leaderState.data.isDie)
        {
            leaderState.canSpawn = false;

            return;
        }
        //유닛카운트가 맥스가 됐거나 , 유닛비용보다 가진 골드가 적을때 false;
        if (leaderAI.maxUnitCount <= leaderAI.currentUnitCount || leaderAI.Gold <= leaderAI.unitCost)
        {
            leaderState.canSpawn = false;
        }
        else
        {
            leaderState.canSpawn = true;
        }
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        isRespawning = true;

        yield return new WaitForSeconds(delay);

        // 부활 로직을 여기에 구현
        // 예를 들면, 죽었던 유닛을 다시 생성하는 등의 동작을 수행

        // 부활이 완료되면 다시 살아난 것으로 플래그를 변경
        leaderAI.       Respawn(targetLeader);

        // 다음 부활을 위해 플래그를 초기화
        isRespawning = false;
    }
    private void Upgrade_Set(int Team, Soilder_Controller soilder_Controller)
    {
        if(GameManager.instance.leaders[Team].isUpgrade_SolDam)
        {
            soilder_Controller.data.damage = soilder_Controller.infodata.damage + 5;
        }

        if(GameManager.instance.leaders[Team].isUpgrade_SolHP)
        {
            soilder_Controller.data.currentHP = soilder_Controller.infodata.currentHP + 50;
            soilder_Controller.data.maxHP = soilder_Controller.infodata.maxHP + 50;
        }
    }
}