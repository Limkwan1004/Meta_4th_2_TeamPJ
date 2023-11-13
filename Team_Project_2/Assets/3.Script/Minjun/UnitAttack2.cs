using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class UnitAttack2 : MonoBehaviour
{
    //[SerializeField] private Ply_Controller player;
    /*
     �̴Ͼ��� �߽������� ���̾ �����ϴ� �� ����
    ���̾��� ���尡������� Ÿ������ ������
    Ÿ�ٰ����� �̴Ͼ��� ���� �ٶ󺸰� (Lookat�޼ҵ�) ������ �̵�  -> ����� Lerp�� �̵� ���� �׺���̼����� ����
    �̵��� �̴Ͼ��� ���ݹ��� �ݶ��̴�(�̴Ͼ�տ� ���� �ڽ��ݶ��̴� (���񼭹��̹�ó��))�� ������ ������ ����

     
     
     
     */
    //�ӽ� �̴Ͼ�ü��
    private int HP = 3;
    private bool isDie;
   private Ply_Controller player;
    //���� ������ ������
    protected LeaderState leaderState;
    protected GameObject leader;

    public GameObject GetLeader()
    {
        return leader;
    }
    // ���� ���ݰ�������
    [SerializeField] private float scanRange = 13f;
    [SerializeField] private float AttackRange = 1.5f;

    //�̵��� ���������� ���ݹ����ݶ��̴��� ��Ҵ°�?
    [SerializeField] private bool isdetecting = false;
    //�������ΰ�?
    protected bool isAttacking = false;
    private bool isHitting = false;
    private bool isSuccessAtk = true;
    protected Animator ani;
    protected Coroutine attackCoroutine;
    private int myLayer;
    private int combinedMask;
    // ���� ��� ���̾�
    private LayerMask TeamLayer;
    private LayerMask EnemyLayer;
    //�׾����� �ڽ��ݶ��̴� Enable�ϱ����� �������� 
    [SerializeField] private BoxCollider HitBox_col;
    [SerializeField] private BoxCollider Ob_Weapon_col;
    
    //����, ��Ʈ ������
    private WaitForSeconds attackDelay;
    private WaitForSeconds hitDelay = new WaitForSeconds(0.2f);
    //�׺���̼�
    private NavMeshAgent navMeshAgent;
    public bool isClose;
    [Header("����Ÿ�� Transform")]
    [SerializeField] public Transform nearestTarget;
    [Header("����Ÿ�� Layer")]
    [SerializeField] LayerMask target;



    private Following following;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Ply_Controller>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        
    }

    private void Start()
    {
        //�ڽ��� ���̾ ������ �������̾ ���� �迭 ����ϴ� �޼ҵ�

        myLayer = gameObject.layer;
        TeamLayer = LayerMask.NameToLayer("Team");
        combinedMask = TargetLayers();




        //
        if (myLayer != TeamLayer)
        {

            leaderState = FindLeader();
            if (leaderState != null)
            {
                leader = leaderState.gameObject;

            }
        }
        else
        {
            leader = player.gameObject;
        
        }
       

    }

    private void Update()
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }

        if (!isDie && leader != player.gameObject)
        {
            if (!leaderState.isDead)
            {
                switch (leaderState.bat_State)
                {
                    case LeaderState.BattleState.Attack:
                      
                        AttackOrder();
                        
                        break;
                    default:
                        FollowOrder();
                        break;
                }
            }
            else
            {

                AttackOrder();


            }
        }
      
        if(!isDie && !GameManager.instance.isDead && leader == player.gameObject)
        {
            switch (player.CurrentMode)
            {
                case Ply_Controller.Mode.Follow:
                    
                    if (player.CurrentMode == Ply_Controller.Mode.Follow)
                    {
                        if (isClose == true)
                        {
                            ani.SetBool("Move", false);
                        }
                        else
                        {
                            ani.SetBool("Move", true);
                        }
                    }
                  
                    break;
                case Ply_Controller.Mode.Attack:
                    AttackOrder();
                    break;
                case Ply_Controller.Mode.Stop:
                    break;
            }
        }
        else
        {

            AttackOrder();
        }
        // �̴Ͼ���Ʈ�ѷ��� �ű��ʿ伺����.
        if (HP <= 0)
        {
            //�������� ,�̵����� 
            if (!isDie)
            {
                Die();
            }
            isDie = true;
        }
    }
        //���̾� ������ ����� Ÿ�� �����ϴ¸޼ҵ�
        Transform GetNearestTarget(RaycastHit[] hits)
        {
            Transform nearest = null;
            float closestDistance = float.MaxValue;

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.CompareTag("SpawnPoint"))
                {
                    continue;
                }
                float distance = Vector3.Distance(transform.position, hit.transform.position);


                if (distance < closestDistance && !hit.transform.CompareTag("SpawnPoint"))
                {
                    closestDistance = distance;
                    nearest = hit.transform;
                }
            }

            return nearest;
        }
        //�������������� �����ٶ󺸴� �޼ҵ�
        private void LookatTarget(Transform target)
        {

            Vector3 AttackDir = target.position - transform.position;
            AttackDir.y = 0; // Y �� �̵��� �����Ͽ� ������� ����
            Quaternion rotation = Quaternion.LookRotation(AttackDir);
            transform.rotation = rotation;
        }
        //�������������� �����ϱ����� ������ �̵��ϴ¸޼ҵ�

        //�������� �׸��¸޼ҵ�
        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, scanRange);

        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireSphere(transform.position, AttackRange);
        //}


        //���� �ݶ��̴��� ������Ʈ�� ���̾ �ٸ���
        //�°��ִ����� �ƴϸ�
        //���� �ݶ��̴��� ���϶�
        // ��, ���� ������ ������ ���̾ �ڽŰ� �ٸ��� ��Ʈ
        private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Weapon") && (other.gameObject.layer != gameObject.layer) && !isHitting)
        {
            StartCoroutine(Hit_co());
        }

    }
    //�����ڷ�ƾ�޼ҵ�
    protected IEnumerator Attack_co()
    {
        //������Ÿ��
        float d = Random.Range(2f, 2.1f);
        attackDelay = new WaitForSeconds(d);

        //���� ���������� ����
        isAttacking = true;

        isSuccessAtk = false;
        ani.SetTrigger("Attack");
        yield return attackDelay;


        isAttacking = false;
    }
    //��Ʈ �ڷ�ƾ�޼ҵ�
    private IEnumerator Hit_co()
    {
        isHitting = true;
        //��Ʈ�� ������ޱ�
        HP -= 1;


        //���ݵ��� ĵ���� ������Ÿ�� �ʱ�ȭ
        if (!isSuccessAtk)
        {


            StopCoroutine(attackCoroutine);
            isAttacking = false;
        }
        ani.SetTrigger("Hit");
        yield return hitDelay;
        isHitting = false;


    }

    //�̺�Ʈ���� ���� ����Ű�� �޼ҵ�
    public void WeaponActive()
    {
        isSuccessAtk = true;
        Ob_Weapon_col.enabled = true;
        Invoke("WeaponFalse", 0.1f);

    }
    private void WeaponFalse()
    {
        Ob_Weapon_col.enabled = false;
    }
    //������ �޼ҵ�
    public void Die()
    {
        ani.SetTrigger("Dead");  // �״¸�����
        if (gameObject.layer == TeamLayer)
        {
            player.UnitList_List.Remove(gameObject);
            following.Stop_List.Remove(gameObject);
        }
        else
        {
            leaderState.UnitList.Remove(gameObject);
            leaderState.currentUnitCount--;
        }
        gameObject.layer = 9;   // ���̾� DIe�� �����ؼ� Ÿ������ �ȵǰ�
        HitBox_col.enabled = false;    //�ε������ʰ� �ݶ��̴� false
        //StopCoroutine(attackCoroutine);   //���ݵ����̶�� ���ݵ� ����

        Destroy(gameObject, 3f);  // �װ��� 3���� ��Ʈ����








    }

    //�ڽ��� ���̾�� ���� ������ ã�� �޼ҵ�
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
    //�ڽ��� ���̾���� ������ ���̾���� ���н����ִ� �޼ҵ�
    //��> �ڽ��� Enemy1 �̶�� Team,Enemy2, Enemy3 �� ������ ����
    private int TargetLayers()
    {
        int[] combinedLayerMask;
        int myLayer = gameObject.layer;
        //�� 4������ ���̾� 
        int[] layerArray = new int[] { LayerMask.NameToLayer("Team"), LayerMask.NameToLayer("Enemy1"), LayerMask.NameToLayer("Enemy2"), LayerMask.NameToLayer("Enemy3") };
        //�츮���� ���̾ ������ ������ ���̾ ���� �迭
        combinedLayerMask = new int[3];
        int combinedIndex = 0;


        for (int i = 0; i < layerArray.Length; i++)
        {
            if (myLayer != layerArray[i])
            {
                combinedLayerMask[combinedIndex] = layerArray[i];
                combinedIndex++;
            }

        }
        int layerMask0 = 1 << combinedLayerMask[0];
        int layerMask1 = 1 << combinedLayerMask[1];
        int layerMask2 = 1 << combinedLayerMask[2];
        combinedMask = layerMask0 | layerMask1 | layerMask2;
        return combinedMask;
    }
    //�ڽ��� ������ �������������� ��������ϱ����Ѹ޼ҵ�
    private void AttackOrder()
    {
        RaycastHit[] allHits = Physics.SphereCastAll(transform.position, scanRange, Vector3.forward, 0, combinedMask);
        nearestTarget = GetNearestTarget(allHits);

        if (nearestTarget != null) //Ž���� ���� ������
        {

            LookatTarget(nearestTarget);
            float attackDistance = Vector3.Distance(transform.position, nearestTarget.position);
            if (attackDistance <= AttackRange)
            {
                isdetecting = true;
            }
            else
            {
                isdetecting = false;
            }


            if (!isdetecting) //Ž�������� �ָ������� ������ �̵�
            {
                navMeshAgent.isStopped = false;
                ani.SetBool("Move", true);
                navMeshAgent.SetDestination(nearestTarget.transform.position);



            }
            else // Ž���� ���� �����ϸ� �̵��� ���߰� ����
            {

                ani.SetBool("Move", false);
                navMeshAgent.isStopped = true;
                if (!isAttacking)
                {
                    attackCoroutine = StartCoroutine(Attack_co());
                    //StartCoroutine(Attack_co());
                }
            }
        }
        else//Ž���� ���� ������,
        {
            /*
             1. ������ Attack ����� �������� �ʹ��־ ������ ���� �������
             2. ������ ������� 
             */
            if(leader == null) // ������ ������ ���ڸ����� ���
            {
                ani.SetBool("Move", false);
                navMeshAgent.isStopped = true;
                return;
            }
            else // ������ ������ �������� �̵�
            {
                FollowOrder();
            }


            
        }
    }
    private void FollowOrder()
    {
        ani.SetBool("Move", true);
        if (navMeshAgent.isStopped) { 
        navMeshAgent.isStopped = false;
        }   
        navMeshAgent.SetDestination(leader.transform.position);
    }
    
}