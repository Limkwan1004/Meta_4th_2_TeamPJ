using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitAttack : Minion_Controller
{
    //[SerializeField] private Ply_Controller player;
    /*
     �̴Ͼ��� �߽������� ���̾ �����ϴ� �� ����
    ���̾��� ���尡������� Ÿ������ ������
    Ÿ�ٰ����� �̴Ͼ��� ���� �ٶ󺸰� (Lookat�޼ҵ�) ������ �̵�  -> ����� Lerp�� �̵� ���� �׺���̼����� ����
    �̵��� �̴Ͼ��� ���ݹ��� �ݶ��̴�(�̴Ͼ�տ� ���� �ڽ��ݶ��̴� (���񼭹��̹�ó��))�� ������ ������ ����

     
     
     
     */
    //�ӽ� �̴Ͼ�ü��
    private int HP = 10;
    private bool isDie;



    // ���� ���ݰ�������
    public float scanRange = 13f;
    //������ ���� �̵��ӵ� -> �÷��̾� �̵��ӵ��� ���߱�
    private float moveSpeed = 1f;
    //�̵��� ���������� ���ݹ����ݶ��̴��� ��Ҵ°�?
    private bool isdetecting = false;
    //�������ΰ�?
    private bool isAttacking = false;
    private bool isHitting = false;
    private bool isSuccessAtk = true;
    private Animator ani;
    private Coroutine attackCoroutine;
    // ���� ��� ���̾�
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private GameObject weapon;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private BoxCollider boxCollider1;
    private BoxCollider weapon_col;
    private int layerIndex;
    public Transform nearestTarget;

    private WaitForSeconds attackDelay;
    private WaitForSeconds hitDelay = new WaitForSeconds(0.5f);

    private NavMeshAgent navMeshAgent;
    private void Start()
    {
        //���ݵ����� ����


        navMeshAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        weapon_col = weapon.GetComponent<BoxCollider>();



        //GameObject.FindGameObjectWithTag("Player").TryGetComponent<Ply_Controller>(out player);
        if (gameObject.layer == LayerMask.NameToLayer("Team"))
        {
            layerIndex = LayerMask.NameToLayer("Enemy");
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            layerIndex = LayerMask.NameToLayer("Team");
        }
        targetLayer = 1 << layerIndex;

    }

    private void Update()
    {

        //if(player.CurrentMode == Ply_Controller.Mode.Attack) { 
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, scanRange, Vector3.forward, 0, targetLayer);
        nearestTarget = GetNearestTarget(hits);



        if (nearestTarget != null && !isDie)
        {
            //Ÿ�ٰ����� Ÿ�������� �ٶ󺸱�
            LookatTarget(nearestTarget);
            // ������ ���ݹ����� �������� Ÿ�ٿ��� �̵�
            Debug.Log(isdetecting);
            if (!isdetecting && !isAttacking)
            {
                AttackMoving(nearestTarget);
            }
            // Ÿ���� ���ݹ����� �������� ����
            else
            {
                navMeshAgent.isStopped = true;
                ani.SetBool("Move", false);

                // 
                if (!isAttacking)
                {
                    attackCoroutine = StartCoroutine(Attack_co());
                    //StartCoroutine(Attack_co());
                }

            }
        }
        //}

        if (HP <= 0)
        {
            //�������� ,�̵����� 
            if (!isDie)
            {
                ani.SetTrigger("Dead");
                gameObject.layer = 9;
                boxCollider.enabled = false;
                boxCollider1.enabled = false;
                Destroy(gameObject, 3f);
            }
            isDie = true;
        }
    }
    //���̾� ������
    Transform GetNearestTarget(RaycastHit[] hits)
    {
        Transform nearest = null;
        float closestDistance = float.MaxValue;

        foreach (RaycastHit hit in hits)
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearest = hit.transform;
            }
        }

        return nearest;
    }
    private void LookatTarget(Transform target)
    {

        Vector3 AttackDir = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(AttackDir);
    }
    void AttackMoving(Transform target)
    {
        //���� �̴Ͼ���Ʈ�ѷ����� �����ҿ���.(�ִϸ��̼�)
        // ���� ������ ����
        //Debug.Log("����Ÿ�� : " + target.name);
        ani.SetBool("Move", true);
        navMeshAgent.isStopped = false;
        //���� �׺���̼����� �̵����� 
        navMeshAgent.SetDestination(target.transform.position);
        //transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);



    }
    //�������� �׸��¸޼ҵ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Team"))
        {
            if (other.CompareTag("Weapon") && other.gameObject.layer == LayerMask.NameToLayer("Enemy") && !isHitting)
            {
                StartCoroutine(Hit_co());
            }

        }
        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (other.CompareTag("Weapon") && other.gameObject.layer == LayerMask.NameToLayer("Team") && !isHitting)
            {
                StartCoroutine(Hit_co());
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Team"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {

                isdetecting = true;
                //���� �����ݶ��̴��� ������� �̵��� ���߰� ���� �ִϸ��̼� ����
            }
            else
            {
                isdetecting = false;
            }

        }
        else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Team"))
            {

                isdetecting = true;
                //���� �����ݶ��̴��� ������� �̵��� ���߰� ���� �ִϸ��̼� ����
            }
            else
            {
                isdetecting = false;
            }

        }


    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (gameObject.layer == LayerMask.NameToLayer("Team"))
    //    {
    //        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //        {
    //            Debug.Log("1111");
    //            isdetecting = false;
    //            //���� �����ݶ��̴��� ������� �̵��� ���߰� ���� �ִϸ��̼� ����
    //        }

    //    }
    //    else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        if (other.gameObject.layer == LayerMask.NameToLayer("Team"))
    //        {
    //            Debug.Log("2222");
    //            isdetecting = false;
    //            //���� �����ݶ��̴��� ������� �̵��� ���߰� ���� �ִϸ��̼� ����
    //        }
    //    }
    //}
    private IEnumerator Attack_co()
    {
        float d = Random.Range(2f, 2.1f);
        attackDelay = new WaitForSeconds(d);
        //���ݽð����� �̵��Ұ�
        //
        Debug.Log("���������");
        isAttacking = true;
        isSuccessAtk = false;
        ani.SetTrigger("Attack");
        yield return attackDelay;

        Debug.Log("���������");
        isAttacking = false;
    }

    private IEnumerator Hit_co()
    {
        isHitting = true;
        //��Ʈ�� ������ޱ�
        HP -= 1;


        //���ݵ��� ĵ���� ������Ÿ�� �ʱ�ȭ
        if (!isSuccessAtk)
        {

            Debug.Log("���ݵ��� �¾��� ���ʱ�ȭ");
            StopCoroutine(attackCoroutine);
            isAttacking = false;
        }

        ani.SetTrigger("Hit");
        yield return hitDelay;
        isHitting = false;


    }

    public void WeaponActive()
    {
        isSuccessAtk = true;
        weapon_col.enabled = true;
        Invoke("WeaponFalse", 0.1f);

    }
    private void WeaponFalse()
    {
        weapon_col.enabled = false;
    }
}