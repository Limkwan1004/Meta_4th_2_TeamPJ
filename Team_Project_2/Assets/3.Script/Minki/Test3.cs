using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test3 : MonoBehaviour
{
    [SerializeField] private Ply_Controller player_con;
    [SerializeField] private Ply_Movement player_move;
    [SerializeField] private LeaderState Leader;

    private float Min = -210f;
    private float Max = 210f;

    private void Awake()
    {
        player_con = GetComponent<Ply_Controller>();
        player_move = GetComponent<Ply_Movement>();
        Leader = GetComponent<LeaderState>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            Following(2.5f);
        }
        else
        {
            Following(4.5f);
        }
    }

    /*public void Following()
    {
        float offset = 1f;

        Vector3 playerBack = player.transform.position - player.transform.forward * offset; // offset�� �÷��̾�� ���� ������ �Ÿ�

        for (int i = 0; i < player.UnitList_List.Count; i++)
        {
            player.UnitList_List[i].GetComponent<NavMeshAgent>().SetDestination(playerBack);
        }
    }*/

    public void Following_Shield(float speed)
    {
        // �÷��̾��� ���� �̵� ����� �ӵ�
        Vector3 playerDirection = player_move.MoveDir.normalized;

        for (int i = 0; i < player_con.UnitList_List.Count; i++)
        {
            GameObject unit = player_con.UnitList_List[i];

            // ������ ��ġ�� ȸ���� �÷��̾�� ����ȭ
            Vector3 newPosition = unit.transform.position + playerDirection * speed * Time.deltaTime;
            unit.transform.position = newPosition;

            if (playerDirection != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(playerDirection);
                unit.transform.rotation = newRotation;
            }
        }
    }

    public void Following(float speed)
    {
        float offset = 1f;
        float stoppingDistance = 2.5f; // ���簡 ���߱� ������ �Ÿ�

        Vector3 playerBack = player_con.transform.position - player_con.transform.forward * offset;

        for (int i = 0; i < player_con.UnitList_List.Count; i++)
        {
            GameObject unit = player_con.UnitList_List[i];
            NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();

            float distanceToDestination = Vector3.Distance(unit.transform.position, playerBack);

            if (distanceToDestination > stoppingDistance)
            {
                agent.SetDestination(playerBack);
                agent.speed = speed;
            }
            else
            {
                agent.velocity = Vector3.zero; // ���簡 ���������Ƿ� �ӵ��� 0���� ����
            }
        }
    }
}
